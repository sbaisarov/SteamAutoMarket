namespace SteamAutoMarket.Models
{
    public class SteamAppId
    {
        public SteamAppId()
        {
        }

        public SteamAppId(int appId, string name, int? contextId = null)
        {
            this.AppId = appId;
            this.Name = name;
            this.ContextId = contextId;
        }

        public SteamAppId(int appId, int? contextIds = null)
            : this(appId, $"{appId}", contextIds)
        {
        }

        public int AppId { get; set; }

        public int? ContextId { get; set; }

        public string Name { get; set; }
    }
}