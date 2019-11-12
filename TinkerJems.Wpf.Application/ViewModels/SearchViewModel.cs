using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinkerJems.Core.Models;
using TinkerJems.Wpf.Application.Services;

namespace TinkerJems.Wpf.Application.ViewModels
{
    public class SearchViewModel : BindableBase
    {
        public SearchViewModel()
        {
            _jewelryService = new JewelryService();
            _ = populateJewelry();
            _ = PopulateRandomJewleryItems();
        }

        private readonly JewelryService _jewelryService;

        private IEnumerable<JewelryItem> jewelryItems;

        public IEnumerable<JewelryItem> JewelryItems
        {
            get { return jewelryItems; }
            set { jewelryItems = value; }
        }

        private IEnumerable<JewelryItem> randomItems;

        public IEnumerable<JewelryItem> RandomItems
        {
            get { return randomItems; }
            set { randomItems = value; }
        }

        private async Task PopulateRandomJewleryItems()
        {

        }


        private async Task populateJewelry()
        {
            var rnd = new Random();
            JewelryItems = await _jewelryService.GetJewelryItemsAsync();
            RandomItems = jewelryItems.OrderBy(i => rnd.Next()).Take(4);
            foreach (var j in JewelryItems)
                j.ImageUrl = $"https://localhost:5001/images/{j.ImageUrl}";
        }
    }
}
