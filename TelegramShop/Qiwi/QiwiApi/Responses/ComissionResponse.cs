namespace TelegramShop.Qiwi.QiwiApi.Responses
{
    using System.Collections.Generic;

    using TelegramShop.Qiwi.QiwiApi.Entities.General;

    public class CommissionResponse
    {
        public CommissionResponseContent content;
    }

    public class CommissionResponseContent
    {
        public CommissionResponseTerms terms;
    }

    public class CommissionResponseTerms
    {
        public CommissionResponseComission commission;
    }

    public class CommissionResponseComission
    {
        public List<CommissionRange> ranges;
    }
}