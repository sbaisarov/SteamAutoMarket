using System.Collections.Generic;
using autotrade.Steam.TradeOffer.Enums;
using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class Offer
    {
        [JsonProperty("tradeofferid")] public string TradeOfferId { get; set; }

        [JsonProperty("accountid_other")] public int AccountIdOther { get; set; }

        [JsonProperty("message")] public string Message { get; set; }

        [JsonProperty("expiration_time")] public int ExpirationTime { get; set; }

        [JsonProperty("trade_offer_state")] public TradeOfferState TradeOfferState { get; set; }

        [JsonProperty("items_to_give")] public List<CEconAsset> ItemsToGive { get; set; }

        [JsonProperty("items_to_receive")] public List<CEconAsset> ItemsToReceive { get; set; }

        [JsonProperty("is_our_offer")] public bool IsOurOffer { get; set; }

        [JsonProperty("time_created")] public int TimeCreated { get; set; }

        [JsonProperty("time_updated")] public int TimeUpdated { get; set; }

        [JsonProperty("from_real_time_trade")] public bool FromRealTimeTrade { get; set; }

        [JsonProperty("escrow_end_date")] public int EscrowEndDate { get; set; }

        [JsonProperty("confirmation_method")] private int ConfirmationMethod { get; set; }

        public TradeOfferConfirmationMethod EConfirmationMethod
        {
            get => (TradeOfferConfirmationMethod) ConfirmationMethod;
            set => ConfirmationMethod = (int) value;
        }
    }
}