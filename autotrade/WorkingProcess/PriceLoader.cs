using autotrade.CustomElements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static autotrade.Steam.TradeOffer.Inventory;

namespace autotrade.WorkingProcess {
    class PriceLoader {
        private static bool IS_FORCED = false;

        public static PricesCash CURRENT_PRICES_CACHE;
        public static PricesCash AVERAGE_PRICES_CACHE;

        private static DataGridView ALL_ITEMS_GRID;
        private static DataGridView ITEMS_TO_SALE_GRID;

        private static BackgroundWorker ALL_ITEMS_WORKING_THREAD;
        private static BackgroundWorker ITEMS_TO_SALE_WORKING_THREAD;

        private static bool AllItemThreadShouldWait = false;

        public static void Init(DataGridView allItemsGrid, DataGridView itemsToSaleGrid) {
            if (ITEMS_TO_SALE_GRID == null || ALL_ITEMS_GRID == null) {

                ALL_ITEMS_GRID = allItemsGrid;
                ITEMS_TO_SALE_GRID = itemsToSaleGrid;

                ALL_ITEMS_WORKING_THREAD = new BackgroundWorker();
                ALL_ITEMS_WORKING_THREAD.DoWork += LoadAllItemsToSalePrices;

                ITEMS_TO_SALE_WORKING_THREAD = new BackgroundWorker();
                ITEMS_TO_SALE_WORKING_THREAD.DoWork += LoadItemsToSalePrices;

                if (CURRENT_PRICES_CACHE == null) CURRENT_PRICES_CACHE = new PricesCash("current_prices_cache.ini");
                if (AVERAGE_PRICES_CACHE == null) AVERAGE_PRICES_CACHE = new PricesCash("average_prices_cache.ini");
            }
        }

        public static void StopAll() {
            if (ALL_ITEMS_WORKING_THREAD.IsBusy) ALL_ITEMS_WORKING_THREAD.CancelAsync();
            if (ITEMS_TO_SALE_WORKING_THREAD.IsBusy) ITEMS_TO_SALE_WORKING_THREAD.CancelAsync();
        }

        public static void StartPriceLoading(TableToLoad tableToLoad, bool force = false) {
            IS_FORCED = force;
            if (IS_FORCED) {
                ClearAllPriceCells(tableToLoad);
            }

            switch (tableToLoad) {
                case TableToLoad.ALL_ITEMS_TABLE: {
                        if (ALL_ITEMS_WORKING_THREAD.IsBusy) return;
                        if (ITEMS_TO_SALE_WORKING_THREAD.IsBusy) AllItemThreadShouldWait = true;

                        ALL_ITEMS_WORKING_THREAD.RunWorkerAsync();
                        break;
                    }

                case TableToLoad.ITEMS_TO_SALE_TABLE: {
                        if (ITEMS_TO_SALE_WORKING_THREAD.IsBusy) return;
                        AllItemThreadShouldWait = true;

                        ITEMS_TO_SALE_WORKING_THREAD.RunWorkerAsync();
                        break;
                    }
            }

        }

        private static void WaitFor_AllItemThreadShouldWait() {
            if (AllItemThreadShouldWait == false) return;
            else {
                Thread.Sleep(1000);
                WaitFor_AllItemThreadShouldWait();
            }
        }

        public static void ClearAllPriceCells(TableToLoad tableToLoad) {
            DataGridViewTextBoxCell cell;

            if (tableToLoad == TableToLoad.ALL_ITEMS_TABLE) {
                foreach (var row in ALL_ITEMS_GRID.Rows.Cast<DataGridViewRow>()) {
                    cell = AllItemsListGridUtils.GetGridCurrentPriceTextBoxCell(ALL_ITEMS_GRID, row.Index);
                    if (cell != null) cell.Value = null;

                    cell = AllItemsListGridUtils.GetGridAveragePriceTextBoxCell(ALL_ITEMS_GRID, row.Index);
                    if (cell != null) cell.Value = null;
                }
            } else if (tableToLoad == TableToLoad.ITEMS_TO_SALE_TABLE) {
                foreach (var row in ITEMS_TO_SALE_GRID.Rows.Cast<DataGridViewRow>()) {
                    cell = ItemsToSaleGridUtils.GetGridPriceTextBoxCell(ALL_ITEMS_GRID, row.Index);
                    if (cell != null) cell.Value = null;

                    cell = ItemsToSaleGridUtils.GetGridAveragePriceTextBoxCell(ALL_ITEMS_GRID, row.Index);
                    if (cell != null) cell.Value = null;
                }
            }
        }

        #region ALL ITEMS
        private static void LoadAllItemsToSalePrices(object sender, DoWorkEventArgs ev) {
            var rows = GetAllItemsRowsWithNoPrice();
            foreach (var row in rows) {
                try {
                    var currentPriceCell = AllItemsListGridUtils.GetGridCurrentPriceTextBoxCell(ALL_ITEMS_GRID, row.Index);
                    var averagePriceCell = AllItemsListGridUtils.GetGridAveragePriceTextBoxCell(ALL_ITEMS_GRID, row.Index);

                    var prices = GetAllItemsRowPrice(row);

                    double? currentPrice = prices.Item1;
                    if (currentPrice != -1) {
                        currentPriceCell.Value = currentPrice;
                    }

                    //todo
                    double? averagePrice = prices.Item2;
                    if (averagePrice != -1) {
                        averagePriceCell.Value = averagePrice;
                    }

                } catch (Exception e) {
                    Utils.Logger.Warning("Error on parsing item price", e);
                }
                WaitFor_AllItemThreadShouldWait();
            }

            Thread.Sleep(1000);
            rows = GetItemsToSaleRowsWithNoPrice();
            if (rows.Count() != 0) LoadItemsToSalePrices(sender, ev);
        }

        private static IEnumerable<DataGridViewRow> GetAllItemsRowsWithNoPrice() {
            return ALL_ITEMS_GRID.Rows.Cast<DataGridViewRow>()
                .Where(r =>
                AllItemsListGridUtils.GetRowItemPrice(ALL_ITEMS_GRID, r.Index).Equals(null)
                ||
                AllItemsListGridUtils.GetRowAveragePrice(ALL_ITEMS_GRID, r.Index).Equals(null)
                );
        }

        private static Tuple<double?, double?> GetAllItemsRowPrice(DataGridViewRow row) {
            RgFullItem item = new RgFullItem();
            try {
                var itemsCell = AllItemsListGridUtils.GetGridHidenItemsListCell(ALL_ITEMS_GRID, row.Index);
                if (itemsCell == null || itemsCell.Value == null) return new Tuple<double?, double?>(-1, -1);

                item = ((List<RgFullItem>)itemsCell.Value).FirstOrDefault();
                if (item == null) return new Tuple<double?, double?>(-1, -1);

                double? currentPrice = null;
                if (IS_FORCED == false) {
                    var cachedCurrentPrice = CURRENT_PRICES_CACHE.Get(item);
                    if (cachedCurrentPrice != null) {
                        currentPrice = cachedCurrentPrice.Price;
                    }
                }

                if (currentPrice == null) {
                    CurrentSession.SteamManager.GetCurrentPrice(out currentPrice, item.Asset, item.Description);
                    if (currentPrice != null && currentPrice != 0) {
                        CURRENT_PRICES_CACHE.Cache(item.Description.market_hash_name, currentPrice.Value);
                    }
                }

                double? averagePrice = null;
                if (IS_FORCED == false) {
                    var cachedCurrentPrice = AVERAGE_PRICES_CACHE.Get(item);
                    if (cachedCurrentPrice != null) {
                        averagePrice = cachedCurrentPrice.Price;
                    }
                }

                if (averagePrice == null) {
                    CurrentSession.SteamManager.GetAveragePrice(out averagePrice, item.Asset, item.Description, 100);
                    if (averagePrice != null && averagePrice != 0) {
                        AVERAGE_PRICES_CACHE.Cache(item.Description.market_hash_name, averagePrice.Value);
                    }
                }

                return new Tuple<double?, double?>(currentPrice, averagePrice);
            } catch (Exception e) {
                Utils.Logger.Debug($"Error on parsing item price - {e.Message}");
                return new Tuple<double?, double?>(0, 0);
            }
        }
        #endregion

        #region ITEMS TO SALE
        private static void LoadItemsToSalePrices(object sender, DoWorkEventArgs ev) {
            var rows = GetItemsToSaleRowsWithNoPrice();
            foreach (var row in rows) {
                try {
                    var currentPriceCell = ItemsToSaleGridUtils.GetGridPriceTextBoxCell(ITEMS_TO_SALE_GRID, row.Index);
                    var averagePriceCell = ItemsToSaleGridUtils.GetGridAveragePriceTextBoxCell(ITEMS_TO_SALE_GRID, row.Index);


                    var prices = GetItemsToSaleRowPrice(row);

                    double? currentPrice = prices.Item1;
                    if (currentPrice != -1) {
                        currentPriceCell.Value = currentPrice;
                    }

                    double? averagePrice = prices.Item2;
                    if (averagePrice != -1) {
                        averagePriceCell.Value = averagePrice;
                    }
                } catch (Exception e) {
                    Utils.Logger.Warning("Error on parsing item price", e);
                }
            }

            Thread.Sleep(1000);
            rows = GetItemsToSaleRowsWithNoPrice();
            if (rows.Count() != 0) {
                LoadItemsToSalePrices(sender, ev);
            } else {
                AllItemThreadShouldWait = false;
            }

        }

        private static IEnumerable<DataGridViewRow> GetItemsToSaleRowsWithNoPrice() {
            return ITEMS_TO_SALE_GRID.Rows.Cast<DataGridViewRow>()
                .Where(
                r =>
                ItemsToSaleGridUtils.GetRowItemPrice(ITEMS_TO_SALE_GRID, r.Index).Equals(null)
                ||
                ItemsToSaleGridUtils.GetRowAveragePrice(ITEMS_TO_SALE_GRID, r.Index).Equals(null)
                );
        }

        private static Tuple<double?, double?> GetItemsToSaleRowPrice(DataGridViewRow row) {
            RgFullItem item = new RgFullItem();
            try {
                var itemsCell = ItemsToSaleGridUtils.GetGridHidenItemsListCell(ITEMS_TO_SALE_GRID, row.Index);
                if (itemsCell == null || itemsCell.Value == null) return new Tuple<double?, double?>(-1, -1);

                item = ItemsToSaleGridUtils.GetRowItemsList(ITEMS_TO_SALE_GRID, row.Index).FirstOrDefault();
                if (item == null) return new Tuple<double?, double?>(-1, -1);

                double? currentPrice = null;
                if (IS_FORCED == false) {
                    var cachedCurrentPrice = CURRENT_PRICES_CACHE.Get(item);
                    if (cachedCurrentPrice != null) {
                        currentPrice = cachedCurrentPrice.Price;
                    }
                }

                if (currentPrice == null) {
                    CurrentSession.SteamManager.GetCurrentPrice(out currentPrice, item.Asset, item.Description);
                    if (currentPrice != null && currentPrice != 0) {
                        CURRENT_PRICES_CACHE.Cache(item.Description.market_hash_name, currentPrice.Value);
                    }
                }

                double? averagePrice = null;
                if (IS_FORCED == false) {
                    var cachedCurrentPrice = AVERAGE_PRICES_CACHE.Get(item);
                    if (cachedCurrentPrice != null) {
                        averagePrice = cachedCurrentPrice.Price;
                    }
                }

                if (averagePrice == null) {
                    CurrentSession.SteamManager.GetCurrentPrice(out averagePrice, item.Asset, item.Description);
                    if (averagePrice != null && averagePrice != 0) {
                        AVERAGE_PRICES_CACHE.Cache(item.Description.market_hash_name, averagePrice.Value);
                    }
                }

                return new Tuple<double?, double?>(currentPrice, averagePrice);
            } catch (Exception e) {
                Utils.Logger.Debug($"Error on parsing item price - {e.Message}");
                return new Tuple<double?, double?>(0, 0);
            }
        }
        #endregion

    }

    class LoadedItemPrice {
        public DateTime ParseTime { get; set; }
        public double Price { get; set; }

        public LoadedItemPrice(DateTime parseTime, double price) {
            ParseTime = parseTime;
            Price = price;
        }
    }

    class PricesCash {
        private Dictionary<string, LoadedItemPrice> CACHE;
        private readonly string CACHE_PRICES_PATH;
        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };

        public PricesCash(string filePath) {
            CACHE_PRICES_PATH = filePath;
        }

        private Dictionary<string, LoadedItemPrice> Get() {
            if (CACHE == null) {
                if (File.Exists(CACHE_PRICES_PATH)) {
                    CACHE = JsonConvert.DeserializeObject<Dictionary<string, LoadedItemPrice>>(File.ReadAllText(CACHE_PRICES_PATH), _jsonSettings);
                } else {
                    CACHE = new Dictionary<string, LoadedItemPrice>();
                    UpdateAll();
                }
            }
            return CACHE;
        }

        public LoadedItemPrice Get(RgFullItem item) {
            //todo some login with time
            Get().TryGetValue(item.Description.market_hash_name, out LoadedItemPrice cached);
            return cached;
        }

        public void Cache(string hashName, double price) {
            Get()[hashName] = new LoadedItemPrice(DateTime.Now, price);
            UpdateAll();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateAll() {
            File.WriteAllText(CACHE_PRICES_PATH, JsonConvert.SerializeObject(Get(), Formatting.Indented, _jsonSettings));
        }
    }

    public enum TableToLoad {
        ALL_ITEMS_TABLE,
        ITEMS_TO_SALE_TABLE
    }
}
