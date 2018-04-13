using Newtonsoft.Json.Linq;
using OPSkins;
using OPSkins.Model.Inventory;
using autotrade.Interfaces.Steam.TradeOffer;
using SteamAuth;
using SteamKit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static autotrade.Interfaces.Steam.TradeOffer.Inventory;

namespace autotrade
{
    class ApiServices
    {
        //Obskins
        OPSkinsClient opsClient = new OPSkinsClient("0c60904c51f9a9c38a6439da2cad21");
        OPSkins.Interfaces.IInventory obskinsInventoryList;
        //Steam
        Interfaces.Steam.TradeOffer.Inventory inventory = new Interfaces.Steam.TradeOffer.Inventory();


        SteamID steamid = new SteamID(76561198177211015);
        

        //GET obskins all inventorys
        public List<InventorySale> obskinsAllInventory()
        {
            obskinsInventoryList = new OPSkins.Interfaces.IInventory(opsClient);
            return obskinsInventoryList.GetInventory().Items;
        }

        InventoryRootModel invList = new InventoryRootModel();

        public InventoryRootModel steamAllInventory()
        {
            invList = inventory.GetInventory(steamid, 730, 2);
            if ( invList!= null )
            {
                return invList;

            }
            else
            {
                return null;
            }

        }

        /*
        public async Task<List<Inventory>> steamFetchInventory()
        {
            Inventory invList = steamManager.inventory.FetchInventory(76561198313282396, "C708FBA4244D5874370AB7BC57C8FFCE", 730);
            if (invList != null)
            {
                return invList;

            }
            else
            {
                return null;
            }
        }*/
    }
}