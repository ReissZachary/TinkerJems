using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinkerJems.Core.Models;
using TinkerJems.Web2.Data;

namespace TinkerJems.Web2.Pages
{
    public class SelectedTagModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<JewelryItem> JewelryItems { get; }

        public SelectedTagModel(ApplicationDbContext context)
        {
            _context = context;
            JewelryItems = new List<JewelryItem>();
        }
        public void OnGet()
        {

        }
    }
}