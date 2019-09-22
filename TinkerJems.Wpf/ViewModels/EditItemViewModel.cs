using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinkerJems.Wpf.ViewModels
{
    public class EditItemViewModel : BindableBase
    {
        public EditItemViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        private readonly IRegionManager regionManager;

        private DelegateCommand<string> _editItemCommand;
        public DelegateCommand<string> EditItemCommand =>
            _editItemCommand ?? (_editItemCommand = new DelegateCommand<string>(ExecuteEditItemCommand));

        void ExecuteEditItemCommand(string parameter)
        {
            regionManager.RequestNavigate("ContentArea", parameter);
        }

    }
}
