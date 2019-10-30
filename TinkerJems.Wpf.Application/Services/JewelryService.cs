﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinkerJems.Core.Models;
using TinkerJems.Web2.Controllers;

namespace TinkerJems.Wpf.Application.Services
{
    class JewelryService
    {
        public async Task<IEnumerable<JewelryItem>> GetJewelryItemsAsync()
        {
            var client = new RestClient("https://localhost:5001/api/jewelryitems/");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request).Content;

            IEnumerable<JewelryItem> JewelryItems = JsonConvert.DeserializeObject<IEnumerable<JewelryItem>>(response);

            return JewelryItems;
        }
    }
}
