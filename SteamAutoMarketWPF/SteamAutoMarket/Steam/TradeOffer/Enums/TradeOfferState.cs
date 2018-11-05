namespace Steam.TradeOffer.Enums
{
    public enum TradeOfferState
    {
        TradeOfferStateInvalid = 1,

        TradeOfferStateActive = 2,

        TradeOfferStateAccepted = 3,

        TradeOfferStateCountered = 4,

        TradeOfferStateExpired = 5,

        TradeOfferStateCanceled = 6,

        TradeOfferStateDeclined = 7,

        TradeOfferStateInvalidItems = 8,

        TradeOfferStateNeedsConfirmation = 9,

        TradeOfferStateCanceledBySecondFactor = 10,

        TradeOfferStateInEscrow = 11,

        TradeOfferStateUnknown
    }
}