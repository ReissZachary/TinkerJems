using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinkerJems.Core.Models;

namespace TinkerJems.Wpf.Application.Services
{
    public class JewelryService
    {
        public async Task<IEnumerable<JewelryItem>> GetJewelryItemsAsync()
        {
            var client = new RestClient("https://localhost:5001/api/jewelryitems/");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request).Content;

            IEnumerable<JewelryItem> JewelryItems = JsonConvert.DeserializeObject<IEnumerable<JewelryItem>>(response);

            return JewelryItems;
        }

        public IEnumerable<JewelryItem> GetJewelryByCategory(string category)
        {
            category = category.Substring(0, category.Length - 1);
            var client = new RestClient($"https://localhost:5001/api/jewelryitems/GetJewelryByCategory?category={category}");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request).Content;

            IEnumerable<JewelryItem> JewelryItems = JsonConvert.DeserializeObject<IEnumerable<JewelryItem>>(response);

            return JewelryItems;
        }
    }
}
