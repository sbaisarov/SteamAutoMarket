namespace SteamAutoMarket.Repository.Settings
{
    using System.Collections.Generic;

    using SteamAutoMarket.Models;
    using SteamAutoMarket.Repository.Context;

    public class SettingsModel
    {
        public SteamAppId MarketSellSelectedAppid { get; set; }

        public SteamAppId TradeSelectedAppId { get; set; }

        public List<SteamAppId> AppIdList { get; set; } = UiDefaultValues.AppIdList;

        public string MafilesPath { get; set; }
    }
}