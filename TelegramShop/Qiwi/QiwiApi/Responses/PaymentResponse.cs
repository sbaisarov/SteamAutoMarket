namespace TelegramShop.Qiwi.QiwiApi.Responses
{
    using System.Collections.Generic;

    using TelegramShop.Qiwi.QiwiApi.Entities.General;
    using TelegramShop.Qiwi.QiwiApi.Entities.Transaction;

    public class PaymentResponse
    {
        public string id;
        public CurrencyAmount sum;
        public Dictionary<string, string> fields;
        public string source;
        public Transaction transaction;
    }
}