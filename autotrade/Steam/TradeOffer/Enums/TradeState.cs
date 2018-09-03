namespace autotrade.Steam.TradeOffer.Enums
{
    public enum TradeState
    {
        TradeStateInit = 0,
        TradeStatePreCommited = 1,
        TradeStateCommited = 2,
        TradeStateComplete = 3,
        TradeStateFailed = 4,
        TradeStatePartialSupportRollback = 5,
        TradeStateFullSupportRollback = 6,
        TradeStateSupportRollbackSelective = 7,
        TradeStateRollbackFailed = 8,
        TradeStateRollbackAbandoned = 9,
        TradeStateInEscrow = 10,
        TradeStateEscrowRollback = 11,
        TradeStateUnknown
    }
}