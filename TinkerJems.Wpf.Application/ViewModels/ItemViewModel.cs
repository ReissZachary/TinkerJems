﻿using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using TinkerJems.Core.Models;
using TinkerJems.Wpf.Application.Events;
using TinkerJems.Wpf.Application.Shared;
using TinkerJems.Wpf.Application.Views;

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
            Title = "ItemView";
        }

        private DelegateCommand navigateToCheckout;
        public DelegateCommand NavigateToCheckout => navigateToCheckout ?? (navigateToCheckout= new DelegateCommand(
                () =>
                {
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("OrderedItem", SelectedItem);
                    HistoryStack.ViewStack.Push(new History { PageName = Title, Item = SelectedItem });
                    _regionManager.RequestNavigate(Constants.NavigationRegion, nameof(CheckoutView), navigationParams);
                }
            ));

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
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
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
