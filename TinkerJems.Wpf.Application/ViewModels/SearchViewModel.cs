using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TinkerJems.Core.Models;
using TinkerJems.Wpf.Application.Events;
using TinkerJems.Wpf.Application.Services;
using TinkerJems.Wpf.Application.Shared;
using TinkerJems.Wpf.Application.Views;

namespace TinkerJems.Wpf.Application.ViewModels
{
    public class SearchViewModel : BindableBase
    {
        public SearchViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _jewelryService = new JewelryService();
            Title = "SearchView";
            _ = populateJewelry();
        }

        private DelegateCommand<JewelryItem> navigateToItem;
        public DelegateCommand<JewelryItem> NavigateToItem => navigateToItem ?? (navigateToItem = new DelegateCommand<JewelryItem>(
                (item)=>
                {
                    SelectedJewelryItem = item;
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("Item", item);
                    HistoryStack.ViewStack.Push(new History { PageName = Title });
                    _regionManager.RequestNavigate(Constants.NavigationRegion, nameof(ItemView), navigationParams);
                }
            ));

        private DelegateCommand<string> navigateToFilter;
        public DelegateCommand<string> NavigateToFilter => navigateToFilter ?? (navigateToFilter = new DelegateCommand<string>(
                (category) =>
                {
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("Category", category);
                    HistoryStack.ViewStack.Push(new History { PageName = Title });
                    _regionManager.RequestNavigate(Constants.NavigationRegion, nameof(FilterView), navigationParams);
                }
            ));

        private readonly IRegionManager _regionManager;
        private readonly JewelryService _jewelryService;

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private Task loadViewTask;

        public Task LoadViewTask
        {
            get { return loadViewTask; }
            set { SetProperty(ref loadViewTask, value); }
        }

        private Visibility viewVisibility;

        public Visibility ViewVisibility
        {
            get { return viewVisibility; }
            set { SetProperty(ref viewVisibility, value); }
        }

        private Visibility loadingViewVisibility;
        public Visibility LoadingViewVisibility
        {
            get { return loadingViewVisibility; }
            set
            {
                SetProperty(ref loadingViewVisibility, value);
            }
        }


        private IEnumerable<JewelryItem> jewelryItems;

        public IEnumerable<JewelryItem> JewelryItems
        {
            get { return jewelryItems; }
            set { jewelryItems = value; }
        }

        private JewelryItem selectedJewelryItem;

        public JewelryItem SelectedJewelryItem
        {
            get { return selectedJewelryItem; }
            set
            {
                SetProperty(ref selectedJewelryItem, value);
            }
        }

        private string selectedCategory;

        public string SelectedCategory
        {
            get { return selectedCategory; }
            set {SetProperty(ref selectedCategory, value); }
        }



        private IEnumerable<JewelryItem> randomRings;
        public IEnumerable<JewelryItem> RandomRings
        {
            get { return randomRings; }
            set { randomRings = value; }
        }

        private IEnumerable<JewelryItem> randomNecklaces;

        public IEnumerable<JewelryItem> RandomNecklaces
        {
            get { return randomNecklaces; }
            set { randomNecklaces = value; }
        }

        private IEnumerable<JewelryItem> randomEarrings;

        public IEnumerable<JewelryItem> RandomEarrings
        {
            get { return randomEarrings; }
            set { randomEarrings = value; }
        }

        private IEnumerable<JewelryItem> randomBracelets;

        public IEnumerable<JewelryItem> RandomBracelets
        {
            get { return randomBracelets; }
            set { randomBracelets = value; }
        }

        private async Task populateJewelry()
        {
            JewelryItems = await _jewelryService.GetJewelryItemsAsync();
            LoadImages(JewelryItems);
            PopulateRandomJewelryItems(JewelryItems);
        }

        public void LoadImages(IEnumerable<JewelryItem> jewelryItems)
        {
            foreach (var j in jewelryItems)
                j.ImageUrl = $"https://localhost:5001/images/{j.ImageUrl}";
        }

        public void PopulateRandomJewelryItems(IEnumerable<JewelryItem> jewelryItems)
        {
            populateRandomRings(jewelryItems);
            populateRandomNecklaces(jewelryItems);
            populateRandomEarrings(jewelryItems);
            populateRandomBracelets(jewelryItems);

        }

        private void populateRandomRings(IEnumerable<JewelryItem> jewelryItems)
        {
            var rnd = new Random();
            var rings = jewelryItems.Where(i=>i.Category == "Ring");
            RandomRings = rings.OrderBy(i => rnd.Next()).Take(4);
        }

        private void populateRandomNecklaces(IEnumerable<JewelryItem> jewelryItems)
        {
            var rnd = new Random();
            var necklaces = jewelryItems.Where(i=>i.Category == "Necklace");
            RandomNecklaces = necklaces.OrderBy(i => rnd.Next()).Take(4);
        }

        private void populateRandomEarrings(IEnumerable<JewelryItem> jewelryItems)
        {
            var rnd = new Random();
            var earrings = jewelryItems.Where(i => i.Category == "Earring");
            RandomEarrings = earrings.OrderBy(i => rnd.Next()).Take(4);
        }

        private void populateRandomBracelets(IEnumerable<JewelryItem> jewelryItems)
        {
            var rnd = new Random();
            var bracelets = jewelryItems.Where(i => i.Category == "Bracelet");
            RandomBracelets = bracelets.OrderBy(i => rnd.Next()).Take(4);
        }


    }
}
