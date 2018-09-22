namespace TelegramShop.Qiwi.QiwiApi.Entities.Profile
{
    using TelegramShop.Qiwi.QiwiApi.Enumerations;

    public class UserInfo
    {
        public Currency? defaultPayCurrency;
        public int? defaultPaySource;
        public string email;
        public long? firstTxnId;
        public string language;
        public string @operator;
        public string phoneHash;
        public bool? promoEnabled;
    }
}