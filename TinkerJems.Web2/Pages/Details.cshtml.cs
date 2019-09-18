using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TinkerJems.Core;
using TinkerJems.Core.Models;

namespace TinkerJems.Web2.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly TinkerJems.Web2.Data.ApplicationDbContext _context;

        public DetailsModel(TinkerJems.Web2.Data.ApplicationDbContext context)
        {
            _context = context;
        }


        public JewelryItem JewelryItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JewelryItem = await _context.JewelryItem.FirstOrDefaultAsync(m => m.Id == id);

            if (JewelryItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
