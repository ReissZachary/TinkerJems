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
using TinkerJems.Wpf.Application.Shared;
using TinkerJems.Wpf.Application.Views;
using System.Linq;
using System.Windows.Threading;

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
            Title = "CheckoutView";
        }

        private void initializeErrors()
        {
            QuantityError = "* Required";
            CustomerEmailError = "* Required";
        }

        private bool hasNoErrors()
        {
            if (CustomerEmail == null || Quantity == 0 || SelectedSize == null ||
                CustomerEmailError != null || QuantityError != null || SizeError != null)
            {
                SubmitError = "**Please complete the form correctly to procede**";
                return false;
            }

            SubmitError = null;
            return true;
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
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
                SelectedSize = SizeList.Skip(SizeList.Count / 2).First();
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
                    SendEmailToJeweler();
                    SendConfirmationEmail();
                    HistoryStack.ViewStack.Push(new History { PageName = nameof(SearchView)});
                    ResetCheckout();
                    _regionManager.RequestNavigate(Constants.NavigationRegion, nameof(ConfirmationView));
                }
            ));


        private DelegateCommand showWaitingText;
        public DelegateCommand ShowWaitingText => showWaitingText ?? (showWaitingText = new DelegateCommand(
                () =>
                {
                    if (hasNoErrors())
                    {
                        Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Loaded, (Action)(() =>
                        {
                            ButtonVisibility = Visibility.Collapsed;
                        }));
                        Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Loaded, (Action)(() =>
                        {
                            WaitingVisibility = Visibility.Visible;
                        }));

                        Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() =>
                        {
                            SubmitOrder.Execute();
                        }));
                    }

                }
            ));

        private Visibility waitingVisibility = Visibility.Collapsed;

        public Visibility WaitingVisibility
        {
            get { return waitingVisibility; }
            set { SetProperty(ref waitingVisibility, value); }
        }


        private Visibility buttonVisibility = Visibility.Visible;

        public Visibility ButtonVisibility
        {
            get { return buttonVisibility; }
            set { SetProperty(ref buttonVisibility, value); }
        }

        private void SendConfirmationEmail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("TinkerGems", "testemail.zachary.reiss@gmail.com"));
            message.To.Add(new MailboxAddress("Valued Customer", CustomerEmail));
            message.Subject = "Your confirmation order from Tinker Gems!";

            var text = "<h1>Dear " + CustomerEmail + "</ h1 >";
            text +=
                    @$"<br/>
                    <h1><strong>Here is a summary of your recent order from Tinker Gems:</strong></h1>
                    <table>
                        <tr><td>Item:</td><td> {OrderedItem.Name}</td></tr>
                        <tr><td>Total price:</td><td> {(OrderedItem.Price * Quantity):c}</td></tr>
                        <tr><td>Quantity:</td><td> {Quantity}</td></tr>
                        <tr><td>Size:</td><td> {SelectedSize}</td></tr>
                        <tr><td>Details:</td><td> {OrderDetails}</td></tr>
                    </table>
                    <h2>Thank you for choosing Tinker Gems!</h2>";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = text
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

        private void ResetCheckout()
        {
            initializeErrors();
            CustomerEmail = null;
            Quantity = 0;
            OrderDetails = null;
            SelectedSize = null;

        }

        private void SendEmailToJeweler()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("TinkerGems", "testemail.zachary.reiss@gmail.com"));
            message.To.Add(new MailboxAddress("Valued Customer", "testemail.zachary.reiss@gmail.com"));
            message.Subject = "Incoming Jewelry Order";

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<h1>Nettie, Here is a new order for you to process!</h1>" + "<br/>" +
                        "<h3>Item: " + OrderedItem.Name + "</h3>" +
                        "<h3>Total price: " + "$" + (OrderedItem.Price * Quantity) + "</h3>" +
                        "<h3>Quantity: " + Quantity.ToString() + "</h3>" +
                        "<h3>Size: " + SelectedSize + "</h3>" +
                        "<h3>Details: " + OrderDetails + "</h3>" + "</br><br/>" +
                        "<h3>Contact Email: " + CustomerEmail + "</h3>"

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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ButtonVisibility = Visibility.Visible;
            OrderedItem = navigationContext.Parameters.GetValue<JewelryItem>("OrderedItem");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            WaitingVisibility = Visibility.Collapsed;
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
