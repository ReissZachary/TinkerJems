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
    public class SelectedTagModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<JewelryItem> JewelryItems { get; set; }
        public Tag Tag { get; private set; }

        public SelectedTagModel(ApplicationDbContext context)
        {
            _context = context;
            JewelryItems = new List<JewelryItem>();
        }
        public async Task OnGetAsync(string tagName)
        {
            Tag = await _context.Tags.Include(t => t.TaggedItems)
                .ThenInclude(i => i.JewelryItem)
                .FirstOrDefaultAsync(t => t.Name == tagName);
            JewelryItems.AddRange(Tag.TaggedItems.Select(t => t.JewelryItem));
        }
    }
}