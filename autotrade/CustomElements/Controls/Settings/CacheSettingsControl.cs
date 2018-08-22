using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using autotrade.WorkingProcess.Settings;
using System.IO;
using autotrade.WorkingProcess;
using autotrade.Steam.Market;
using autotrade.WorkingProcess.PriceLoader;
using System.Linq.Expressions;

namespace autotrade.CustomElements.Controls.Settings {
    public partial class CacheSettingsControl : UserControl {
        private readonly static string CACHED_TEXT = "Caches items quantity - ";
        private readonly static string CACHED_ABSOLETE_TEXT = "Not obsolete quantity - ";

        public CacheSettingsControl() {
            InitializeComponent();

            var settings = SavedSettings.Get();
            CurrentCacheNumericUpDown.Value = settings.SETTINGS_HOURS_TO_BECOME_OLD_CURRENT_PRICE;
            AverageCacheNumericUpDown.Value = settings.SETTINGS_HOURS_TO_BECOME_OLD_AVERAGE_PRICE;

            InitCurrentCacheStatusesAsync();
        }

        private void InitCurrentCacheStatusesAsync() {
            Task.Run(() => {
                SetCacheCount(ImagesCacheCountLable, Directory.GetFiles(ImagesCache.IMAGES_PATH).Count());
                SetCacheCount(MarketIdCacheCountLable, MarketInfoCache.Get().Count);
                SetCacheCount(SettingsCacheCountLable, GetChangedSettingsCount());

                var averagePriceCache = PriceLoader.AVERAGE_PRICES_CACHE;
                SetCacheCount(AverageCacheCountLable, averagePriceCache.Get().Count);
                SetObsoleteCacheCount(AverageObsoleteCacheCountLable, GetObsoleteValues(averagePriceCache).Count);

                var currentPriceCache = PriceLoader.CURRENT_PRICES_CACHE;
                SetCacheCount(CurrentCacheCountLable, currentPriceCache.Get().Count);
                SetObsoleteCacheCount(CurrentObsoleteCacheCountLable, GetObsoleteValues(currentPriceCache).Count);
            });
        }

        private static Dictionary<string, LoadedItemPrice> GetObsoleteValues(PricesCache cache) {
            return cache.Get().Where(x => cache.IsOld(x.Value)).ToDictionary(x => x.Key, x => x.Value);
        }

        private static int GetChangedSettingsCount() {
            int count = 0;
            var oldSettings = SavedSettings.Get();
            var newSettings = new SavedSettings();
            var newSettingsType = newSettings.GetType();

            foreach (var item in oldSettings.GetType().GetFields()) {
                if (newSettingsType.GetField(item.Name).GetValue(newSettings) != item.GetValue(oldSettings)) {
                    count++;
                }
            }
            return count;
        }

        private void SetCacheCount(Label label, int count) {
            label.Text = CACHED_TEXT + count;
        }

        private void SetObsoleteCacheCount(Label label, int count) {
            label.Text = CACHED_ABSOLETE_TEXT + count;
        }

        private void CurrentCacheNumericUpDown_ValueChanged(object sender, EventArgs e) {
            SavedSettings.UpdateField(ref SavedSettings.Get().SETTINGS_HOURS_TO_BECOME_OLD_CURRENT_PRICE, (int)CurrentCacheNumericUpDown.Value);
        }

        private void AverageCacheNumericUpDown_ValueChanged(object sender, EventArgs e) {
            SavedSettings.UpdateField(ref SavedSettings.Get().SETTINGS_HOURS_TO_BECOME_OLD_AVERAGE_PRICE, (int)AverageCacheNumericUpDown.Value);
        }
    }
}
