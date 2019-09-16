using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Prism.Commands;
using Prism.Mvvm;
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
        public MainWindowViewModel()
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
        }
        public DelegateCommand ViewRings { get; }


    }
}
