using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TinkerJems.Core;
using TinkerJems.Core.Models;
using TinkerJems.Web2.Data;

namespace TinkerJems.Web2.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public JewelryItem JewelryItem { get; set; }

        [BindProperty]
        public string NewTags { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JewelryItem = await _context.JewelryItems.FirstOrDefaultAsync(m => m.Id == id);

            if (JewelryItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
