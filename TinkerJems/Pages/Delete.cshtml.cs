using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TinkerJems.Core.Models;

namespace TinkerJems.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly TinkerJems.Core.Data.DataContext _context;

        public DeleteModel(TinkerJems.Core.Data.DataContext context)
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

            JewelryItem = await _context.JewelryItem.FirstOrDefaultAsync(m => m.Id == id);

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

            JewelryItem = await _context.JewelryItem.FindAsync(id);

            if (JewelryItem != null)
            {
                _context.JewelryItem.Remove(JewelryItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Items");
        }
    }
}
