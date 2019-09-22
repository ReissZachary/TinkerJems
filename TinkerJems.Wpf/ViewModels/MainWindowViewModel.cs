using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TinkerJems.Core.Models;

namespace TinkerJems.Wpf
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel(IRegionManager regionManager)
        {

            ViewRings = new DelegateCommand(
                () =>
                {
                    //execute
                    //string testMessage = "ADDED TODO";
                    //MessageBox.Show(testMessage);
                },
                () =>
                {
                    //can execute
                    return false;
                }
                );
            this.regionManager = regionManager;
        }
        public DelegateCommand ViewRings { get; }
        private DelegateCommand<string> _addItemCommand;
        private readonly IRegionManager regionManager;

        public DelegateCommand<string> AddItemCommand =>
            _addItemCommand ?? (_addItemCommand = new DelegateCommand<string>(ExecuteAddItemCommand));

        void ExecuteAddItemCommand(string parameter)
        {
            regionManager.RequestNavigate("ContentRegion", parameter);
        }

        private DelegateCommand<string> _editItemCommand;
        public DelegateCommand<string> EditItemCommand =>
            _editItemCommand ?? (_editItemCommand = new DelegateCommand<string>(ExecuteEditItemCommand));

        void ExecuteEditItemCommand(string parameter)
        {
            regionManager.RequestNavigate("ContentRegion", parameter);
        }

    }
}
