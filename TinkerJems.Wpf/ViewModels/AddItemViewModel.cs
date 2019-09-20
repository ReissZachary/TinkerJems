using Prism.Commands;
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
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }

        private string thumbnail;
        public string Thumbnail
        {
            get { return thumbnail; }
            set { SetProperty(ref thumbnail, value); }
        }

        private string mainURL;
        public string MainURL
        {
            get { return mainURL; }
            set { SetProperty(ref mainURL, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private string longDescription;
        public string LongDescription
        {
            get { return longDescription; }
            set { SetProperty(ref longDescription, value); }
        }

        private DelegateCommand addItem;
        public DelegateCommand AddItem =>
            addItem ?? (addItem = new DelegateCommand(ExecuteAddItem)
            .ObservesProperty(() => Name)
            .ObservesProperty(() => Price)
            .ObservesProperty(() => Thumbnail)
            .ObservesProperty(() => MainURL)
            .ObservesProperty(() => Description)
            .ObservesProperty(() => LongDescription));

        void ExecuteAddItem()
        {
           
        }

        //public AddItemViewModel()
        //{
        //    Price = 15M;
        //    Thumbnail = "anoia";
        //    MainURL = "HELLO";
        //    Description = "item of jewelry";
        //    LongDescription = "This is the long descriptiong that descibes the minute details of the item.";
        //}
    }
}
