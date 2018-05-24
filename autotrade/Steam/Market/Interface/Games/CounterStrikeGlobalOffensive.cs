using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Market.Enums;
using Market.Exceptions;
using Market.Models;
using Market.Models.Json;

namespace Market.Interface.Games
{
    public class CounterStrikeGlobalOffensive
    {
        private readonly Steam _steam;

        public CounterStrikeGlobalOffensive(Steam steam)
        {
            _steam = steam;
        }

        public List<MarketSearchItem> Cases()
        {
            var tag = new Dictionary<string, string>
            {
                {"category_730_Type[]", "tag_CSGO_Type_WeaponCase" }
            };

            var search = _steam.Client.Search(count: 100, appId: AppIds.CounterStrikeGlobalOffensive, sortColumn: EMarketSearchSortColumns.Quantity, custom: tag);

            if (!search.Items.Any())
                throw new SteamException("Not found any cases");

            var list = new List<MarketSearchItem>(search.Items);

            if (search.Items.Count >= search.TotalCount) return list;

            int tempCount = search.Items.Count;

            while (tempCount < search.TotalCount)
            {
                var searchPlus = _steam.Client.Search(count: 100, appId: AppIds.CounterStrikeGlobalOffensive, sortColumn: EMarketSearchSortColumns.Quantity, custom: tag, start: tempCount);
                list.AddRange(searchPlus.Items);
                tempCount = tempCount + searchPlus.Items.Count;
            }

            return list;
        }

        public JMarketAppFilterCsgoFacets Tags()
        {
            var resp = _steam.Client.AppFilters(AppIds.CounterStrikeGlobalOffensive);
            var respDes = JsonConvert.DeserializeObject<JMarketAppFilter<JMarketAppFilterCsgoFacets>>(resp);
            return respDes.Facets;
        }

        public Task<List<MarketSearchItem>> CaseAsync(string collectionTag, bool getAll = true)
        {
            var result = new Task<List<MarketSearchItem>>(() => Case(collectionTag, getAll));
            result.Start();
            return result;
        }

        public Task<List<MarketSearchItem>> StickersAsync(string collectionTag, bool getAll = true)
        {
            var result = new Task<List<MarketSearchItem>>(() => Stickers(collectionTag, getAll));
            result.Start();
            return result;
        }

        public List<MarketSearchItem> Case(string collectionTag, bool getAll = true)
        {
            var tag = new KeyValuePair<string, string>("category_730_ItemSet[]", "tag_" + collectionTag);
            return GetCollection(tag, getAll);
        }

        public List<MarketSearchItem> Stickers(string collectionTag, bool getAll = true)
        {
            var tag = new KeyValuePair<string, string>("category_730_StickerCapsule[]", "tag_" + collectionTag);
            return GetCollection(tag, getAll);
        }

        private List<MarketSearchItem> GetCollection(KeyValuePair<string, string> tagPair, bool getAll = true)
        {
            if (string.IsNullOrEmpty(tagPair.Value))
                throw new SteamException("Collection tag should not be empty");
            if (string.IsNullOrEmpty(tagPair.Key))
                throw new SteamException("Collection key should not be empty");

            var tag = new Dictionary<string, string> { { tagPair.Key, tagPair.Value } };

            var search = _steam.Client.Search(count: 100, appId: AppIds.CounterStrikeGlobalOffensive, sortColumn: EMarketSearchSortColumns.Quantity, custom: tag);

            if (!search.Items.Any())
                throw new SteamException("Not found items. Wrong collection tag?");

            var list = new List<MarketSearchItem>(search.Items);

            if (search.Items.Count >= search.TotalCount) return list;

            if (getAll)
            {
                int tempCount = search.Items.Count;

                while (tempCount < search.TotalCount)
                {
                    var searchPlus = _steam.Client.Search(count: 100, appId: AppIds.CounterStrikeGlobalOffensive, sortColumn: EMarketSearchSortColumns.Quantity, custom: tag, start: tempCount);
                    list.AddRange(searchPlus.Items);
                    tempCount = tempCount + searchPlus.Items.Count;
                }
            }

            return list;
        }
    }
}