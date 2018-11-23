namespace SteamAutoMarket.Models
{
    using System.Linq;

    using SteamAutoMarket.Repository.Settings;

    using SteamKit2;

    public class SteamAccountHyperlinkModel
    {
        public SteamAccountHyperlinkModel(int accountId)
        {
            var steamId = new SteamID((uint)accountId, EUniverse.Public, EAccountType.Individual).ConvertToUInt64();

            this.AccountName =
                SettingsProvider.GetInstance().SteamAccounts.FirstOrDefault(a => a.SteamId == steamId)?.Login
                ?? steamId.ToString();

            this.AccountLink = $"https://steamcommunity.com/profiles/{steamId}/";
        }

        public string AccountLink { get; }

        public string AccountName { get; }
    }
}