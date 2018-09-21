namespace TelegramShop.Qiwi.QiwiApi.Entities.Balance
{
    using TelegramShop.Qiwi.QiwiApi.Entities.General;

    public class Balance
    {
        public string alias;
        public string fsAlias;
        public string title;
        public bool? hasBalance;
        public BalanceType type;
        public CurrencyAmount balance;
    }
}