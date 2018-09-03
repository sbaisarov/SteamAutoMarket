namespace autotrade.Steam.Market.Interface.Games
{
    public class AvailableGames
    {
        public readonly CounterStrikeGlobalOffensive CounterStrikeGlobalOffensive;

        public AvailableGames(SteamMarketHandler steam)
        {
            CounterStrikeGlobalOffensive = new CounterStrikeGlobalOffensive(steam);
        }
    }
}