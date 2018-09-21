namespace TelegramShop.Qiwi.QiwiApi.Enumerations
{
    using System;

    /// <summary>
    /// Wallet operatin.
    /// </summary>
    [Flags]
    public enum Operation
    {
        /// <summary>
        /// Income operation.
        /// </summary>
        IN = 1,
        /// <summary>
        /// Outcome operation.
        /// </summary>
        OUT = 2,
        /// <summary>
        /// Qiwi card operations.
        /// </summary>
        QIWI_CARD = 4,
        /// <summary>
        /// All operations.
        /// </summary>
        ALL = IN | OUT | QIWI_CARD
    }
}