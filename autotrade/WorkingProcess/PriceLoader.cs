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
        public static PricesCash CURRENT_PRICES_CACHE;
        public static PricesCash AVERAGE_PRICES_CACHE;

        private static DataGridView ITEM_GRID;
        private static BackgroundWorker WORKING_THREAD;

        public static void Init(DataGridView grid) {
            if (ITEM_GRID == null) {
                ITEM_GRID = grid;
                WORKING_THREAD = new BackgroundWorker();
                WORKING_THREAD.DoWork += _loadItemsPrice;
                if (CURRENT_PRICES_CACHE == null) CURRENT_PRICES_CACHE = new PricesCash("current_prices_cache.ini");
                if (AVERAGE_PRICES_CACHE == null) AVERAGE_PRICES_CACHE = new PricesCash("average_prices_cache.ini");
            }
        }

        public static void StartPriceLoading() {
            if (WORKING_THREAD.IsBusy) return;
            WORKING_THREAD.RunWorkerAsync();
        }

        private static void _loadItemsPrice(object sender, DoWorkEventArgs ev) {
            var rows = _getRowsWithNoPrice();
            foreach (var row in rows) {
                try {
                    var priceCell = ItemsToSaleGridUtils.GetGridPriceTextBoxCell(ITEM_GRID, row.Index);
                    double? rowPrice = _getRowPrice(row);
                    if (rowPrice == -1) continue;
                    priceCell.Value = rowPrice;
                } catch (Exception e) {
                    Utils.Logger.Warning("Error on parsing item price", e);
                }
            }

            Thread.Sleep(1000);
            rows = _getRowsWithNoPrice();
            if (rows.Count() != 0) _loadItemsPrice(sender, ev);
        }

        private static IEnumerable<DataGridViewRow> _getRowsWithNoPrice() {
            return ITEM_GRID.Rows.Cast<DataGridViewRow>().Where(r => ItemsToSaleGridUtils.GetRowItemPrice(ITEM_GRID, r.Index).Equals(null));
        }

        private static double? _getRowPrice(DataGridViewRow row) {
            RgFullItem item = new RgFullItem();
            try {
                var cell = ItemsToSaleGridUtils.GetGridHidenItemsListCell(ITEM_GRID, row.Index);
                if (cell == null || cell.Value == null) return -1;

                item = ItemsToSaleGridUtils.GetRowItemsList(ITEM_GRID, row.Index).FirstOrDefault();
                if (item == null) return -1;

                double? price;

                var cached = CURRENT_PRICES_CACHE.Get(item);
                if (cached != null) { //some login with time
                    price = cached.Price;
                } else {
                    CurrentSession.SteamManager.GetCurrentPrice(out price, item.Asset, item.Description);
                    if (price != null && price != 0) {
                        CURRENT_PRICES_CACHE.Cache(item.Description.market_hash_name, price.Value);
                    }
                }

                return price;
            } catch (Exception e) {
                Utils.Logger.Debug($"Error on parsing item price - {e.Message}");
                return 0;
            }
        }
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
}
