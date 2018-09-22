namespace TelegramShop.Qiwi.QiwiApi.Responses
{
    using System.Collections.Generic;

    using TelegramShop.Qiwi.QiwiApi.Entities.General;

    public class PaymentStatisticsResponse
    {
        public List<CurrencyAmount> incomingTotal;
        public List<CurrencyAmount> outgoingTotal;
    }
}