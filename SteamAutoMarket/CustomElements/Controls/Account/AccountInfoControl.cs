namespace SteamAutoMarket.CustomElements.Controls.Account
{
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using SteamAutoMarket.Steam.Market.Exceptions;
    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess;
    using SteamAutoMarket.WorkingProcess.Settings;
    using SteamKit2;

    public partial class AccountInfoControl : UserControl
    {
        public AccountInfoControl()
        {
            this.InitializeComponent();
        }

        public void AuthCurrentAccount()
        {
            this.InfoGridView.Rows.Clear();

            Task.Run(
                () =>
                {
                    Dispatcher.AsMainForm(
                        () => { this.LoginLable.Text = CurrentSession.SteamManager.Guard.AccountName; });

                    var avatar =
                        ImageUtils.GetSteamProfileBigImage(CurrentSession.SteamManager.Guard.Session.SteamID);
                    if (avatar != null)
                    {
                        avatar = ImageUtils.ResizeImage(avatar, 184, 184);
                        this.AvatarImageBox.BackgroundImage = avatar;
                    }

                    var steamId = new SteamID(
                        ulong.Parse(CurrentSession.SteamManager.Guard.Session.SteamID.ToString()));
                    this.AddInfoTableRow("SteamId-64", steamId.ConvertToUInt64().ToString());
                    this.AddInfoTableRow("AccountId", steamId.AccountID.ToString());
                    this.AddInfoTableRow(
                        "TradeToken",
                        SavedSteamAccount.Get().FirstOrDefault(a => a.Login == this.LoginLable.Text)?.TradeToken);

                    try
                    {
                        var walletInfo = CurrentSession.SteamManager.MarketClient.WalletInfo();
                        if (walletInfo != null)
                        {
                            this.AddInfoTableRow("Wallet country", walletInfo.WalletCountry);
                            this.AddInfoTableRow(
                                "Currency",
                                CurrentSession.SteamManager.MarketClient.Currencies[walletInfo.Currency
                                    .ToString()]);
                            this.AddInfoTableRow(
                                "Max balance",
                                walletInfo.MaxBalance.ToString(CultureInfo.InvariantCulture));
                            this.AddInfoTableRow(
                                "Wallet balance",
                                walletInfo.WalletBalance.ToString(CultureInfo.InvariantCulture));
                            this.AddInfoTableRow(
                                "Wallet fee",
                                walletInfo.WalletFee.ToString(CultureInfo.InvariantCulture));
                            this.AddInfoTableRow(
                                "Wallet fee minimum",
                                walletInfo.WalletFeeMinimum.ToString(CultureInfo.InvariantCulture));
                            this.AddInfoTableRow("Wallet fee percent", walletInfo.WalletFeePercent.ToString());
                            this.AddInfoTableRow(
                                "Wallet trade max balance",
                                walletInfo.WalletTradeMaxBalance.ToString(CultureInfo.InvariantCulture));
                        }
                    }
                    catch (SteamException e)
                    {
                        Logger.Error(string.Empty, e);
                    }
                });
        }

        public void AddInfoTableRow(string param, string value)
        {
            Dispatcher.AsMainForm(() => { this.InfoGridView.Rows.Add(param, value); });
        }
    }
}