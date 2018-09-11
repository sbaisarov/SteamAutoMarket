namespace SteamAutoMarket.CustomElements.Controls.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using SteamAutoMarket.Steam.Market;
    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess.Caches;
    using SteamAutoMarket.WorkingProcess.PriceLoader;
    using SteamAutoMarket.WorkingProcess.Settings;

    public partial class CacheSettingsControl : UserControl
    {
        private const string CachedText = "Cached items quantity - ";

        private const string CachedObsoleteText = "Obsolete quantity - ";

        public CacheSettingsControl()
        {
            this.InitializeComponent();

            var settings = SavedSettings.Get();
            this.CurrentCacheNumericUpDown.Value = settings.SettingsHoursToBecomeOldCurrentPrice;
            this.AverageCacheNumericUpDown.Value = settings.SettingsHoursToBecomeOldAveragePrice;

            this.InitCurrentCacheStatusesAsync();
        }

        private static int GetCachedImagesCount()
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

        private static int GetCachedMarketIdsCount()
        {
            return MarketInfoCache.Get().Count;
        }

        private static int GetCachedAveragePricesCount()
        {
            return PriceLoader.AveragePricesCache.Get().Count;
        }

        private static int GetObsoleteCachedAveragePricesCount()
        {
            return GetObsoleteValues(PriceLoader.AveragePricesCache).Count;
        }

        private static int GetCachedCurrentPricesCount()
        {
            return PriceLoader.CurrentPricesCache.Get().Count;
        }

        private static int GetObsoleteCachedCurrentPricesCount()
        {
            return GetObsoleteValues(PriceLoader.CurrentPricesCache).Count;
        }

        private static Dictionary<string, LoadedItemPrice> GetObsoleteValues(PricesCache cache)
        {
            return cache.Get().Where(x => cache.IsOld(x.Value)).ToDictionary(x => x.Key, x => x.Value);
        }

        private static int GetChangedSettingsCount()
        {
            var oldSettings = SavedSettings.Get();
            var newSettings = new SavedSettings();
            var newSettingsType = newSettings.GetType();

            return (from item in oldSettings.GetType().GetFields()
                    let newValue = newSettingsType.GetField(item.Name).GetValue(newSettings)
                    let oldValue = item.GetValue(oldSettings)
                    where newValue.ToString() != oldValue.ToString()
                    select newValue).Count();
        }

        private static void SetCacheCount(Control label, int count)
        {
            label.Text = CachedText + count;
        }

        private static void SetObsoleteCacheCount(Control label, int count)
        {
            label.Text = CachedObsoleteText + count;
        }

        private static bool ConfirmationClearCacheWindow()
        {
            var confirmResult = MessageBox.Show(
                @"Are you sure you want to clear cached items?",
                @"Confirm deletion?",
                MessageBoxButtons.YesNo);

            return confirmResult == DialogResult.Yes;
        }

        private void InitCurrentCacheStatusesAsync()
        {
            try
            {
                this.UpdateButton.Enabled = false;
                Task.Run(
                    () =>
                        {
                            SetCacheCount(this.ImagesCacheCountLable, GetCachedImagesCount());
                            SetCacheCount(this.MarketIdCacheCountLable, GetCachedMarketIdsCount());
                            SetCacheCount(this.SettingsCacheCountLable, GetChangedSettingsCount());

                            SetCacheCount(this.AverageCacheCountLable, GetCachedAveragePricesCount());
                            SetObsoleteCacheCount(
                                this.AverageObsoleteCacheCountLable,
                                GetObsoleteCachedAveragePricesCount());

                            SetCacheCount(this.CurrentCacheCountLable, GetCachedCurrentPricesCount());
                            SetObsoleteCacheCount(
                                this.CurrentObsoleteCacheCountLable,
                                GetObsoleteCachedCurrentPricesCount());
                        });
                this.UpdateButton.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void CurrentCacheNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(
                    ref SavedSettings.Get().SettingsHoursToBecomeOldCurrentPrice,
                    (int)this.CurrentCacheNumericUpDown.Value);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AverageCacheNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(
                    ref SavedSettings.Get().SettingsHoursToBecomeOldAveragePrice,
                    (int)this.AverageCacheNumericUpDown.Value);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void ImagesCacheOpenButtonClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(ImagesCache.ImagesPath);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void ImagesCacheClearButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (!ConfirmationClearCacheWindow())
                {
                    return;
                }

                var di = new DirectoryInfo(ImagesCache.ImagesPath);
                foreach (var file in di.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (IOException)
                    {
                    }
                }

                SetCacheCount(this.ImagesCacheCountLable, GetCachedImagesCount());
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void MarketIdCacheOpenButtonClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(MarketInfoCache.CachePricesPath);
                SetCacheCount(this.MarketIdCacheCountLable, GetCachedMarketIdsCount());
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void MarketIdCacheClearButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (!ConfirmationClearCacheWindow())
                {
                    return;
                }

                MarketInfoCache.Clear();
                SetCacheCount(this.MarketIdCacheCountLable, GetCachedMarketIdsCount());
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void SettingsOpenButtonClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(SavedSettings.SettingsFilePath);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void SettingsRestoreDefaultButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (!ConfirmationClearCacheWindow())
                {
                    return;
                }

                SavedSettings.RestoreDefault();
                SetCacheCount(this.SettingsCacheCountLable, GetChangedSettingsCount());
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AverageOpenButtonClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(PriceLoader.AveragePricesCache.CachePricesPath);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void CurrentOpenButtonClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(PriceLoader.CurrentPricesCache.CachePricesPath);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AverageClearObsoleteButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (!ConfirmationClearCacheWindow())
                {
                    return;
                }

                PriceLoader.AveragePricesCache.ClearOld();
                SetCacheCount(this.AverageCacheCountLable, GetCachedAveragePricesCount());
                SetObsoleteCacheCount(this.AverageObsoleteCacheCountLable, GetObsoleteCachedAveragePricesCount());
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void CurrentClearObsoleteButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (!ConfirmationClearCacheWindow())
                {
                    return;
                }

                PriceLoader.CurrentPricesCache.ClearOld();
                SetCacheCount(this.CurrentCacheCountLable, GetCachedCurrentPricesCount());
                SetObsoleteCacheCount(this.CurrentObsoleteCacheCountLable, GetObsoleteCachedCurrentPricesCount());
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AverageClearButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (!ConfirmationClearCacheWindow())
                {
                    return;
                }

                PriceLoader.AveragePricesCache.Clear();
                SetCacheCount(this.AverageCacheCountLable, GetCachedAveragePricesCount());
                SetObsoleteCacheCount(this.AverageObsoleteCacheCountLable, GetObsoleteCachedAveragePricesCount());
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void CurrentClearButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (!ConfirmationClearCacheWindow())
                {
                    return;
                }

                PriceLoader.CurrentPricesCache.Clear();
                SetCacheCount(this.CurrentCacheCountLable, GetCachedCurrentPricesCount());
                SetObsoleteCacheCount(this.CurrentObsoleteCacheCountLable, GetObsoleteCachedCurrentPricesCount());
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void UpdateButtonClick(object sender, EventArgs e)
        {
            try
            {
                this.InitCurrentCacheStatusesAsync();
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }
    }
}