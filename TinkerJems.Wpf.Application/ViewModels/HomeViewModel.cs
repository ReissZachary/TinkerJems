using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using TinkerJems.Wpf.Application.Shared;

namespace TinkerJems.Wpf.Application.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        public DelegateCommand<string> NavigateToSearch { get; }

        public HomeViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Title = "HomeView";
            NavigateToSearch = new DelegateCommand<string>((uri) =>
            {
                HistoryStack.ViewStack.Push(new History {PageName = Title });
                _regionManager.RequestNavigate("NavigationRegion", uri);
            });
        }

        private DelegateCommand navigateBack;
        public DelegateCommand NavigateBack => navigateBack ?? (navigateBack = new DelegateCommand(
                () =>
                {
                    var view = HistoryStack.ViewStack.Pop();
                    if(view.PageName == "ItemView")
                    {

                        var navigationParams = new NavigationParameters();
                        navigationParams.Add("Item", view.Item);
                        _regionManager.RequestNavigate(Constants.NavigationRegion, view.PageName, navigationParams);
                    }
                    else
                    {
                        _regionManager.RequestNavigate(Constants.NavigationRegion, view.PageName);
                    }
                }
            ));

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
    }
}
