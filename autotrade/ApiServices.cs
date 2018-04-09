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

namespace autotrade
{
    class ApiServices
    {
        //Obskins
        OPSkinsClient opsClient = new OPSkinsClient("0c60904c51f9a9c38a6439da2cad21");
        OPSkins.Interfaces.IInventory obskinsInventoryList;
        //Steam
        UserLogin steamClient = new UserLogin("", "");
        SteamGuardAccount guard = new SteamGuardAccount();

        //guard = JsonConvert.DeserializeObject<SteamGuardAccount>(File.ReadAllText(@""));
        //steamClient.TwoFactorCode = guard.GenerateSteamGuardCode();
        //steamClient.DoLogin();

        SteamID steamid = new SteamID(76561198177211015);
        

        //GET obskins all inventorys
        public List<InventorySale> obskinsAllInventory()
        {
            obskinsInventoryList = new OPSkins.Interfaces.IInventory(opsClient);
            return obskinsInventoryList.GetInventory().Items;
        }

        public async Task<List<InventorySale>> steamAllInventory()
        {
            List<InventorySale> invList = await Interfaces.Steam.Inventory.GetInventory(steamid);
            if ( invList!= null )
            {
                return invList;

            }
            else
            {
                return null;
            }

        }
    }
}
