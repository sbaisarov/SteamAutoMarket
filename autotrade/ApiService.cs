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
        const string API_URL = "https://api.opskins.com/{0}/{1}/v{2}?key={3}";
        HttpClient client = new HttpClient();
        List<Inventory> invent = new List<Inventory>();
        string apiKey;

        public ApiService()
        {

        }

        public ApiService(string apiKey)
        {
            this.apiKey = apiKey;
        }


        public async Task<List<Inventory>> getInventoryAll()
        {
            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("http://5a9e4c92b1404b0014cfe19f.mockapi.io/api/GetInventory");
            //HttpResponseMessage response = await client.GetAsync("https://api.opskins.com/IInventory/GetInventory/v2/?key=0c60904c51f9a9c38a6439da2cad21");
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                JArray jArray = JArray.Parse(res);
                IList<JToken> jToken = jArray[0]["response"]["items"].ToList();


                //invent = JsonConvert.DeserializeObject<List<Inventory>>(res);
                
                foreach (JToken result in jToken)
                {
                    Inventory item = result.ToObject<Inventory>();
                    invent.Add(item);
                }
                Console.WriteLine(invent[0].id);
                return invent;

            }
            else
            {
                return null;
            }

        }

        //public async Task<List<Prices>> getPrices(int inverval)
        //{
            //HttpResponseMessage response = await client.GetAsync("http://5a9e4c92b1404b0014cfe19f.mockapi.io/api/GetInventory");
        //}
    }
}