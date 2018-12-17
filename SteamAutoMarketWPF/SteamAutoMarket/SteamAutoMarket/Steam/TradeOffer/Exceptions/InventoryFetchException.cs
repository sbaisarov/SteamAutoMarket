namespace SteamAutoMarket.Steam.TradeOffer.Exceptions
{
    using SteamKit2;

    public class InventoryFetchException : TradeException
    {
        public InventoryFetchException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InventoryFetchException" /> class.
        /// </summary>
        /// <param name='steamId'>
        ///     Steam identifier that caused the fetch exception.
        /// </param>
        public InventoryFetchException(SteamID steamId)
            : base($"Failed to fetch inventory for: {steamId}")
        {
            this.FailingSteamId = steamId;
        }

        /// <summary>
        ///     Gets the Steam identifier that caused the fetch exception.
        /// </summary>
        /// <value>
        ///     The failing steam identifier.
        /// </value>
        public SteamID FailingSteamId { get; }
    }
}