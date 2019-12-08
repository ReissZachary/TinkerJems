using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TinkerJems.Core.Models;
using TinkerJems.Wpf.Application.Services;
using TinkerJems.Wpf.Application.Views;

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
                SplashScreenHomeVisibility = Visibility.Collapsed;
            });
        }

        private DelegateCommand showWaitingText;
        public DelegateCommand ShowWaitingText => showWaitingText ?? (showWaitingText = new DelegateCommand(
                () =>
                {
                    Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Loaded, (Action)(() =>
                    {
                        WaitingVisibility = Visibility.Visible;
                    }));

                    Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() =>
                    {
                        NavigateToHome.Execute(nameof(HomeView));
                    }));

                }
            ));

        private Visibility splashScreenHomeVisibility;

        public Visibility SplashScreenHomeVisibility
        {
            get { return splashScreenHomeVisibility; }
            set { SetProperty(ref splashScreenHomeVisibility, value); }
        }

        private Visibility waitingVisibility = Visibility.Collapsed;

        public Visibility WaitingVisibility
        {
            get { return waitingVisibility; }
            set { SetProperty(ref waitingVisibility, value); }
        }

    }
}
