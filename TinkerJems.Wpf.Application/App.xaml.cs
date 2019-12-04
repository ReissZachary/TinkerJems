using CommonServiceLocator;
using Prism.Events;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TinkerJems.Wpf.Application.Views;

namespace TinkerJems.Wpf.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return ServiceLocator.Current.GetInstance<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SearchView>();
            containerRegistry.RegisterForNavigation<HomeView>();
            containerRegistry.RegisterForNavigation<ConfirmationView>();
            containerRegistry.RegisterForNavigation<ItemView>();
            containerRegistry.RegisterForNavigation<FilterView>();
            containerRegistry.RegisterForNavigation<CheckoutView>();
        }

        protected override void ConfigureServiceLocator()
        {
            base.ConfigureServiceLocator();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}
