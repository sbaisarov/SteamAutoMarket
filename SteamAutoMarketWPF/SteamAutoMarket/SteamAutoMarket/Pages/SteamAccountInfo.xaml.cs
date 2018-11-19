namespace SteamAutoMarket.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;

    using Core;

    using Steam.Market.Interface;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Models;
    using SteamAutoMarket.Models.Enums;
    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Utils.Extension;
    using SteamAutoMarket.Utils.Logger;

    /// <summary>
    /// Interaction logic for SteamAccountInfo.xaml
    /// </summary>
    public partial class SteamAccountInfo : INotifyPropertyChanged
    {
        private string avatar = ResourceUtils.GetResourceImageUri("NoAvatar.jpg");

        private bool refreshButtonIsEnabled = true;

        public SteamAccountInfo()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Avatar
        {
            get => this.avatar;
            set
            {
                if (value == this.avatar) return;
                this.avatar = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<AccountInfoModel> AccountParameters { get; } =
            new ObservableCollection<AccountInfoModel>();

        public bool RefreshButtonIsEnabled
        {
            get => this.refreshButtonIsEnabled;
            set
            {
                if (value == this.refreshButtonIsEnabled) return;
                this.refreshButtonIsEnabled = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private IEnumerable<EAccountInfoFlags> GetCurrentFlags()
        {
            var flags = new List<EAccountInfoFlags>();

            if (this.AccountInfoCheckbox.IsChecked != null && this.AccountInfoCheckbox.IsChecked.Value)
            {
                flags.Add(EAccountInfoFlags.AccountInfo);
            }

            if (this.MafileInfoCheckbox.IsChecked != null && this.MafileInfoCheckbox.IsChecked.Value)
            {
                flags.Add(EAccountInfoFlags.MafileInfo);
            }

            if (this.ProfileInfoCheckbox.IsChecked != null && this.ProfileInfoCheckbox.IsChecked.Value)
            {
                flags.Add(EAccountInfoFlags.ProfileInfo);
            }

            if (this.WalletInfoCheckbox.IsChecked != null && this.WalletInfoCheckbox.IsChecked.Value)
            {
                flags.Add(EAccountInfoFlags.WalletInfo);
            }

            return flags;
        }

        private void RefreshAccountInfo_OnClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            var flags = this.GetCurrentFlags();
            if (!flags.Any())
            {
                ErrorNotify.CriticalMessageBox(
                    "No info flags was marked to load! Mark items before starting info parse");
                return;
            }

            this.AccountParameters.Clear();
            Task.Run(
                () =>
                    {
                        try
                        {
                            this.RefreshButtonIsEnabled = false;
                            var manager = UiGlobalVariables.SteamManager;
                            this.Avatar =
                                ImageUtils.GetSteamProfileFullImageUri(manager.SteamId.ConvertToUInt64().ToString());

                            foreach (var flag in flags)
                            {
                                switch (flag)
                                {
                                    case EAccountInfoFlags.AccountInfo:
                                        {
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("Login", manager.Login));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("Password", manager.Password));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel(
                                                    "SteamID",
                                                    manager.SteamId.ConvertToUInt64().ToString()));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("TradeToken", manager.TradeToken));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("APIKey", manager.ApiKey));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel(
                                                    "CurrencyValue",
                                                    MarketClient.Currencies[$"{manager.Currency}"]));
                                            break;
                                        }

                                    case EAccountInfoFlags.MafileInfo:
                                        {
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("SharedSecret", manager.Guard.SharedSecret));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("IdentitySecret", manager.Guard.IdentitySecret));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("RevocationCode", manager.Guard.RevocationCode));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("DeviceID", manager.Guard.DeviceID));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("Secret1", manager.Guard.Secret1));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("SerialNumber", manager.Guard.SerialNumber));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("TokenGID", manager.Guard.TokenGID));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("URI", manager.Guard.URI));
                                            break;
                                        }

                                    case EAccountInfoFlags.ProfileInfo:
                                        {
                                            // todo
                                            break;
                                        }

                                    case EAccountInfoFlags.WalletInfo:
                                        {
                                            var info = manager.MarketClient.WalletInfo();
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("WalletCountry", info.WalletCountry));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("Currency", info.Currency));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("MaxBalance", info.MaxBalance));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("WalletBalance", info.WalletBalance));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("WalletFee", info.WalletFee));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("WalletFeeMinimum", info.WalletFeeMinimum));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel("WalletFeePercent", info.WalletFeePercent));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel(
                                                    "WalletPublisherFeePercent",
                                                    info.WalletPublisherFeePercent));
                                            this.AccountParameters.AddDispatch(
                                                new AccountInfoModel(
                                                    "WalletTradeMaxBalance",
                                                    info.WalletTradeMaxBalance));
                                            break;
                                        }

                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorNotify.CriticalMessageBox(ex);
                        }

                        this.RefreshButtonIsEnabled = true;
                    });
        }

        private void ExportToFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var filePath = AppDomain.CurrentDomain.BaseDirectory + "account_info.csv";

                File.WriteAllText(
                    filePath,
                    string.Join(Environment.NewLine, this.AccountParameters.Select(x => $"{x.Name}\t{x.Value}")));

                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                ErrorNotify.CriticalMessageBox(ex);
            }
        }
    }
}