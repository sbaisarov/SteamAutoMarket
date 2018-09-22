namespace TelegramShop.Qiwi.QiwiApi.Entities.Payments
{
    using System;

    using TelegramShop.Qiwi.QiwiApi.Entities.General;
    using TelegramShop.Qiwi.QiwiApi.Enumerations;

    public class Payment
    {
        public long? txnId;
        public long? personId;
        public DateTime? date;
        public int? errorCode;
        public string error;
        public PaymentStatus? status;
        public string type;
        public string statusText;
        public string trmTxnId;
        public string account;
        public CurrencyAmount sum;
        public CurrencyAmount comission;
        public CurrencyAmount total;
        public PaymentProvider provider;
        public string comment;
        public decimal? currencyRate;
        public object extras;
        public bool? chequeReady;
        public bool? bankDocumentAvailable;
        public bool? bankDocumentReady;
        public bool? repeatPaymentEnabled;
    }
}