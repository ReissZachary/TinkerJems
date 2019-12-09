using Moq;
using NUnit.Framework;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinkerJems.Core.Models;
using TinkerJems.Wpf.Application.Services;
using TinkerJems.Wpf.Application.ViewModels;

namespace TinkerJems.WPF.Application.Tests
{
    public class FilterViewSortByTests
    {
        private FilterViewModel _filterViewModel;
        private JewelryService _jewelryService;

        public List<JewelryItem> items { get; set; }

        [SetUp]
        public void Setup()
        {
            var regionManagerMock = new Mock<IRegionManager>();
            _filterViewModel = new FilterViewModel(regionManagerMock.Object);
            _jewelryService = new JewelryService();

            List<JewelryItem> mockItems = new List<JewelryItem>
            {
                    new JewelryItem {Price = 1.00M, Name = "A"},
                    new JewelryItem {Price = 2.00M, Name = "B"},
                    new JewelryItem {Price = 3.00M, Name = "C"},
                    new JewelryItem {Price = 4.00M, Name = "D"}
            };

            items = mockItems;
        }

        [Test]
        public void UserSelectSortByNameAscending_ListIsSortedInAscending()
        {
            var sort = "Name (A-Z)";
            var sortedItems = _filterViewModel.sortItems(sort, items);

            Assert.That(sortedItems.First().Name == "A");
            Assert.That(sortedItems.Last().Name == "D");
        }

        [Test]
        public void UserSelectSortByNameDescending_ListIsSortedInAscending()
        {
            var sort = "Name (Z-A)";
            var sortedItems = _filterViewModel.sortItems(sort, items);

            Assert.That(sortedItems.First().Name == "D");
            Assert.That(sortedItems.Last().Name == "A");
        }

        [Test]
        public void UserSelectSortByPriceAscending_ListIsSortedInAscending()
        {
            var sort = "Price (Low-High)";
            var sortedItems = _filterViewModel.sortItems(sort, items);

            Assert.That(sortedItems.First().Price == 1.00M);
            Assert.That(sortedItems.Last().Price == 4.00M);
        }

        [Test]
        public void UserSelectSortByPriceDescending_ListIsSortedInAscending()
        {
            var sort = "Price (High-Low)";
            var sortedItems = _filterViewModel.sortItems(sort, items);

            Assert.That(sortedItems.First().Price == 4.00M);
            Assert.That(sortedItems.Last().Price == 1.00M);
        }
    }
}
