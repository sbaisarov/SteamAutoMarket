using System.Collections.Generic;

namespace autotrade.Steam.TradeOffer.Models.Full
{
    public class FullTradeOffer
    {
        public Offer Offer { get; set; }
        public List<FullTradeItem> ItemsToGive { get; set; }
        public List<FullTradeItem> ItemsToReceive { get; set; }
    }
}