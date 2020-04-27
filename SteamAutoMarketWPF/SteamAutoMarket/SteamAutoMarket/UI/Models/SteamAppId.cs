namespace SteamAutoMarket.UI.Models
{
    using System;

    [Serializable]
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

        protected bool Equals(SteamAppId other)
        {
            return AppId == other.AppId && ContextId == other.ContextId && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((SteamAppId)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = AppId;
                hashCode = (hashCode * 397) ^ ContextId.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}