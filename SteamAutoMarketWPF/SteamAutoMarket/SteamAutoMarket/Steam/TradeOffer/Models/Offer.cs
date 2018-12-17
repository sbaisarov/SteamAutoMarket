namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using SteamAutoMarket.Steam.TradeOffer.Enums;

    [Serializable]
    public class Offer
    {
        [JsonProperty("accountid_other")]
        public int AccountIdOther { get; set; }

        public TradeOfferConfirmationMethod EConfirmationMethod
        {
            get => (TradeOfferConfirmationMethod)this.ConfirmationMethod;
            set => this.ConfirmationMethod = (int)value;
        }

        [JsonProperty("escrow_end_date")]
        public int EscrowEndDate { get; set; }

        [JsonProperty("expiration_time")]
        public int ExpirationTime { get; set; }

        [JsonProperty("from_real_time_trade")]
        public bool FromRealTimeTrade { get; set; }

        [JsonProperty("is_our_offer")]
        public bool IsOurOffer { get; set; }

        [JsonProperty("items_to_give")]
        public List<CEconAsset> ItemsToGive { get; set; }

        [JsonProperty("items_to_receive")]
        public List<CEconAsset> ItemsToReceive { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("time_created")]
        public int TimeCreated { get; set; }

        [JsonProperty("time_updated")]
        public int TimeUpdated { get; set; }

        [JsonProperty("tradeofferid")]
        public string TradeOfferId { get; set; }

        [JsonProperty("trade_offer_state")]
        public TradeOfferState TradeOfferState { get; set; }

        [JsonProperty("confirmation_method")]
        private int ConfirmationMethod { get; set; }
    }
}