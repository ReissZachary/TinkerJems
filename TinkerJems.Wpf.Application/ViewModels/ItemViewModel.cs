using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using TinkerJems.Core.Models;
using TinkerJems.Wpf.Application.Events;

namespace TinkerJems.Wpf.Application.ViewModels
{
    public class ItemViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        public ItemViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            eventAggregator.GetEvent<SelectedItemEvent>().Subscribe((message) => { this.SelectedItem = message; });
            var x = eventAggregator.GetEvent<SelectedItemEvent>();
                
        }

        private JewelryItem selectedItem;
        public JewelryItem SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem,value); }
        }
        
    }
}
