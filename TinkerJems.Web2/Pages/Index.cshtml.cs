using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinkerJems.Web2.Services;

namespace TinkerJems.Web2.Pages
{
    public class HomePageModel : PageModel
    {
        private readonly QuoteService _quoteService;
        

        public HomePageModel()
        {
            _quoteService = new QuoteService();
           // _ = PrintQuote();
        }


        private Quote quote;

        public Quote Quote
        {
            get { return quote; }
            set { quote = value; }
        }

        //private async Task PrintQuote()
        //{
        //    Quote = await _quoteService.getQuoteAsync();
        //}

        public void OnGet()
        {

        }
    }
}