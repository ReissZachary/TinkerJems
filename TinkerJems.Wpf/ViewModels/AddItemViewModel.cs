using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinkerJems.Wpf.ViewModels
{
    public class AddItemViewModel : BindableBase
    {
        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }

        public AddItemViewModel()
        {
            Price = 15M;
        }
    }
}
