using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TinkerJems.Wpf.Application.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        public DelegateCommand<string> NavigateToSearch { get; }

        public HomeViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateToSearch = new DelegateCommand<string>((uri) =>
            {
                _regionManager.RequestNavigate("NavigationRegion", uri);
            });
        }
    }
}
