namespace SteamAutoMarket.Pages.Settings
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    using Core;

    using Steam.Market;

    using SteamAutoMarket.Properties;
    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Repository.Image;
    using SteamAutoMarket.Repository.Settings;
    using SteamAutoMarket.Utils.Logger;

    /// <summary>
    /// Interaction logic for CacheSettings.xaml
    /// </summary>
    public partial class CacheSettings : INotifyPropertyChanged
    {
        private int averagePricesCount;

        private int cachedImagesCount;

        private int currentPricesCount;

        private int marketIdCount;

        public CacheSettings()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int AveragePricesCount
        {
            get => this.averagePricesCount;
            set
            {
                this.averagePricesCount = value;
                this.OnPropertyChanged();
            }
        }

        public int CachedImagesCount
        {
            get => this.cachedImagesCount;
            set
            {
                this.cachedImagesCount = value;
                this.OnPropertyChanged();
            }
        }

        public int CurrentPricesCount
        {
            get => this.currentPricesCount;
            set
            {
                this.currentPricesCount = value;
                this.OnPropertyChanged();
            }
        }

        public int MarketIdCount
        {
            get => this.marketIdCount;
            set
            {
                this.marketIdCount = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void AveragePriceButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            if (File.Exists(UiGlobalVariables.SteamManager.AveragePriceCache.FilePath) == false)
            {
                ErrorNotify.CriticalMessageBox(
                    $"{UiGlobalVariables.SteamManager.AveragePriceCache.FilePath} directory not found");
                return;
            }

            Process.Start(UiGlobalVariables.SteamManager.AveragePriceCache.FilePath);
        }

        private void CurrentPriceButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            if (File.Exists(UiGlobalVariables.SteamManager.CurrentPriceCache.FilePath) == false)
            {
                ErrorNotify.CriticalMessageBox(
                    $"{UiGlobalVariables.SteamManager.CurrentPriceCache.FilePath} directory not found");
                return;
            }

            Process.Start(UiGlobalVariables.SteamManager.CurrentPriceCache.FilePath);
        }

        private int GetAveragePriceCachedItemsCount()
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                Logger.Log.Debug(
                    "No logged in account found. Average price cache cant be loaded. Cached items count is 0");
                return 0;
            }

            try
            {
                Logger.Log.Debug(
                    $"Getting count of cached images from {UiGlobalVariables.SteamManager.AveragePriceCache.FilePath}");
                return UiGlobalVariables.SteamManager.AveragePriceCache.Get().Count;
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
                return 0;
            }
        }

        private int GetCachedImagesCount()
        {
            if (Directory.Exists(ImageCache.ImagesPath) == false)
            {
                Logger.Log.Debug($"Directory - {ImageCache.ImagesPath} do not exist. Cached images count is 0");
                return 0;
            }

            try
            {
                Logger.Log.Debug($"Getting count of cached images from {ImageCache.ImagesPath}");
                return Directory.EnumerateFiles(ImageCache.ImagesPath).Count();
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
                return 0;
            }
        }

        private int GetCachedMarketIdsCount()
        {
            if (File.Exists(MarketInfoCache.CacheFilePath) == false)
            {
                Logger.Log.Debug(
                    $"Directory - {MarketInfoCache.CacheFilePath} do not exist. Cached items id count is 0");
                return 0;
            }

            try
            {
                Logger.Log.Debug($"Getting count of cached images from {MarketInfoCache.CacheFilePath}");
                return MarketInfoCache.Get().Count;
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
                return 0;
            }
        }

        private int GetCurrentPriceCachedItemsCount()
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                Logger.Log.Debug(
                    "No logged in account found. Current price cache cant be loaded. Cached items count is 0");
                return 0;
            }

            try
            {
                Logger.Log.Debug(
                    $"Getting count of cached images from {UiGlobalVariables.SteamManager.CurrentPriceCache.FilePath}");
                return UiGlobalVariables.SteamManager.CurrentPriceCache.Get().Count;
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
                return 0;
            }
        }

        private void ImagesOpenFolderOnClick(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(ImageCache.ImagesPath) == false)
            {
                ErrorNotify.CriticalMessageBox($"{ImageCache.ImagesPath} directory not found");
                return;
            }

            Process.Start(ImageCache.ImagesPath);
        }

        private void MarketIdClear(object sender, RoutedEventArgs e)
        {
            if (File.Exists(MarketInfoCache.CacheFilePath) == false)
            {
                ErrorNotify.CriticalMessageBox($"{MarketInfoCache.CacheFilePath} directory not found");
                return;
            }

            Process.Start(MarketInfoCache.CacheFilePath);
        }

        private void ReloadButtonClick(object sender, RoutedEventArgs e)
        {
            this.CachedImagesCount = this.GetCachedImagesCount();
            this.MarketIdCount = this.GetCachedMarketIdsCount();
            this.CurrentPricesCount = this.GetCurrentPriceCachedItemsCount();
            this.AveragePricesCount = this.GetAveragePriceCachedItemsCount();
        }

        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {
            if (File.Exists(SettingsUpdated.SettingsPath) == false)
            {
                ErrorNotify.CriticalMessageBox($"{SettingsUpdated.SettingsPath} directory not found");
                return;
            }

            Process.Start(SettingsUpdated.SettingsPath);
        }
    }
}