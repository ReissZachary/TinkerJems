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

namespace TinkerJems.Wpf.Application.ViewModels
{
    public class CheckoutViewModel : BindableBase, INavigationAware
    {

        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        public CheckoutViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
        }

        IConfiguration config;

        private JewelryItem orderedItem;

        public JewelryItem OrderedItem
        {
            get { return orderedItem; }
            set { SetProperty(ref orderedItem, value); }
        }

        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }

        private int size;

        public int Size
        {
            get { return size; }
            set { SetProperty(ref size, value); }
        }

        private string customerEmail;

        public string CustomerEmail
        {
            get { return customerEmail; }
            set { SetProperty(ref customerEmail, value); }
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
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("ZackDiego", "testemail.zachary.reiss@gmail.com"));
                    message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "testemail.zachary.reiss@gmail.com"));
                    message.Subject = "How you doin'?";

                    message.Body = new TextPart("plain")
                    {
                        Text = OrderDetails + Quantity.ToString() + Size.ToString()
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
}
