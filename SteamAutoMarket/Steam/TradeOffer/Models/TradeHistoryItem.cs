using System.Collections.Generic;
using Newtonsoft.Json;
using SteamAutoMarket.Steam.TradeOffer.Enums;

namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    public class TradeHistoryItem
    {
        [JsonProperty("tradeid")] public string TradeId { get; set; }

        [JsonProperty("steamid_other")] public string SteamIdOther { get; set; }

        [JsonProperty("time_init")] public string TimeInit { get; set; }

        [JsonProperty("time_escrow_end")] public string TimeEscrowEnd { get; set; }

        [JsonProperty("status")] public TradeState Status { get; set; }

        [JsonProperty("assets_received")] public List<TradedAsset> AssetsReceived { get; set; }

        [JsonProperty("assets_given")] public List<TradedAsset> AssetsGiven { get; set; }

        [JsonProperty("currency_received")] public List<TradedCurrency> CurrencyReceived { get; set; }

        [JsonProperty("currency_given")] public List<TradedCurrency> CurrencyGiven { get; set; }
    }
}