using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamAuth;
using SteamKit2;

namespace autotrade.Interfaces.Steam.TradeOffer {
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
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }

    }
    */
        InventoryRootModel inventoryRoot = new InventoryRootModel();
        public InventoryRootModel GetInventory(SteamID steamid, int appid, int contextid) {
            string url = String.Format(
                "https://steamcommunity.com/inventory/{0}/{1}/{2}",
                //"https://steamcommunity.com/profiles/76561198177211015/inventory/json/730/2",
                steamid.ConvertToUInt64(), appid, contextid
            );
            try {
                string response = SteamWeb.Request(url, "GET", dataString: null);
                inventoryRoot = JsonConvert.DeserializeObject<InventoryRootModel>(response);
                Console.WriteLine(inventoryRoot.success + " " + inventoryRoot.assets);
                return inventoryRoot;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
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

        //Get All Inventory
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
            public bool success { get; set; }
            public List<RgInventory> assets = new List<RgInventory>();
            //public List<object> rgCurrency { get; set; }
            public List<RgDescription> descriptions = new List<RgDescription>();

            public static RgDescription GetDescription(RgInventory asset, List<RgDescription> descriptions) {
                RgDescription description = null;
                try {
                    description = descriptions
                        .First(item =>
                            item.instanceid == asset.instanceid
                            && item.classid == asset.classid);

                } catch (Exception ex) when (ex is ArgumentNullException || ex is InvalidOperationException) {
                    Logger.Error("Description not found");
                }
                return description;
            }
        }

        public class RgFullItem {
            public RgInventory Asset { get; set; }
            public RgDescription Description { get; set; }

            public RgFullItem CloneAsset() {
                var item = new RgFullItem {
                    Asset = new RgInventory {
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
