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

            await loadJewelryItem(id);

            if (JewelryItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        private async Task loadJewelryItem(int? id)
        {
            JewelryItem = await _context.JewelryItems
                            .Include(j => j.Tags)
                            .ThenInclude(t => t.Tag)
                            .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            foreach (var tagName in NewTags.Split("\r\n"))
            {
                var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName);
                if (tag == null)
                {
                    tag = new Tag { Name = tagName };
                    _context.Tags.Add(tag);
                    await _context.SaveChangesAsync();
                }
                JewelryItem = _context.JewelryItems.FirstOrDefault(m => m.Id == id);
                var itemTag = new JewelryItemTag();
                itemTag.JewelryItem = JewelryItem;
                itemTag.JewelryItemId = JewelryItem.Id;
                itemTag.Tag = tag;
                itemTag.TagId = tag.Id;
                _context.ItemTags.Add(itemTag);
                await _context.SaveChangesAsync();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteTagAsync( int jewelryItemId, int tagId)
        {
            var itemTag = await _context.ItemTags.FirstOrDefaultAsync(it => it.JewelryItemId == jewelryItemId && it.TagId == tagId);
            if (itemTag != null)
            {
                _context.ItemTags.Remove(itemTag);
                 await _context.SaveChangesAsync();
                await loadJewelryItem(jewelryItemId);
            }
            return Page();
        } 
    }
}
