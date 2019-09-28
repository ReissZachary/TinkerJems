using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace TinkerJems.Wpf.ViewModels
{
    public class AddItemViewModel : BindableDataErrorInfoBase, INotifyPropertyChanged
    {

        public AddItemViewModel()
        {
            Price = .01M;
            Name = "";
            thumbnail= "";
            mainURL = "";
            description = "";
            longDescription = "";
        }

        private string? name;
        public string? Name
        {
            get { return name; }
            set
            {
                if (value == string.Empty)
                {
                    NameError = "* Item must have a name";
                }
                else
                {
                    NameError = null;
                }
                SetProperty(ref name, value);
            }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                if (value <= 0)
                {
                    PriceError = "* Price cannot be <= 0";
                }
                else
                {
                    PriceError = null;
                }
                SetProperty(ref price, value);
            }
        }

        private string thumbnail;
        public string Thumbnail
        {
            get { return thumbnail; }
            set
            {
                if (value == string.Empty)
                {
                    ThumbnailError = "* Item must have a thumbnail";
                }
                else
                {
                    ThumbnailError = null;
                }
                SetProperty(ref thumbnail, value);
            }
        }

        private string mainURL;
        public string MainURL
        {
            get { return mainURL; }
            set
            {
                if (value == string.Empty)
                {
                    MainURLError = "* Item must have a main URL";
                }
                else
                {
                    MainURLError = null;
                }
                SetProperty(ref mainURL, value);
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (value == string.Empty)
                {
                    DescriptionError = "* Item must have a description";
                }
                else
                {
                    DescriptionError = null;
                }
                SetProperty(ref description, value);
            }
        }

        private string longDescription;
        public string LongDescription
        {
            get { return longDescription; }
            set
            {
                if (value == string.Empty)
                {
                    LongDescriptionError = "* Item must have a long description";
                }
                else
                {
                    LongDescriptionError = null;
                }
                SetProperty(ref longDescription, value);
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
                NameErrorVisibility = value.IsNullOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private Visibility nameErrorVisibility;
        public Visibility NameErrorVisibility
        {
            get { return nameErrorVisibility; }
            set { SetProperty(ref nameErrorVisibility, value); }
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


        private string thumbnailError;
        public string ThumbnailError
        {
            get { return thumbnailError; }
            set
            {
                SetProperty(ref thumbnailError, value);

                ErrorDictionary[nameof(ThumbnailError)] = value;
                ThumbnailErrorVisibility = value.IsNullOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        private Visibility thumbnailErrorVisibility;
        public Visibility ThumbnailErrorVisibility
        {
            get { return thumbnailErrorVisibility; }
            set { SetProperty(ref thumbnailErrorVisibility, value); }
        }



        private string mainURLError;
        public string MainURLError
        {
            get { return mainURLError; }
            set
            {
                SetProperty(ref mainURLError, value);

                ErrorDictionary[nameof(MainURLError)] = value;
                MainURLErrorVisibility = value.IsNullOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private Visibility mainURLErrorVisibility;
        public Visibility MainURLErrorVisibility
        {
            get { return mainURLErrorVisibility; }
            set { SetProperty(ref mainURLErrorVisibility, value); }
        }



        private string descriptionError;
        public string DescriptionError
        {
            get { return descriptionError; }
            set
            {
                SetProperty(ref descriptionError, value);

                ErrorDictionary[nameof(DescriptionError)] = value;
                DescriptionErrorVisibility = value.IsNullOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private Visibility descriptionErrorVisibility;
        public Visibility DescriptionErrorVisibility
        {
            get { return descriptionErrorVisibility; }
            set { SetProperty(ref descriptionErrorVisibility, value); }
        }



        private string longDescriptionError;
        public string LongDescriptionError
        {
            get { return longDescriptionError; }
            set
            {
                SetProperty(ref longDescriptionError, value);
                ErrorDictionary[nameof(LongDescriptionError)] = value;
                LongDescriptionErrorVisibility = value.IsNullOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private Visibility longDescriptionErrorVisibility;
        public Visibility LongDescriptionErrorVisibility
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
            //Name = null;
            //Price = .01M;
            //Description = null;
            //LongDescription = null;
        }

        private DelegateCommand clearForm;
        public DelegateCommand ClearForm =>
            clearForm ?? (clearForm = new DelegateCommand(ExecuteClearForm));

        void ExecuteClearForm()
        {
            Name = "";
            Price = 0;
            Description = "";
            LongDescription = "";
        }

    }

    public static class DemoExtensionMethods
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }
    }
}
