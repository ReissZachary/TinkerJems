using Moq;
using NUnit.Framework;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinkerJems.Core.Models;
using TinkerJems.Wpf.Application.Services;
using TinkerJems.Wpf.Application.ViewModels;

namespace TinkerJems.WPF.Application.Tests
{
    public class SearchViewTests
    {
        private SearchViewModel _searchViewModel;
        private JewelryService _jewelryService;

        public List<JewelryItem> items { get; set; }

        [SetUp]
        public void Setup()
        {
            var regionManagerMock = new Mock<IRegionManager>();
            _searchViewModel = new SearchViewModel(regionManagerMock.Object);
            _jewelryService = new JewelryService();

            List<JewelryItem> mockItems = new List<JewelryItem>
            {
                    new JewelryItem {Category = "Ring"},
                    new JewelryItem {Category = "Ring"},
                    new JewelryItem {Category = "Ring"},
                    new JewelryItem {Category = "Ring"},

                    new JewelryItem {Category = "Bracelet"},
                    new JewelryItem {Category = "Bracelet"},
                    new JewelryItem {Category = "Bracelet"},
                    new JewelryItem {Category = "Bracelet"},


                    new JewelryItem {Category = "Earring"},
                    new JewelryItem {Category = "Earring"},
                    new JewelryItem {Category = "Earring"},
                    new JewelryItem {Category = "Earring"},

                    new JewelryItem {Category = "Necklace"},
                    new JewelryItem {Category = "Necklace"},
                    new JewelryItem {Category = "Necklace"},
                    new JewelryItem {Category = "Necklace"}
            };

            items = mockItems;
        }

        [Test]
        public void UserGetsToSearchView_FourElementsGetLoaded()
        {
            _searchViewModel.PopulateRandomJewelryItems(items);

            Assert.That(_searchViewModel.RandomBracelets.Count() == 4);
            Assert.That(_searchViewModel.RandomEarrings.Count() == 4);
            Assert.That(_searchViewModel.RandomNecklaces.Count() == 4);
            Assert.That(_searchViewModel.RandomRings.Count() == 4);

        }

        [Test]
        public async Task UserGetsToSearchView_ItemImagesAreLoadedCorrectly()
        {
            var jewelry = items;
            _searchViewModel.LoadImages(jewelry);
            foreach(var jewel in jewelry)
            {
                Assert.That(jewel.ImageUrl.IsNullOrWhiteSpace() == false); ;
            }
        }
    }
}
