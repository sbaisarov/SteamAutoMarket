namespace Market.Interface.Games
{
    public class AvailableGames
    {
        public readonly CounterStrikeGlobalOffensive CounterStrikeGlobalOffensive;

        public AvailableGames(Steam steam)
        {
            CounterStrikeGlobalOffensive = new CounterStrikeGlobalOffensive(steam);
        }
    }
}