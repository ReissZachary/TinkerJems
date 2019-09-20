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
    public class DeleteModel : PageModel
    {
        private readonly TinkerJems.Web2.Data.ApplicationDbContext _context;

        public DeleteModel(TinkerJems.Web2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public JewelryItem JewelryItem { get; set; }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JewelryItem = await _context.JewelryItems.FindAsync(id);

            if (JewelryItem != null)
            {
                _context.JewelryItems.Remove(JewelryItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Items");
        }
    }
}
