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
        HttpClient client = new HttpClient();
        List<Inventory> invent = new List<Inventory>();

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
    }
}