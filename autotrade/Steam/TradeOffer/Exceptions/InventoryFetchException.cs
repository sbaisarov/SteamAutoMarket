using SteamKit2;

namespace autotrade.Steam.TradeOffer.Exceptions
{
    public class InventoryFetchException : TradeException
    {
        public InventoryFetchException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SteamTrade.Exceptions.InventoryFetchException" /> class.
        /// </summary>
        /// <param name='steamId'>
        ///     Steam identifier that caused the fetch exception.
        /// </param>
        public InventoryFetchException(SteamID steamId)
            : base($"Failed to fetch inventory for: {steamId}")
        {
            FailingSteamId = steamId;
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