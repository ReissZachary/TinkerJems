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
                    Process.Start(personalProcessInfo);
                }
            ));

        public ProcessStartInfo personalProcessInfo = new ProcessStartInfo
        {
            FileName = "Chrome.exe",
            UseShellExecute = true,
            Arguments = "https://www.instagram.com/nettie.graham/"
        };

        private DelegateCommand tinkerInstagram;
        public DelegateCommand TinkerInstagram => tinkerInstagram ?? (tinkerInstagram = new DelegateCommand(
                () =>
                {
                    Process.Start(tinkerProcessInfo);
                }
            ));

        public ProcessStartInfo tinkerProcessInfo = new ProcessStartInfo
        {
            FileName = "Chrome.exe",
            UseShellExecute = true,
            Arguments = "https://www.instagram.com/tinker.gems/"
        };

        private DelegateCommand email;
        public DelegateCommand Email => email ?? (email = new DelegateCommand(
                () =>
                {
                    var email = "tinkergemsjewelry@gmail.com";
                    string mailto = $"mailto:{email}?Subject={"Subject of message"}&Body={" "}";
                    Process.Start(new ProcessStartInfo(mailto) { UseShellExecute = true });
                }
            ));       
    }
}
