using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using autotrade.Steam.Market;
using autotrade.WorkingProcess.Caches;
using autotrade.WorkingProcess.PriceLoader;
using autotrade.WorkingProcess.Settings;

namespace autotrade.CustomElements.Controls.Settings
{
    public partial class CacheSettingsControl : UserControl
    {
        private static readonly string CACHED_TEXT = "Cached items quantity - ";
        private static readonly string CACHED_ABSOLETE_TEXT = "Obsolete quantity - ";

        public CacheSettingsControl()
        {
            InitializeComponent();

            var settings = SavedSettings.Get();
            CurrentCacheNumericUpDown.Value = settings.SETTINGS_HOURS_TO_BECOME_OLD_CURRENT_PRICE;
            AverageCacheNumericUpDown.Value = settings.SETTINGS_HOURS_TO_BECOME_OLD_AVERAGE_PRICE;

            InitCurrentCacheStatusesAsync();
        }

        private void InitCurrentCacheStatusesAsync()
        {
            UpdateButton.Enabled = false;
            Task.Run(() =>
            {
                SetCacheCount(ImagesCacheCountLable, GetCachedImagesCount());
                SetCacheCount(MarketIdCacheCountLable, GetCachedMarketIdsCount());
                SetCacheCount(SettingsCacheCountLable, GetChangedSettingsCount());

                SetCacheCount(AverageCacheCountLable, GetCachedAveragePricesCount());
                SetObsoleteCacheCount(AverageObsoleteCacheCountLable, GetObsoleteCachedAveragePricesCount());

                SetCacheCount(CurrentCacheCountLable, GetCachedCurrentPricesCount());
                SetObsoleteCacheCount(CurrentObsoleteCacheCountLable, GetObsoleteCachedCurrentPricesCount());
            });
            UpdateButton.Enabled = true;
        }

        private int GetCachedImagesCount()
        {
            return Directory.GetFiles(ImagesCache.IMAGES_PATH).Count();
        }

        private int GetCachedMarketIdsCount()
        {
            return MarketInfoCache.Get().Count;
        }

        private int GetCachedAveragePricesCount()
        {
            return PriceLoader.AVERAGE_PRICES_CACHE.Get().Count;
        }

        private int GetObsoleteCachedAveragePricesCount()
        {
            return GetObsoleteValues(PriceLoader.AVERAGE_PRICES_CACHE).Count;
        }

        private int GetCachedCurrentPricesCount()
        {
            return PriceLoader.CURRENT_PRICES_CACHE.Get().Count;
        }

        private int GetObsoleteCachedCurrentPricesCount()
        {
            return GetObsoleteValues(PriceLoader.CURRENT_PRICES_CACHE).Count;
        }

        private static Dictionary<string, LoadedItemPrice> GetObsoleteValues(PricesCache cache)
        {
            return cache.Get().Where(x => cache.IsOld(x.Value)).ToDictionary(x => x.Key, x => x.Value);
        }

        private static int GetChangedSettingsCount()
        {
            var count = 0;
            var oldSettings = SavedSettings.Get();
            var newSettings = new SavedSettings();
            var newSettingsType = newSettings.GetType();

            foreach (var item in oldSettings.GetType().GetFields())
            {
                var newValue = newSettingsType.GetField(item.Name).GetValue(newSettings);
                var oldValue = item.GetValue(oldSettings);
                if (newValue.ToString() != oldValue.ToString()) count++;
            }

            return count;
        }

        private void SetCacheCount(Label label, int count)
        {
            label.Text = CACHED_TEXT + count;
        }

        private void SetObsoleteCacheCount(Label label, int count)
        {
            label.Text = CACHED_ABSOLETE_TEXT + count;
        }

        private void CurrentCacheNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().SETTINGS_HOURS_TO_BECOME_OLD_CURRENT_PRICE,
                (int) CurrentCacheNumericUpDown.Value);
        }

        private void AverageCacheNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().SETTINGS_HOURS_TO_BECOME_OLD_AVERAGE_PRICE,
                (int) AverageCacheNumericUpDown.Value);
        }

        private void ImagesCacheOpenButton_Click(object sender, EventArgs e)
        {
            Process.Start(ImagesCache.IMAGES_PATH);
        }

        private void ImagesCacheClearButton_Click(object sender, EventArgs e)
        {
            if (ConfirmationClearCacheWindow())
            {
                var di = new DirectoryInfo(ImagesCache.IMAGES_PATH);
                foreach (var file in di.GetFiles())
                    try
                    {
                        file.Delete();
                    }
                    catch (IOException)
                    {
                    }

                SetCacheCount(ImagesCacheCountLable, GetCachedImagesCount());
            }
        }

        private void MarketIdCacheOpenButton_Click(object sender, EventArgs e)
        {
            Process.Start(MarketInfoCache.CACHE_PRICES_PATH);
            SetCacheCount(MarketIdCacheCountLable, GetCachedMarketIdsCount());
        }

        private void MarketIdCacheClearButton_Click(object sender, EventArgs e)
        {
            if (ConfirmationClearCacheWindow())
            {
                MarketInfoCache.Clear();
                SetCacheCount(MarketIdCacheCountLable, GetCachedMarketIdsCount());
            }
        }

        private void SettingsOpenButton_Click(object sender, EventArgs e)
        {
            Process.Start(SavedSettings.SETTINGS_FILE_PATH);
        }

        private void SettingsRestoreDefaultButton_Click(object sender, EventArgs e)
        {
            if (ConfirmationClearCacheWindow())
            {
                SavedSettings.RestoreDefault();
                SetCacheCount(SettingsCacheCountLable, GetChangedSettingsCount());
            }
        }

        private void AverageOpenButton_Click(object sender, EventArgs e)
        {
            Process.Start(PriceLoader.AVERAGE_PRICES_CACHE.CACHE_PRICES_PATH);
        }

        private void CurrentOpenButton_Click(object sender, EventArgs e)
        {
            Process.Start(PriceLoader.CURRENT_PRICES_CACHE.CACHE_PRICES_PATH);
        }

        private void AverageClearObsoleteButton_Click(object sender, EventArgs e)
        {
            if (ConfirmationClearCacheWindow())
            {
                PriceLoader.AVERAGE_PRICES_CACHE.ClearOld();
                SetCacheCount(AverageCacheCountLable, GetCachedAveragePricesCount());
                SetObsoleteCacheCount(AverageObsoleteCacheCountLable, GetObsoleteCachedAveragePricesCount());
            }
        }

        private void CurrentClearObsoleteButton_Click(object sender, EventArgs e)
        {
            if (ConfirmationClearCacheWindow())
            {
                PriceLoader.CURRENT_PRICES_CACHE.ClearOld();
                SetCacheCount(CurrentCacheCountLable, GetCachedCurrentPricesCount());
                SetObsoleteCacheCount(CurrentObsoleteCacheCountLable, GetObsoleteCachedCurrentPricesCount());
            }
        }

        private void AverageClearButton_Click(object sender, EventArgs e)
        {
            if (ConfirmationClearCacheWindow())
            {
                PriceLoader.AVERAGE_PRICES_CACHE.Clear();
                SetCacheCount(AverageCacheCountLable, GetCachedAveragePricesCount());
                SetObsoleteCacheCount(AverageObsoleteCacheCountLable, GetObsoleteCachedAveragePricesCount());
            }
        }

        private void CurrentClearButton_Click(object sender, EventArgs e)
        {
            if (ConfirmationClearCacheWindow())
            {
                PriceLoader.CURRENT_PRICES_CACHE.Clear();
                SetCacheCount(CurrentCacheCountLable, GetCachedCurrentPricesCount());
                SetObsoleteCacheCount(CurrentObsoleteCacheCountLable, GetObsoleteCachedCurrentPricesCount());
            }
        }

        private bool ConfirmationClearCacheWindow()
        {
            var confirmResult = MessageBox.Show("Are you sure you want to clear cached items?",
                "Confirm deletion?", MessageBoxButtons.YesNo);

            return confirmResult == DialogResult.Yes;
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            InitCurrentCacheStatusesAsync();
        }
    }
}