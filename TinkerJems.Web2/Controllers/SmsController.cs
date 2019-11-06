using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;

namespace TinkerJems.Web2.Controllers
{
    public class SmsController : Controller //twilio
    {
        public ActionResult SendSms()
        {
            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

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