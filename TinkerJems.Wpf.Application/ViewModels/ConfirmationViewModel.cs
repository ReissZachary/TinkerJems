using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using TinkerJems.Core.Models;
using TinkerJems.Wpf.Application.Shared;
using TinkerJems.Wpf.Application.Views;

namespace TinkerJems.Wpf.Application.ViewModels
{
    public class ConfirmationViewModel : BindableBase
    {
        public ConfirmationViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        private DelegateCommand continueShopping;
        private IRegionManager _regionManager;

        public DelegateCommand ContinueShopping => continueShopping ?? (continueShopping = new DelegateCommand(
                () =>
                {
                    _regionManager.RequestNavigate(Constants.NavigationRegion, nameof(SearchView));
                }
            ));
    }
}
