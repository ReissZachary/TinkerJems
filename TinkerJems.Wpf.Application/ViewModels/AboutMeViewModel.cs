using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Navigation;
using Prism.Mvvm;
using Prism.Commands;

namespace TinkerJems.Wpf.Application.ViewModels
{
    public class AboutMeViewModel : BindableBase
    {
        public AboutMeViewModel()
        {

        }

        private DelegateCommand personalInstagram;
        public DelegateCommand PersonalInstagram => personalInstagram ?? (personalInstagram = new DelegateCommand(
                () =>
                {
                    Process.Start("Chrome.exe", "https://www.instagram.com/nettie.graham/");
                }
            ));
    }
}
