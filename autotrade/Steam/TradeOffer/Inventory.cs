using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using SteamAuth;
using SteamAutoMarket.Steam.TradeOffer.Exceptions;
using SteamAutoMarket.Steam.TradeOffer.Models;
using SteamAutoMarket.Steam.TradeOffer.Models.Full;
using SteamAutoMarket.Utils;
using SteamKit2;

namespace SteamAutoMarket.Steam.TradeOffer
{
    public class Inventory
    {
        public Inventory()
        {
        }

        protected Inventory(InventoryResult apiInventory)
        {
            NumSlots = apiInventory.NumBackpackSlots;
            Items = apiInventory.Items;
            IsPrivate = apiInventory.Status == "15";
            IsGood = apiInventory.Status == "1";
        }

        public uint NumSlots { get; set; }
        public Item[] Items { get; set; }
        public bool IsPrivate { get; }
        public bool IsGood { get; }

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
                var startAssetid = "";
                do
                {
                    inventoryPage = LoadInventoryPage(steamid, appid, contextid, startAssetid);
                    startAssetid = inventoryPage.LastAssetid;
                    items.AddRange(ProcessInventoryPage(inventoryPage));
                } while (inventoryPage.MoreItems == 1);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return items;
        }

        public List<FullRgItem> GetInventoryWithLogs(SteamID steamid, int appid, int contextid)
        {
            var items = new List<FullRgItem>();
            try
            {
                var startAssetid = "";
                var inventoryPage = LoadInventoryPage(steamid, appid, contextid, startAssetid);
                Program.LoadingForm.SetTotalItemsCount(inventoryPage.TotalInventoryCount,
                    (int)Math.Ceiling((double)inventoryPage.TotalInventoryCount / 5000), "Total items count");

                do
                {
                    inventoryPage = LoadInventoryPage(steamid, appid, contextid, startAssetid);
                    startAssetid = inventoryPage.LastAssetid;
                    items.AddRange(ProcessInventoryPage(inventoryPage));
                    Program.LoadingForm.TrackLoadedIteration("Page {currentPage} of {totalPages} loaded");
                } while (inventoryPage.MoreItems == 1);
            }
            catch (Exception ex)
            {
                if (ex.GetType() != typeof(ThreadAbortException)) Logger.Error("Error on loading inventory", ex);
            }

            return items;
        }

        private InventoryRootModel LoadInventoryPage(SteamID steamid, int appid, int contextid,
            string startAssetid = "", int count = 5000)
        {
            var url = "https://" +
                      $"steamcommunity.com/inventory/{steamid.ConvertToUInt64()}/{appid}/{contextid}?l=english&count={count}&start_assetid={startAssetid}";
            var response = SteamWeb.Request(url, "GET", dataString: null);
            var inventoryRoot = JsonConvert.DeserializeObject<InventoryRootModel>(response);
            return inventoryRoot;
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

        /// <summary>
        ///     Check to see if user is Free to play
        /// </summary>
        /// <returns>Yes or no</returns>
        public bool IsFreeToPlay()
        {
            return NumSlots % 100 == 50;
        }

        public Item GetItem(ulong id)
        {
            // Check for Private Inventory
            if (IsPrivate)
                throw new TradeException("Unable to access Inventory: Inventory is Private!");

            return Items?.FirstOrDefault(item => item.Id == id);
        }

        public List<Item> GetItemsByDefindex(int defindex)
        {
            // Check for Private Inventory
            if (IsPrivate)
                throw new TradeException("Unable to access Inventory: Inventory is Private!");

            return Items.Where(item => item.DefIndex == defindex).ToList();
        }
    }
}