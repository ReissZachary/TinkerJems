using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TinkerJems.Core.Models;

namespace TinkerJems.Web2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TinkerJems.Core.Data.DataContext _context;

        public IndexModel(TinkerJems.Core.Data.DataContext context)
        {
            _context = context;
            JewelryItem = new List<JewelryItem>();
        }

        public IList<JewelryItem> JewelryItem { get;set; }

        public async Task OnGetAsync()
        {
            JewelryItem = await _context.JewelryItem.ToListAsync();
        }
    }
}
