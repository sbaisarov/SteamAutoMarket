using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace autotrade
{
    class ApiService
    {
        public string apiUrl = "https://api.opskins.com/{0}/{1}/{2}?key=";
        HttpClient client = new HttpClient();
        List<Inventory> invent = new List<Inventory>();
        List<Prices> prices = new List<Prices>();

        public ApiService(string apiKey = "0c60904c51f9a9c38a6439da2cad21")
        {
            apiUrl += apiKey;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<JArray> SendRequest(string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                JArray jArray = JArray.Parse(res);
                return jArray;
            }

            return null;
        }

        public async Task<List<Inventory>> GetInventoryAll()
        {
            JArray jArray = await SendRequest("http://5a9e4c92b1404b0014cfe19f.mockapi.io/api/GetInventory");
            //HttpResponseMessage response = await client.GetAsync(string.Format(apiUrl, "IInventory", "GetInventory", "v2");
            if (jArray != null)
            {
                invent = JsonConvert.DeserializeObject<List<Inventory>>(jArray[0]["response"]["items"].ToString());
            }
            return null;
        }

        public async Task<List<Prices>> GetLowestPrices()
        {
            JArray jArray = await SendRequest(string.Format(apiUrl, "IPricing", "GetAllLowestPrices", "v1"));
            if (jArray != null)
            {
                prices = JsonConvert.DeserializeObject<List<Prices>>(jArray[0]["response"].ToString());
                return prices;
            }
            return null;
        }

        public async Task<List<Prices>> GetAveragePrices()
        {
            JArray jArray = await SendRequest(string.Format(apiUrl, "IPricing", "GetPriceList", "v2"));
            if (jArray != null)
            {
                prices = JsonConvert.DeserializeObject<List<Prices>>(jArray[0]["response"].ToString());
                return prices;
            }
            return null;
        }
    }
}



//public dynamic GetInventory(string steamid)
//{
//    string url = "http://steamcommunity.com/profiles/" + steamid + "/inventory/json/730/2";
//    dynamic response = JsonConvert.DeserializeObject(Fetch("get", url));
//    if (response != null)
//    {
//        return response;
//    }
//    return null;
//}