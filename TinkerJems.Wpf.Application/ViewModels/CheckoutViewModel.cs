using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using TinkerJems.Core.Models;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit;
using TinkerJems.Wpf.Application.Services;
using System.Windows;

namespace TinkerJems.Wpf.Application.ViewModels
{
    public class CheckoutViewModel : BindableBase, INavigationAware
    {

        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly ValidationService _valiationService;

        public CheckoutViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _valiationService = new ValidationService();
            initializeErrors();
        }

        private void initializeErrors()
        {
            QuantityError = "*";
            CustomerEmailError = "* Required";
            SizeError = "*";
        }

        private bool hasNoErrors()
        {
            if (CustomerEmail == null || Quantity == 0 || SelectedSize == null ||
                CustomerEmailError != null || QuantityError != null || SizeError != null)
            {
                SubmitError = "Please complete the form correctly to procede";
                return false;
            }

            SubmitError = null;
            return true;
        }

        private string submitError;

        public string SubmitError
        {
            get { return submitError; }
            set { SetProperty(ref submitError, value); }
        }


        private JewelryItem orderedItem;

        public JewelryItem OrderedItem
        {
            get { return orderedItem; }
            set
            {
                SetProperty(ref orderedItem, value);
                SizeList = _valiationService.GetAllSizesByCategory(OrderedItem.Category);
            }
        }

        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (_valiationService.ValidateQuantity(value) == false)
                {
                    QuantityError = "Error: Quantity must be a number above 0 and less than 50";
                }
                else
                {
                    QuantityError = null;
                }
                SetProperty(ref quantity, value);
            }
        }

        private Visibility quantityErrorVisibility;

        public Visibility QuantityErrorVisibility
        {
            get { return quantityErrorVisibility; }
            set { SetProperty(ref quantityErrorVisibility, value); }
        }

        private string quantityError;

        public string QuantityError
        {
            get { return quantityError; }
            set
            {
                SetProperty(ref quantityError, value);
                QuantityErrorVisibility = value.IsNullOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible;
            }

        }

        private List<string> sizeList;

        public List<string> SizeList
        {
            get { return sizeList; }
            set
            {
                SetProperty(ref sizeList, value);
            }
        }

        private string selectedSize;

        public string SelectedSize
        {
            get { return selectedSize; }
            set
            {
                SetProperty(ref selectedSize, value);
                SizeError = null;
            }
        }


        private Visibility sizeErrorVisibility;

        public Visibility SizeErrorVisibility
        {
            get { return sizeErrorVisibility; }
            set { SetProperty(ref sizeErrorVisibility, value); }
        }

        private string sizeError;

        public string SizeError
        {
            get { return sizeError; }
            set
            {
               SetProperty(ref sizeError, value);
               SizeErrorVisibility = value.IsNullOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private string customerEmail;

        public string CustomerEmail
        {
            get { return customerEmail; }
            set
            {
                if (_valiationService.ValidateEmail(value) == false)
                {
                    CustomerEmailError = "Error: Must enter a valid email address";
                }
                else
                {
                    CustomerEmailError = null;
                }
                SetProperty(ref customerEmail, value);
            }
        }

        private Visibility customerEmailErrorVisibility;

        public Visibility CustomerEmailErrorVisibility
        {
            get { return customerEmailErrorVisibility; }
            set { SetProperty(ref customerEmailErrorVisibility, value); }
        }


        private string customerEmailError;

        public string CustomerEmailError
        {
            get { return customerEmailError; }
            set
            {
                SetProperty(ref customerEmailError, value);
                CustomerEmailErrorVisibility = value.IsNullOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible;
            }

    }


        private string orderDetails;

        public string OrderDetails
        {
            get { return orderDetails; }
            set { SetProperty(ref orderDetails, value); }
        }

        private DelegateCommand submitOrder;
        public DelegateCommand SubmitOrder => submitOrder ?? (submitOrder = new DelegateCommand(
                () =>
                {
                    if (hasNoErrors())
                    {
                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress("ZackDiego", "testemail.zachary.reiss@gmail.com"));
                        message.To.Add(new MailboxAddress("Valued Customer", "testemail.zachary.reiss@gmail.com"));
                        message.Subject = "Incoming Jewelry Order";

                        message.Body = new TextPart("plain")
                        {
                            Text =  "Nettie, Here is a new order for you to process!" + "\n\n" +
                                    "Item: " + OrderedItem.Name + "\n" +
                                    "Total price: " + "$" + (OrderedItem.Price * Quantity) + "\n" +
                                    "Quantity: " + Quantity.ToString() + "\n" +
                                    "Size: " + SelectedSize + "\n" +
                                    "Details: " + OrderDetails + "\n\n\n" +

                                    "Contact Email: " + CustomerEmail + "\n\n" 

                        };

                        using (var client = new SmtpClient())
                        {
                            // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                            client.Connect("smtp.gmail.com", 587, false);

                            // Note: only needed if the SMTP server requires authentication
                            client.Authenticate("testemail.zachary.reiss@gmail.com", "Thisisatestemail!");

                            client.Send(message);
                            client.Disconnect(true);
                        }
                    }
                }
            ));



        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            OrderedItem = navigationContext.Parameters.GetValue<JewelryItem>("OrderedItem");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
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
