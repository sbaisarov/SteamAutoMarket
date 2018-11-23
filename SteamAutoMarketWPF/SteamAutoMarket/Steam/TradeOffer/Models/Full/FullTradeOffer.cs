namespace Steam.TradeOffer.Models.Full
{
    using System.Collections.Generic;

    public class FullTradeOffer
    {
        public List<FullTradeItem> MyItems { get; set; }

        public Offer Offer { get; set; }

        public List<FullTradeItem> PartnerItems { get; set; }
    }
}