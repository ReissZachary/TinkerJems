using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinkerJems.Wpf.Application.Shared;
using TinkerJems.Wpf.Application.Views;

namespace TinkerJems.Wpf.Application.ViewModels
{
    public class HomeViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;

        public HomeViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Title = "HomeView";
            SelectedCategory = Categories.First();
        }

        private void populateHome()
        {
            NavigateToSearch.Execute();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            populateHome();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private DelegateCommand navigateToSearch;
        public DelegateCommand NavigateToSearch => navigateToSearch ?? (navigateToSearch = new DelegateCommand(
                () =>
                {
                    _regionManager.RequestNavigate(Constants.NavigationRegion, nameof(SearchView));
                }));

        private DelegateCommand navigateBack;
        public DelegateCommand NavigateBack => navigateBack ?? (navigateBack = new DelegateCommand(
                () =>
                {
                    try
                    {
                        var view = HistoryStack.ViewStack.Pop();
                        if (view.PageName == nameof(ItemView))
                        {
                            var navigationParams = new NavigationParameters();
                            navigationParams.Add("Item", view.Item);
                            _regionManager.RequestNavigate(Constants.NavigationRegion, view.PageName, navigationParams);
                        }
                        else if (view.PageName == nameof(FilterView))
                        {
                            var navigationParams = new NavigationParameters();
                            navigationParams.Add("Category", view.Category);
                            _regionManager.RequestNavigate(Constants.NavigationRegion, view.PageName, navigationParams);
                        }
                        else if(view.PageName == nameof(SearchView) || view.PageName == nameof(CheckoutView))
                        {
                            NavigateToSearch.Execute();
                        }
                        else if (view.PageName == nameof(HowItWorksView) || view.PageName == nameof(AboutMeView))
                        {
                            NavigateToSearch.Execute();
                        }
                        else
                        {
                            NavigateToSearch.Execute();
                        }
                    }
                    catch
                    {
                        NavigateToSearch.Execute();
                    }
                }
            ));


        private DelegateCommand navigateToFilter;
        public DelegateCommand NavigateToFilter => navigateToFilter ?? (navigateToFilter = new DelegateCommand(
                () =>
                {
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("Category", SelectedCategory);
                    _regionManager.RequestNavigate(Constants.NavigationRegion, nameof(FilterView), navigationParams);
                }
            ));

        private DelegateCommand navigateToAboutMe;
        public DelegateCommand NavigateToAboutMe => navigateToAboutMe ?? (navigateToAboutMe = new DelegateCommand(
                () =>
                {
                    _regionManager.RequestNavigate(Constants.NavigationRegion, nameof(AboutMeView));
                }
            ));

        private DelegateCommand navigateToHowItWorks;
        public DelegateCommand NavigateToHowItWorks => navigateToHowItWorks ?? (navigateToHowItWorks = new DelegateCommand(
                () =>
                {
                    _regionManager.RequestNavigate(Constants.NavigationRegion, nameof(HowItWorksView));
                }
            ));

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string selectedCategory;

        public string SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                SetProperty(ref selectedCategory,value);
                NavigateToFilter.Execute();

            }
        }

        public IEnumerable<string> Categories { get; } = new[] { "Jewelry", "Rings", "Necklaces", "Earrings", "Bracelets" };

    }
}
