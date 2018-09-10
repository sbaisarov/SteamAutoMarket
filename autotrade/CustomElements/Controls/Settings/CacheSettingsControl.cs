using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using SteamAutoMarket.Steam.Market;
using SteamAutoMarket.WorkingProcess.Caches;
using SteamAutoMarket.WorkingProcess.PriceLoader;
using SteamAutoMarket.WorkingProcess.Settings;

namespace SteamAutoMarket.CustomElements.Controls.Settings
{
    public partial class CacheSettingsControl : UserControl
    {
        private static readonly string CACHED_TEXT = "Cached items quantity - ";

        private static readonly string CACHED_ABSOLETE_TEXT = "Obsolete quantity - ";

        public CacheSettingsControl()
        {
            InitializeComponent();

            var settings = SavedSettings.Get();
            CurrentCacheNumericUpDown.Value = settings.SettingsHoursToBecomeOldCurrentPrice;
            AverageCacheNumericUpDown.Value = settings.SettingsHoursToBecomeOldAveragePrice;

            InitCurrentCacheStatusesAsync();
        }

        private void InitCurrentCacheStatusesAsync()
        {
            UpdateButton.Enabled = false;
            Task.Run(
                () =>
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
            try
            {
                return Directory.GetFiles(ImagesCache.ImagesPath).Count();
            }
            catch (DirectoryNotFoundException)
            {
                return 0;
            }
        }

        private int GetCachedMarketIdsCount()
        {
            return MarketInfoCache.Get().Count;
        }

        private int GetCachedAveragePricesCount()
        {
            return PriceLoader.AveragePricesCache.Get().Count;
        }

        private int GetObsoleteCachedAveragePricesCount()
        {
            return GetObsoleteValues(PriceLoader.AveragePricesCache).Count;
        }

        private int GetCachedCurrentPricesCount()
        {
            return PriceLoader.CurrentPricesCache.Get().Count;
        }

        private int GetObsoleteCachedCurrentPricesCount()
        {
            return GetObsoleteValues(PriceLoader.CurrentPricesCache).Count;
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
            SavedSettings.UpdateField(
                ref SavedSettings.Get().SettingsHoursToBecomeOldCurrentPrice,
                (int)CurrentCacheNumericUpDown.Value);
        }

        private void AverageCacheNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(
                ref SavedSettings.Get().SettingsHoursToBecomeOldAveragePrice,
                (int)AverageCacheNumericUpDown.Value);
        }

        private void ImagesCacheOpenButton_Click(object sender, EventArgs e)
        {
            Process.Start(ImagesCache.ImagesPath);
        }

        private void ImagesCacheClearButton_Click(object sender, EventArgs e)
        {
            if (ConfirmationClearCacheWindow())
            {
                var di = new DirectoryInfo(ImagesCache.ImagesPath);
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
            Process.Start(MarketInfoCache.CachePricesPath);
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
            Process.Start(SavedSettings.SettingsFilePath);
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
            Process.Start(PriceLoader.AveragePricesCache.CachePricesPath);
        }

        private void CurrentOpenButton_Click(object sender, EventArgs e)
        {
            Process.Start(PriceLoader.CurrentPricesCache.CachePricesPath);
        }

        private void AverageClearObsoleteButton_Click(object sender, EventArgs e)
        {
            if (ConfirmationClearCacheWindow())
            {
                PriceLoader.AveragePricesCache.ClearOld();
                SetCacheCount(AverageCacheCountLable, GetCachedAveragePricesCount());
                SetObsoleteCacheCount(AverageObsoleteCacheCountLable, GetObsoleteCachedAveragePricesCount());
            }
        }

        private void CurrentClearObsoleteButton_Click(object sender, EventArgs e)
        {
            if (ConfirmationClearCacheWindow())
            {
                PriceLoader.CurrentPricesCache.ClearOld();
                SetCacheCount(CurrentCacheCountLable, GetCachedCurrentPricesCount());
                SetObsoleteCacheCount(CurrentObsoleteCacheCountLable, GetObsoleteCachedCurrentPricesCount());
            }
        }

        private void AverageClearButton_Click(object sender, EventArgs e)
        {
            if (ConfirmationClearCacheWindow())
            {
                PriceLoader.AveragePricesCache.Clear();
                SetCacheCount(AverageCacheCountLable, GetCachedAveragePricesCount());
                SetObsoleteCacheCount(AverageObsoleteCacheCountLable, GetObsoleteCachedAveragePricesCount());
            }
        }

        private void CurrentClearButton_Click(object sender, EventArgs e)
        {
            if (ConfirmationClearCacheWindow())
            {
                PriceLoader.CurrentPricesCache.Clear();
                SetCacheCount(CurrentCacheCountLable, GetCachedCurrentPricesCount());
                SetObsoleteCacheCount(CurrentObsoleteCacheCountLable, GetObsoleteCachedCurrentPricesCount());
            }
        }

        private bool ConfirmationClearCacheWindow()
        {
            var confirmResult = MessageBox.Show(
                "Are you sure you want to clear cached items?",
                "Confirm deletion?",
                MessageBoxButtons.YesNo);

            return confirmResult == DialogResult.Yes;
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            InitCurrentCacheStatusesAsync();
        }
    }
}