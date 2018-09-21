namespace TelegramShop.Qiwi
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TelegramShop.Qiwi.QiwiApi.Entities.Payments;
    using TelegramShop.Qiwi.QiwiApi.Enumerations;

    public class QiwiPaymentHistoryHandler
    {
        private readonly string number;

        public QiwiPaymentHistoryHandler(string token, string number)
        {
            QiwiApi.QiwiApi.Initialize(token);
            this.number = number;
        }

        public async Task<List<Payment>> GetIncomingTransactions()
        {
            var response = await QiwiApi.QiwiApi.PaymentHistoryAsync(this.number, Operation.IN);
            return response.data;
        }
    }
}