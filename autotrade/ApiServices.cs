using Newtonsoft.Json.Linq;
using OPSkins;
using OPSkins.Model.Inventory;
using SteamAuth;
using SteamKit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using autotrade.Steam;

namespace autotrade
{
    class ApiServices
    {
        //Obskins
        OPSkinsClient opsClient = new OPSkinsClient("0c60904c51f9a9c38a6439da2cad21");
        OPSkins.Interfaces.IInventory obskinsInventoryList;
        //Steam
        SteamManager steamManager = new SteamManager();
        SteamID steamid = new SteamID(76561198074672128);
        Steam.TradeOffer.Inventory.InventoryRootModel steamInv;

        public ApiServices()
        {
            
        }

        //GET obskins all inventorys
        public List<InventorySale> ObskinsAllInventory()
        {
            obskinsInventoryList = new OPSkins.Interfaces.IInventory(opsClient);
            return obskinsInventoryList.GetInventory().Items;
        }

        public Steam.TradeOffer.Inventory.InventoryRootModel SteamAllInventory()
        {
            steamInv = steamManager.inventory.GetInventory(steamid, 753, 6);
            if (steamInv != null )
            {
                return steamInv;

            }
            else
            {
                return null;
            }

        }
    }
}