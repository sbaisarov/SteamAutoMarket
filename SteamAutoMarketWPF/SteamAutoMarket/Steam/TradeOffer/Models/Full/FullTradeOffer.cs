namespace Steam.TradeOffer.Models.Full
{
    using System.Collections.Generic;

    public class FullTradeOffer
    {
        public List<FullTradeItem> ItemsToGive { get; set; }

        public List<FullTradeItem> ItemsToReceive { get; set; }

        public Offer Offer { get; set; }
    }
}