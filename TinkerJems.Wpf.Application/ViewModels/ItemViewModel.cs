using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using TinkerJems.Core.Models;
using TinkerJems.Wpf.Application.Events;

namespace TinkerJems.Wpf.Application.ViewModels
{
    public class ItemViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        public ItemViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<SelectedItemEvent>().Subscribe(msg => {
                SelectedItem = msg;
            }, ThreadOption.UIThread);
            var x = eventAggregator.GetEvent<SelectedItemEvent>().SynchronizationContext;

        }

        private JewelryItem selectedItem;
        public JewelryItem SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem,value); }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            SelectedItem = navigationContext.Parameters.GetValue<JewelryItem>("Item");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
    }
}
