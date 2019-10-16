using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TinkerJems.Core.Models;
using TinkerJems.Web2.Data;

namespace TinkerJems.Web2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
            JewelryItems = new List<JewelryItem>();
        }

        public IList<JewelryItem> JewelryItems { get;set; }

        public async Task OnGetAsync()
        {
            JewelryItems = await _context.JewelryItems.ToListAsync();
        }

        public async Task<JsonResult> OnGetAllAsync()
        {
            return new JsonResult(await _context.JewelryItems.ToListAsync());
        }
    }
}
