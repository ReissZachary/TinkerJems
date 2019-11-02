using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace TinkerJems.Web2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            const string accountSid = "ACd4303a58147c10be0b54f140b10beba6";
            const string authToken = "92cc4e41a273e134ce8b1fca3b658227";
            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Is this working?",
                from: new Twilio.Types.PhoneNumber("+14844986297"),
                to: new Twilio.Types.PhoneNumber("+14356719074")
            );

            Console.WriteLine(message.Sid);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UsePort();
    }

    public static class WebHostBuilderExtenstions
    {
        public static IWebHostBuilder UsePort(this IWebHostBuilder builder)
        {
            var port = Environment.GetEnvironmentVariable("PORT");
            if (string.IsNullOrEmpty(port))
                return builder;
            return builder.UseUrls($"http//*:{port}");
        }
    }
}
