using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TinkerJems.Models;

namespace TinkerJems.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TinkerJems.Models.DataContext _context;

        public IndexModel(TinkerJems.Models.DataContext context)
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
