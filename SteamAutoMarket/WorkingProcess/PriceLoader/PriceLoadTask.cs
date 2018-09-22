namespace SteamAutoMarket.WorkingProcess.PriceLoader
{
    using System.Threading.Tasks;

    internal class PriceLoadTask
    {
        public ETableToLoad TableToLoad { get; }

        public Task Task { get; }

        public PriceLoadTask(ETableToLoad tableToLoad, Task task)
        {
            this.TableToLoad = tableToLoad;
            this.Task = task;
        }
    }
}