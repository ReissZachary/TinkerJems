using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinkerJems.Core.Models;
using TinkerJems.Wpf.Application.Services;

namespace TinkerJems.Wpf.Application.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        public DelegateCommand<string> NavigateToHome { get; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
            NavigateToHome = new DelegateCommand<string>((uri) =>
            {
                _regionManager.RequestNavigate("ContentRegion", uri);
            });
        }
    }
}
