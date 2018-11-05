namespace Steam.Market.Enums
{
    public enum ECreateBuyOrderStatus
    {
        Success,

        Fail,

        LowOrderPrice,

        LimitOfOrders,

        OrderAlreadyPlaced,

        InsufficientFund
    }
}