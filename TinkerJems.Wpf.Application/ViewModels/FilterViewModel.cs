using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinkerJems.Core.Models;
using TinkerJems.Wpf.Application.Services;
using TinkerJems.Wpf.Application.Shared;
using TinkerJems.Wpf.Application.Views;

namespace TinkerJems.Wpf.Application.ViewModels
{
    public class FilterViewModel : BindableBase, INavigationAware
    {
        private JewelryService _jewelryService;
        private readonly IRegionManager _regionManager;

        public FilterViewModel(IRegionManager regionManager)
        {
            _jewelryService = new JewelryService();
            _regionManager = regionManager;
        }

        private DelegateCommand navigateToItem;
        public DelegateCommand NavigateToItem => navigateToItem ?? (navigateToItem = new DelegateCommand(
                () =>
                {
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("Item", SelectedJewelryItem);
                    HistoryStack.ViewStack.Push(new History { PageName = nameof(FilterView), Category = SelectedCategory});
                    _regionManager.RequestNavigate(Constants.NavigationRegion, nameof(ItemView), navigationParams);
                }
            ));


        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            SelectedCategory = navigationContext.Parameters.GetValue<string>("Category");
            JewelryItems = _jewelryService.GetJewelryByCategory(SelectedCategory);
            foreach (var j in JewelryItems)
            {
                j.ImageUrl = $"https://localhost:5001/images/{j.ImageUrl}";
                if(j.Tags != null)
                {
                    foreach(var tag in j.Tags)
                    {
                        Tags.Add(tag.Tag);
                    }

                }
            }

            AllJewelryItems = JewelryItems;

        }

        private IEnumerable<JewelryItem> jewelryItems;

        public IEnumerable<JewelryItem> JewelryItems
        {
            get { return jewelryItems; }
            set { SetProperty(ref jewelryItems , value); }
        }


        private IEnumerable<JewelryItem> allJewelryItems;

        public IEnumerable<JewelryItem> AllJewelryItems
        {
            get { return allJewelryItems; }
            set { SetProperty(ref allJewelryItems, value); }
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

        private List<Tag> tags;

        public List<Tag> Tags
        {
            get { return tags; }
            set { SetProperty( ref tags, value); }
        }


        private List<string> materials = Constants.Materials;

        public List<string> Materials
        {
            get { return materials; }
            set { materials = value; }
        }


        private string selectedCategory;

        public string SelectedCategory
        {
            get { return selectedCategory; }
            set { SetProperty(ref selectedCategory, value); }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }

        private string selectedSort;

        public string SelectedSort
        {
            get { return selectedSort; }
            set
            {
                SetProperty(ref selectedSort, value);
                SortItems(value);
            }
        }


        private string selectedMaterial;

        public string SelectedMaterial
        {
            get { return selectedMaterial; }
            set
            {
                SetProperty(ref selectedMaterial, value);
                FilterMaterial(value);
            }
        }

        private void FilterMaterial(string value)
        {
            //For now, it will be the only filter
            JewelryItems = AllJewelryItems;
            JewelryItems = JewelryItems.Where(j => j.Material == value);
        }

        private void SortItems(string sort)
        {
            JewelryItems = AllJewelryItems;

            if(sort == "Price (Low-High)")
            {
                JewelryItems = JewelryItems.OrderBy(j => j.Price);
            }
            else if(sort == "Price (High-Low)")
            {
                JewelryItems = JewelryItems.OrderByDescending(j => j.Price);
            }
            else if(sort == "Name (A-Z)")
            {
                JewelryItems = JewelryItems.OrderBy(j => j.Name);
            }
            else
            {
                JewelryItems = JewelryItems.OrderByDescending(j => j.Name);
            }
        }

        public IEnumerable<string> SortBy { get; } = new[] { "Price (Low-High)", "Price (High-Low)", "Name (A-Z)", "Name (Z-A)"};
        public IEnumerable<string> FilterByMaterial { get; } = Constants.Materials;

    }
}
