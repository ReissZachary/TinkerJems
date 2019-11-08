using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TinkerJems.Web2.Controllers
{
    public class SmsController : Controller //twilio
    {
        private readonly IConfiguration configuration;

        public SmsController(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public ActionResult SendSms()
        {
            string accountSid = configuration["TwilioAccountSid"];
            string authToken = configuration["TwilioAuthToken"];

            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber("+14356719074");
            var from = new PhoneNumber("+14844986297");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "Use the code TINKJEMSALE for a great discount!");
            return Content(message.Sid);
        }

        //public ActionResult ReceiveSms()
        //{
        //    var response = new MessagingResponse();
        //    response.Message("Hello! Redeem this code to get some awesome discounts! TinkerDeal007");

        //    return TwiML(response);

        //}
    }
}