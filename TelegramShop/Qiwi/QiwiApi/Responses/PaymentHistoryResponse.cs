namespace TelegramShop.Qiwi.QiwiApi.Responses
{
    using System;
    using System.Collections.Generic;

    using TelegramShop.Qiwi.QiwiApi.Entities.Payments;

    public class PaymentHistoryResponse
    {
        public List<Payment> data;
        public long? nextTxnId;
        public DateTime? nextTxnDate;
    }
}