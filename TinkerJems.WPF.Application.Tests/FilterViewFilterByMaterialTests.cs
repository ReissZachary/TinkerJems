using Moq;
using NUnit.Framework;
using Prism.Regions;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using TinkerJems.Core.Models;
using TinkerJems.Wpf.Application.Services;
using TinkerJems.Wpf.Application.ViewModels;

namespace TinkerJems.WPF.Application.Tests
{
    public class FilterViewFilterByMaterialTests
    {
        private FilterViewModel _filterViewModel;
        private JewelryService _jewelryService;

        public List<JewelryItem> itemsWithMaterial { get; set; }

        [SetUp]
        public void Setup()
        {
            var regionManagerMock = new Mock<IRegionManager>();
            _filterViewModel = new FilterViewModel(regionManagerMock.Object);
            _jewelryService = new JewelryService();


            List<JewelryItem> mockMaterialItems = new List<JewelryItem>
            {
                    new JewelryItem {Material = "Metal"},
                    new JewelryItem {Material = "Metal"},

                    new JewelryItem {Material = "Silver"},
                    new JewelryItem {Material = "Silver"},
                    new JewelryItem {Material = "Silver"},

                    new JewelryItem {Material = "Gold"},
                    new JewelryItem {Material = "Gold"},

                    new JewelryItem {Material = "Rock"},
                    new JewelryItem {Material = "Rock"},

                    new JewelryItem {Material = "Wood"},
                    new JewelryItem {Material = "Wood"},
                    new JewelryItem {Material = "Wood"},

                    new JewelryItem {Material = "Leather"},
                    new JewelryItem {Material = "Leather"},

                    new JewelryItem {Material = "Plastic"},
                    new JewelryItem {Material = "Plastic"},
                    new JewelryItem {Material = "Plastic"},
            };
            itemsWithMaterial = mockMaterialItems;
        }

        [TestCase("Metal", 2)]
        [TestCase("Silver", 3)]
        [TestCase("Gold", 2)]
        [TestCase("Rock", 2)]
        [TestCase("Wood", 3)]
        [TestCase("Leather", 2)]
        [TestCase("Plastic", 3)]
        public void UserSelectsFilterByMetal_ListWillHaveAllMetalItems(string material, int numOfItems)
        {
            var filteredItems = _filterViewModel.FilterByMaterial(material, itemsWithMaterial);
            Assert.That(filteredItems.Count() == numOfItems);
        }
    }
}
