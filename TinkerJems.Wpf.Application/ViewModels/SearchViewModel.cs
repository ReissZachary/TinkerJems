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

        private DelegateCommand navigateToItem;
        public DelegateCommand NavigateToItem => navigateToItem ?? (navigateToItem = new DelegateCommand(
                ()=>
                {
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("Item", SelectedJewelryItem);
                    HistoryStack.ViewStack.Push(new History { PageName = Title });
                    _regionManager.RequestNavigate(Constants.NavigationRegion, nameof(ItemView), navigationParams);
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
                NavigateToItem.Execute();
            }
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
            foreach (var j in JewelryItems)
                j.ImageUrl = $"https://localhost:5001/images/{j.ImageUrl}";
            populateRandomJewelryItems(JewelryItems);
        }

        private void populateRandomJewelryItems(IEnumerable<JewelryItem> jewelryItems)
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
