using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace TinkerJems.Wpf.ViewModels
{
    public class AddItemViewModel : BindableDataErrorInfoBase
    {

        public AddItemViewModel()
        {          

            Price = .01M;
            Thumbnail = "example.png";
            MainURL = "mainExample.png";
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value == "")
                {
                    NameError = "Item must have a name";
                }
                else
                {
                    NameError = null;
                }
                SetProperty(ref name, value);
            }
        }
        private string nameError;
        public string NameError
        {
            get { return nameError; }
            set
            {
                SetProperty(ref nameError, value);
                ErrorDictionary[nameof(NameError)] = value;
                PriceErrorVisibility = value.IsNullOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private Visibility nameErrorVisibility;
        public Visibility NameErrorVisibility
        {
            get { return nameErrorVisibility; }
            set { SetProperty(ref nameErrorVisibility, value); }
        }


        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                if(value <= 0)
                {
                    PriceError = "Price cannot be <= 0";
                }
                else
                {
                    PriceError = null;
                }
                SetProperty(ref price, value);
            }
        }

        private string priceError;
        public string PriceError
        {
            get { return priceError; }
            set
            {
                SetProperty(ref priceError, value);
                ErrorDictionary[nameof(PriceError)] = value;
                PriceErrorVisibility = value.IsNullOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        private Visibility priceErrorVisibility;
        public Visibility PriceErrorVisibility
        {
            get { return priceErrorVisibility; }
            set { SetProperty(ref priceErrorVisibility, value); }
        }

        private string thumbnail;
        public string Thumbnail
        {
            get { return thumbnail; }
            set
            {
                if (value == null)
                {
                    ThumbnailError = "Item must have a thumbnail";
                }
                else
                {
                    ThumbnailError = null;
                }
                SetProperty(ref thumbnail, value);
            }
        }

        private string thumbnailError;
        public string ThumbnailError
        {
            get { return thumbnailError; }
            set
            {
                SetProperty(ref thumbnailError, value);

                ErrorDictionary[nameof(ThumbnailError)] = value;
                PriceErrorVisibility = value.IsNullOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        private Visibility thumbnailErrorVisibility;
        public Visibility ThumbnailErrorVisibility
        {
            get { return thumbnailErrorVisibility; }
            set { SetProperty(ref thumbnailErrorVisibility, value); }
        }

        private string mainURL;
        public string MainURL
        {
            get { return mainURL; }
            set
            {
                if (value == null)
                {
                    MainURLError = "Item must have a main URL";
                }
                else
                {
                    MainURLError = null;
                }
                SetProperty(ref mainURL, value);
            }
        }

        private string mainURLError;
        public string MainURLError
        {
            get { return mainURLError; }
            set
            {
                SetProperty(ref mainURLError, value);

                ErrorDictionary[nameof(MainURLError)] = value;
                PriceErrorVisibility = value.IsNullOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private Visibility mainURLErrorVisibility;
        public Visibility MainURLErrorVisibility
        {
            get { return mainURLErrorVisibility; }
            set { SetProperty(ref mainURLErrorVisibility, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (value == null)
                {
                    DescriptionError = "Item must have a description";
                }
                else
                {
                    DescriptionError = null;
                }
                SetProperty(ref description, value);
            }
        }

        private string descriptionError;
        public string DescriptionError
        {
            get { return descriptionError; }
            set
            {
                SetProperty(ref descriptionError, value);

                ErrorDictionary[nameof(DescriptionError)] = value;
                PriceErrorVisibility = value.IsNullOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private Visibility descriptionErrorVisibility;
        public Visibility DescriptionErrorVisibility
        {
            get { return descriptionErrorVisibility; }
            set { SetProperty(ref descriptionErrorVisibility, value); }
        }

        private string longDescription;
        public string LongDescription
        {
            get { return longDescription; }
            set
            {
                if (value == null)
                {
                    LongDescriptionError = "Item must have a long description";
                }
                else
                {
                    LongDescriptionError = null;
                }
                SetProperty(ref longDescription, value);
            }
        }

     
        private string longDescriptionError;
        public string LongDescriptionError
        {
            get { return longDescriptionError; }
            set
            {
                SetProperty(ref longDescriptionError, value);
                ErrorDictionary[nameof(LongDescriptionError)] = value;
                PriceErrorVisibility = value.IsNullOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private Visibility longDescriptionErrorVisibility;
        public Visibility LongDescriptionVisibility
        {
            get { return longDescriptionErrorVisibility; }
            set { SetProperty(ref longDescriptionErrorVisibility, value); }
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
            string message = "Item Added!! You Win!";
            MessageBox.Show(message);
        }

        private DelegateCommand clearForm;
        public DelegateCommand ClearForm =>
            clearForm ?? (clearForm = new DelegateCommand(ExecuteClearForm));

        void ExecuteClearForm()
        {
            Name = null;
            Price = 0;
            Thumbnail = "example.png";
            MainURL = "mainExample.png";
            Description = null;
            LongDescription = null;
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

    public static class DemoExtensionMethods
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }
    }
}
