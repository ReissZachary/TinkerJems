using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TinkerJems.Wpf.Application.ViewModels
{
    public class FilterViewModel : BindableBase, INavigationAware
    {
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            SelectedCategory = navigationContext.Parameters.GetValue<string>("Category");
        }

        private string selectedCategory;

        public string SelectedCategory
        {
            get { return selectedCategory; }
            set { SetProperty(ref selectedCategory, value); }
        }

    }
}
