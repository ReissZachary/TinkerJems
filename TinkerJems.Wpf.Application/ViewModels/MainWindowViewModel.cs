﻿using Prism.Mvvm;
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
        private readonly IRegionManager regionManager;
        private readonly JewelryService _jewelryService;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            _jewelryService = new JewelryService();
            _ = populateJewelry();
        }

        private IEnumerable<JewelryItem> jewelryItems;

        public IEnumerable<JewelryItem> JewelryItems
        {
            get { return jewelryItems; }
            set { jewelryItems = value; }
        }

        private async Task populateJewelry()
        {
            JewelryItems = await _jewelryService.GetJewelryItemsAsync();
        }



    }
}
