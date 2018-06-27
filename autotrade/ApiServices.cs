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
        //Opskins
        OPSkinsClient opsClient = new OPSkinsClient("0c60904c51f9a9c38a6439da2cad21");
        OPSkins.Interfaces.IInventory opskinsInventoryList;
        //Steam
        SteamManager steamManager = new SteamManager();
        SteamID steamid = new SteamID(76561198074672128);
        Steam.TradeOffer.Inventory.InventoryRootModel steamInv;

        public ApiServices()
        {
            
        }

        public List<InventorySale> OpskinsAllInventory()
        {
            opskinsInventoryList = new OPSkins.Interfaces.IInventory(opsClient);
            return opskinsInventoryList.GetInventory().Items;
        }

        public Steam.TradeOffer.Inventory.InventoryRootModel SteamAllInventory()
        {
            steamInv = steamManager.Inventory.GetInventory(steamid, 753, 6);
            return steamInv;

        }
    }
}