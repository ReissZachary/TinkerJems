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
            var accountSid = "ACd4303a58147c10be0b54f140b10beba6";
            var authToken = "92cc4e41a273e134ce8b1fca3b658227";

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