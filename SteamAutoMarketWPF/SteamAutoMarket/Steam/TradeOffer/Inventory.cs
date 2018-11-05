namespace Steam.TradeOffer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Newtonsoft.Json;

    using Steam.SteamAuth;
    using Steam.TradeOffer.Exceptions;
    using Steam.TradeOffer.Models;
    using Steam.TradeOffer.Models.Full;

    using SteamKit2;

    public class Inventory
    {
        public Inventory()
        {
        }

        protected Inventory(InventoryResult apiInventory)
        {
            this.NumSlots = apiInventory.NumBackpackSlots;
            this.Items = apiInventory.Items;
            this.IsPrivate = apiInventory.Status == "15";
            this.IsGood = apiInventory.Status == "1";
        }

        public bool IsGood { get; }

        public bool IsPrivate { get; }

        public Item[] Items { get; set; }

        public uint NumSlots { get; set; }

        /// <summary>
        ///     Fetches the inventory for the given Steam ID using the Steam API.
        /// </summary>
        /// <returns>The give users inventory.</returns>
        /// <param name='steamId'>Steam identifier.</param>
        /// <param name='apiKey'>The needed Steam API key.</param>
        /// <param name="appid"></param>
        public Inventory FetchInventory(ulong steamId, string apiKey, int appid)
        {
            var attempts = 1;
            InventoryResponse result = null;
            while ((result?.result.Items == null) && attempts <= 3)
            {
                var url =
                    $"http://api.steampowered.com/IEconItems_{appid}/GetPlayerItems/v0001/?key={apiKey}&steamid={steamId}";
                var response = SteamWeb.Request(url, "GET", data: null, referer: "http://api.steampowered.com");
                result = JsonConvert.DeserializeObject<InventoryResponse>(response);
                attempts++;
            }

            return new Inventory(result?.result);
        }

        public List<FullRgItem> GetInventory(SteamID steamid, int appid, int contextid)
        {
            var items = new List<FullRgItem>();
            try
            {
                InventoryRootModel inventoryPage;
                var startAssetid = string.Empty;
                do
                {
                    inventoryPage = this.LoadInventoryPage(steamid, appid, contextid, startAssetid);
                    startAssetid = inventoryPage.LastAssetid;
                    items.AddRange(ProcessInventoryPage(inventoryPage));
                }
                while (inventoryPage.MoreItems == 1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return items;
        }

        public Item GetItem(ulong id)
        {
            // Check for Private Inventory
            if (this.IsPrivate)
                throw new TradeException("Unable to access Inventory: Inventory is Private!");

            return this.Items?.FirstOrDefault(item => item.Id == id);
        }

        public List<Item> GetItemsByDefindex(int defindex)
        {
            // Check for Private Inventory
            if (this.IsPrivate)
                throw new TradeException("Unable to access Inventory: Inventory is Private!");

            return this.Items.Where(item => item.DefIndex == defindex).ToList();
        }

        /// <summary>
        ///     Check to see if user is Free to play
        /// </summary>
        /// <returns>Yes or no</returns>
        public bool IsFreeToPlay()
        {
            return this.NumSlots % 100 == 50;
        }

        private static List<FullRgItem> ProcessInventoryPage(InventoryRootModel inventoryRoot)
        {
            var result = new List<FullRgItem>();

            foreach (var item in inventoryRoot.Assets)
            {
                var resultItem = new FullRgItem();
                var description = InventoryRootModel.GetDescription(item, inventoryRoot.Descriptions);
                resultItem.Asset = item;
                resultItem.Description = description;
                result.Add(resultItem);
            }

            return result;
        }

        private InventoryRootModel LoadInventoryPage(
            SteamID steamid,
            int appid,
            int contextid,
            string startAssetid = "",
            int count = 5000)
        {
            var url = "https://"
                      + $"steamcommunity.com/inventory/{steamid.ConvertToUInt64()}/{appid}/{contextid}?l=english&count={count}&start_assetid={startAssetid}";
            var response = SteamWeb.Request(url, "GET", dataString: null);
            var inventoryRoot = JsonConvert.DeserializeObject<InventoryRootModel>(response);
            return inventoryRoot;
        }
    }
}