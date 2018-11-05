namespace SteamAutoMarket.Models
{
    public class SteamAppId
    {
        public SteamAppId()
        {
        }

        public SteamAppId(long appId, string name, int? contextId = null)
        {
            this.AppId = appId;
            this.Name = name;
            this.ContextId = contextId;
        }

        public SteamAppId(long appId, int? contextIds = null)
            : this(appId, $"{appId}", contextIds)
        {
        }

        public long AppId { get; set; }

        public int? ContextId { get; set; }

        public string Name { get; set; }
    }
}