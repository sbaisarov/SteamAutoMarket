namespace SteamAutoMarket.Steam.TradeOffer.Models.Full
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class FullTradeOffer
    {
        public List<FullTradeItem> MyItems { get; set; }

        public Offer Offer { get; set; }

        public List<FullTradeItem> PartnerItems { get; set; }
    }
}