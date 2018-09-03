using System.Collections.Generic;
using System.Linq;
using autotrade.Steam.Market.Models.Json;
using Newtonsoft.Json;
using RestSharp;

namespace autotrade.Steam.Market
{
    public class Invertory
    {
        private readonly SteamMarketHandler _steam;

        public Invertory(SteamMarketHandler steam)
        {
            _steam = steam;
        }

        public Dictionary<JInvertoryAsset, JDescription> Get(long userId, int appId, int contextId, int count = 500,
            bool useAuth = false)
        {
            var url = Urls.Invertory + $"{userId}/{appId}/{contextId}";

            var urlQuery = new Dictionary<string, string>
            {
                {"count", count.ToString()}
            };

            var resp = _steam.Request(url, Method.GET, Urls.Invertory, urlQuery, useAuth);
            var respDes = JsonConvert.DeserializeObject<JInvertory>(resp.Data.Content);

            if (respDes.TotalInventoryCount == 0)
                return new Dictionary<JInvertoryAsset, JDescription>();

            var dic = respDes.Assets.ToDictionary(x => x,
                x => respDes.Descriptions.FirstOrDefault(f => f.Classid == x.ClassId));

            return dic;
        }
    }
}