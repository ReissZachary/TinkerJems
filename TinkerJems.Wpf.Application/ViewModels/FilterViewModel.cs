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

        private DelegateCommand<JewelryItem> navigateToItem;
        public DelegateCommand<JewelryItem> NavigateToItem => navigateToItem ?? (navigateToItem = new DelegateCommand<JewelryItem>(
                (item) =>
                {
                    SelectedJewelryItem = item;
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
            AllJewelryItems = _jewelryService.GetJewelryByCategory(SelectedCategory);
            SelectedMaterial = navigationContext.Parameters.GetValue<string>("Material");
            foreach (var j in AllJewelryItems)
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

            applyFilters(AllJewelryItems);
        }

        //Jewelry items to be modified
        private IEnumerable<JewelryItem> filteredJewelryItems;

        public IEnumerable<JewelryItem> FilteredJewelryItems
        {
            get { return filteredJewelryItems; }
            set { SetProperty(ref filteredJewelryItems , value); }}



        //All the elements in the selected category
        private IEnumerable<JewelryItem> allJewelryItems;

        public IEnumerable<JewelryItem> AllJewelryItems
        {
            get { return allJewelryItems; }
            set { SetProperty(ref allJewelryItems, value); }
        }

        private void applyFilters(IEnumerable<JewelryItem> originalList)
        {
            var temp = originalList;
            temp = FilterByTag(temp);
            temp = FilterByMaterial(temp);
            temp = sortItems(temp);

            FilteredJewelryItems = temp;

        }

        private IEnumerable<JewelryItem> FilterByMaterial(IEnumerable<JewelryItem> temp)
        {
            if (SelectedMaterial == null)
                return temp;
            return temp.Where(t => t.Material == SelectedMaterial);
        }

        private IEnumerable<JewelryItem> FilterByTag(IEnumerable<JewelryItem> temp)
        {
            if (SelectedTag == null)
                return temp;
            else
            {
                return temp;
            }
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

        private List<Tag> tags;

        public List<Tag> Tags
        {
            get { return tags; }
            set { SetProperty( ref tags, value); }
        }

        private string selectedTag;

        public string SelectedTag
        {
            get { return selectedTag; }
            set
            {
                SetProperty(ref selectedTag, value);
                applyFilters(AllJewelryItems);
            }
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

        private string selectedSort;

        public string SelectedSort
        {
            get { return selectedSort; }
            set
            {
                SetProperty(ref selectedSort, value);
                applyFilters(AllJewelryItems);
            }
        }


        private string selectedMaterial;

        public string SelectedMaterial
        {
            get { return selectedMaterial; }
            set
            {
                SetProperty(ref selectedMaterial, value);
                applyFilters(AllJewelryItems);
            }
        }

        private IEnumerable<JewelryItem> sortItems(IEnumerable<JewelryItem> originalList)
        {
            var sort = SelectedSort;

            if(sort == "Price (Low-High)")
            {
                return FilteredJewelryItems = originalList.OrderBy(j => j.Price);
            }
            else if(sort == "Price (High-Low)")
            {
                return FilteredJewelryItems = originalList.OrderByDescending(j => j.Price);
            }
            else if(sort == "Name (A-Z)")
            {
                return FilteredJewelryItems = originalList.OrderBy(j => j.Name);
            }
            else
            {
                return FilteredJewelryItems = originalList.OrderByDescending(j => j.Name);
            }
        }

        public IEnumerable<string> SortBy { get; } = new[] { "Price (Low-High)", "Price (High-Low)", "Name (A-Z)", "Name (Z-A)"};
        public IEnumerable<string> DropDownMaterials { get; } = Constants.Materials;

    }
}
