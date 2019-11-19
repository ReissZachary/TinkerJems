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
    public class DeleteTagModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DeleteTagModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<JewelryItemTag> ItemTags { get; private set; }
        public IEnumerable<JewelryItemTag> SelectedTag { get; set; }

        public JewelryItem JewelryItem { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            JewelryItem = await _context.JewelryItems
                            .Include(j => j.Tags)
                            .ThenInclude(t => t.Tag)
                            .FirstOrDefaultAsync(m => m.Id == id);
            ItemTags = JewelryItem.Tags;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string tagName)
        {
            SelectedTag = ItemTags.Where(t => t.Tag.Name == tagName);

            if (tagName == null)
            {
                return NotFound();
            }
            if (JewelryItem != null)
            {
                //_context.JewelryItems.Remove(JewelryItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Items");
        }

    }
}