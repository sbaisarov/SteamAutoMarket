using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using autotrade.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamAuth;
using SteamKit2;

namespace autotrade.Steam.TradeOffer {
    public class Inventory {
        public Inventory() { }
        /// <summary>
        /// Fetches the inventory for the given Steam ID using the Steam API.
        /// </summary>
        /// <returns>The give users inventory.</returns>
        /// <param name='steamId'>Steam identifier.</param>
        /// <param name='apiKey'>The needed Steam API key.</param>
        /// <param name="steamWeb">The SteamWeb instance for this Bot</param>
        public Inventory FetchInventory(ulong steamId, string apiKey, int appid) {
            int attempts = 1;
            InventoryResponse result = null;
            while ((result == null || result.result.items == null) && attempts <= 3) {
                var url = $"http://api.steampowered.com/IEconItems_{appid}/GetPlayerItems/v0001/?key={apiKey}&steamid={steamId}";
                string response = SteamWeb.Request(url, "GET", data: null, referer: "http://api.steampowered.com");
                result = JsonConvert.DeserializeObject<InventoryResponse>(response);
                attempts++;
            }
            return new Inventory(result.result);
        }

        #region deprecated
        /// <summary>
        /// Gets the inventory for the given Steam ID using the Steam Community website.
        /// </summary>
        /// <returns>The inventory for the given user. </returns>
        /// <param name='steamid'>The Steam identifier. </param>
        /// <param name="steamWeb">The SteamWeb instance for this Bot</param>
        /// 

        /*
    HttpClient client = new HttpClient();
    public async Task<InventoryRootModel> GetInventory(SteamID steamid, int appid, int contextid)
    {
        string url = String.Format(
            "http://steamcommunity.com/profiles/{0}/inventory/json/{1}/{2}",
            steamid.ConvertToUInt64(), appid, contextid
        );

        HttpResponseMessage response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {

            string res = await response.Content.ReadAsStringAsync();
            InventoryRootModel rootOModel = JsonConvert.DeserializeObject<InventoryRootModel>(res);


            //invent = JsonConvert.DeserializeObject<List<Inventory>>(res);

            return rootOModel;

        }
        else
        {
            return null;
        }

        try
        {
            string response =  SteamWeb.Request(url, "GET", dataString: null);
            return JsonConvert.DeserializeObject<InventoryRootOModel>(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }

    }
    */
        #endregion



        public List<RgFullItem> GetInventory(SteamID steamid, int appid, int contextid) {
            var items = new List<RgFullItem>();
            try {
                InventoryRootModel inventoryPage;
                var startAssetid = "";
                do {
                    inventoryPage = LoadInventoryPage(steamid, appid, contextid, startAssetid);
                    startAssetid = inventoryPage.last_assetid;
                    items.AddRange(ProcessInventoryPage(inventoryPage));
                }
                while (inventoryPage.more_items == 1);
            } catch (Exception ex) {
                Logger.Error(ex.Message, ex);
            }
            return items;
        }

        public List<RgFullItem> GetInventoryWithLogs(SteamID steamid, int appid, int contextid) {
            var items = new List<RgFullItem>();
            try {
                var startAssetid = "";
                InventoryRootModel inventoryPage = LoadInventoryPage(steamid, appid, contextid, startAssetid);
                Program.LoadingForm.SetTotalItemsCount(inventoryPage.total_inventory_count, (int)Math.Ceiling((double)inventoryPage.total_inventory_count / 5000), "Total items count");

                do {
                    inventoryPage = LoadInventoryPage(steamid, appid, contextid, startAssetid);
                    startAssetid = inventoryPage.last_assetid;
                    items.AddRange(ProcessInventoryPage(inventoryPage));
                    Program.LoadingForm.TrackLoadedIteration("Page {currentPage} of {totalPages} loaded");
                }
                while (inventoryPage.more_items == 1);
            } catch (Exception ex) {
                if (ex.GetType() != typeof(ThreadAbortException)) {
                    Logger.Error("Error on loading inventory", ex);
                }
            }
            return items;
        }

        private InventoryRootModel LoadInventoryPage(SteamID steamid, int appid, int contextid, string startAssetid = "", int count = 5000) {
            string url = "https://" + $"steamcommunity.com/inventory/{steamid.ConvertToUInt64()}/{appid}/{contextid}?l=english&count=5000&start_assetid={startAssetid}";
            string response = SteamWeb.Request(url, "GET", dataString: null);
            InventoryRootModel inventoryRoot = JsonConvert.DeserializeObject<InventoryRootModel>(response);
            return inventoryRoot;
        }

        private static List<RgFullItem> ProcessInventoryPage(InventoryRootModel inventoryRoot) {
            List<RgFullItem> result = new List<RgFullItem>();

            foreach (RgInventory item in inventoryRoot.assets) {
                RgFullItem resultItem = new RgFullItem();
                RgDescription description = InventoryRootModel.GetDescription(item, inventoryRoot.descriptions);
                resultItem.Asset = item;
                resultItem.Description = description;
                result.Add(resultItem);
            }
            return result;
        }

        public uint NumSlots { get; set; }
        public Item[] Items { get; set; }
        public bool IsPrivate { get; private set; }
        public bool IsGood { get; private set; }

        protected Inventory(InventoryResult apiInventory) {
            NumSlots = apiInventory.num_backpack_slots;
            Items = apiInventory.items;
            IsPrivate = (apiInventory.status == "15");
            IsGood = (apiInventory.status == "1");
        }

        /// <summary>
        /// Check to see if user is Free to play
        /// </summary>
        /// <returns>Yes or no</returns>
        public bool IsFreeToPlay() {
            return this.NumSlots % 100 == 50;
        }

        public Item GetItem(ulong id) {
            // Check for Private Inventory
            if (this.IsPrivate)
                throw new Exceptions.TradeException("Unable to access Inventory: Inventory is Private!");

            return (Items?.FirstOrDefault(item => item.Id == id));
        }

        public List<Item> GetItemsByDefindex(int defindex) {
            // Check for Private Inventory
            if (this.IsPrivate)
                throw new Exceptions.TradeException("Unable to access Inventory: Inventory is Private!");

            return Items.Where(item => item.Defindex == defindex).ToList();
        }

        public class Item {
            public int AppId = 440;
            public long ContextId = 2;

            [JsonProperty("id")]
            public ulong Id { get; set; }

            [JsonProperty("original_id")]
            public ulong OriginalId { get; set; }

            [JsonProperty("defindex")]
            public ushort Defindex { get; set; }

            [JsonProperty("level")]
            public byte Level { get; set; }

            [JsonProperty("quality")]
            public int Quality { get; set; }

            [JsonProperty("quantity")]
            public int RemainingUses { get; set; }

            [JsonProperty("origin")]
            public int Origin { get; set; }

            [JsonProperty("custom_name")]
            public string CustomName { get; set; }

            [JsonProperty("custom_desc")]
            public string CustomDescription { get; set; }

            [JsonProperty("flag_cannot_craft")]
            public bool IsNotCraftable { get; set; }

            [JsonProperty("flag_cannot_trade")]
            public bool IsNotTradeable { get; set; }

            [JsonProperty("attributes")]
            public ItemAttribute[] Attributes { get; set; }

            [JsonProperty("contained_item")]
            public Item ContainedItem { get; set; }
        }

        public class ItemAttribute {
            [JsonProperty("defindex")]
            public ushort Defindex { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("float_value")]
            public float FloatValue { get; set; }

            [JsonProperty("account_info")]
            public AccountInfo AccountInfo { get; set; }
        }

        public class AccountInfo {
            [JsonProperty("steamid")]
            public ulong SteamID { get; set; }

            [JsonProperty("personaname")]
            public string PersonaName { get; set; }
        }

        protected class InventoryResult {
            public string status { get; set; }

            public uint num_backpack_slots { get; set; }

            public Item[] items { get; set; }
        }

        protected class InventoryResponse {
            public InventoryResult result;
        }

        public partial class RgInventory {
            public int appid { get; set; }
            public string classid { get; set; }
            public string instanceid { get; set; }
            public string amount { get; set; }
            public string contextid { get; set; }
            public string assetid { get; set; }
        }

        public class AppData {
            public int limited { get; set; }
            public int def_index { get; set; }
            public int? is_itemset_name { get; set; }
        }

        public class Description {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class Tag {
            public string internal_name { get; set; }
            public string category { get; set; }
            public string localized_tag_name { get; set; }
            public string localized_category_name { get; set; }
        }

        public class RgDescription {
            public int appid { get; set; }
            public string classid { get; set; }
            public string instanceid { get; set; }
            public string icon_url { get; set; }
            public string icon_url_large { get; set; }
            public string icon_drag_url { get; set; }
            public string name { get; set; }
            public string market_hash_name { get; set; }
            public string market_name { get; set; }
            public string name_color { get; set; }
            public string background_color { get; set; }
            public string type { get; set; }
            public bool tradable { get; set; }
            public bool commodity { get; set; }
            public bool marketable { get; set; }
            public int market_tradable_restriction { get; set; }
            public List<Description> descriptions { get; set; }
            public List<Tag> tags { get; set; }
        }

        public class InventoryRootModel {
            public List<RgInventory> assets { get; set; }
            public List<RgDescription> descriptions { get; set; }
            public int more_items { get; set; }
            public string last_assetid { get; set; }
            public int total_inventory_count { get; set; }
            public int success { get; set; }
            public int rwgrsn { get; set; }

            public static RgDescription GetDescription(RgInventory asset, List<RgDescription> descriptions) {
                RgDescription description = null;
                try {
                    description = descriptions
                        .First(item => (asset.instanceid == item.instanceid && asset.classid == item.classid));

                } catch (Exception ex) when (ex is ArgumentNullException || ex is InvalidOperationException) { }
                return description;
            }
        }

        public class RgFullItem {
            public RgInventory Asset { get; set; }
            public RgDescription Description { get; set; }

            public RgFullItem CloneAsset() {
                var item = new RgFullItem
                {
                    Asset = new RgInventory
                    {
                        amount = Asset.amount,
                        appid = Asset.appid,
                        assetid = Asset.assetid,
                        classid = Asset.classid,
                        contextid = Asset.contextid,
                        instanceid = Asset.instanceid
                    },
                    Description = Description
                };
                return item;
            }
        }
    }
}
