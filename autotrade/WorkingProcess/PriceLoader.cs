using autotrade.CustomElements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static autotrade.Steam.TradeOffer.Inventory;

namespace autotrade.WorkingProcess {
    class PriceLoader {
        public static PricesCash ITEM_PRICES_CACHE;
        public static PricesCash AVERAGE_PRICES_CACHE;

        private static DataGridView ITEM_GRID;
        private static BackgroundWorker WORKING_THREAD;

        public static void Init(DataGridView grid) {
            if (ITEM_GRID == null) {
                ITEM_GRID = grid;
                WORKING_THREAD = new BackgroundWorker();
                WORKING_THREAD.DoWork += _loadItemsPrice;
                //todo init price caches}
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
                    priceCell.Value = _getRowPrice(row);
                } catch (Exception e) {
                    Utils.Logger.Warning("Error on parsing item price", e);
                }
            }

            rows = _getRowsWithNoPrice();
            if (rows.Count() != 0) _loadItemsPrice(sender, ev);
        }

        private static IEnumerable<DataGridViewRow> _getRowsWithNoPrice() {
            return ITEM_GRID.Rows.Cast<DataGridViewRow>().Where(r => ItemsToSaleGridUtils.GetRowItemPrice(ITEM_GRID, r.Index).Equals(null));
        }

        private static double? _getRowPrice(DataGridViewRow row) {
            RgFullItem item = new RgFullItem();
            try {
                item = ItemsToSaleGridUtils.GetRowItemsList(ITEM_GRID, row.Index).First();
                CurrentSession.SteamManager.GetCurrentPrice(out double? price, item.Asset, item.Description);
                //todo cashe here
                return price;
            } catch (Exception e) {
                Utils.Logger.Debug($"Error on parsing {item.Description.market_hash_name} - {e.Message}");
                return 0;
            }
        }
    }


    class LoadedItemPrice {
        RgFullItem Item { get; set; }
        DateTime ParseTime { get; set; }
        double Price { get; set; }
    }

    class PricesCash {

    }
}
