using Newtonsoft.Json;

namespace Market.Models.Json
{
    public class JWalletInfo : JSuccess
    {
        [JsonProperty("wallet_currency")]
        public int Currency { get; set; }

        [JsonProperty("wallet_country")]
        public string WalletCountry { get; set; }

        [JsonProperty("wallet_fee")]
        public int WalletFee { get; set; }

        [JsonProperty("wallet_fee_minimum")]
        public int WalletFeeMinimum { get; set; }

        [JsonProperty("wallet_fee_percent")]
        public double WalletFeePercent { get; set; }

        [JsonProperty("wallet_publisher_fee_percent_default")]
        public double WalletPublisherFeePercent { get; set; }

        [JsonProperty("wallet_balance")]
        public int WalletBalance { get; set; }

        [JsonProperty("wallet_trade_max_balance")]
        public int WalletTradeMaxBalance { get; set; }

        [JsonProperty("wallet_max_balance")]
        public int MaxBalance { get; set; }
    }
}