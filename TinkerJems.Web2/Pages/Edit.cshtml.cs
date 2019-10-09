using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinkerJems.Core.Models;

namespace TinkerJems.Web2.Pages
{
    [Authorize(Policy = SeedData.SecurityPolicy_Edit)]
    public class EditModel : PageModel
    {
        private readonly TinkerJems.Web2.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public EditModel(TinkerJems.Web2.Data.ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public JewelryItem JewelryItem { get; set; }

        [BindProperty]
        public string[] Images { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JewelryItem = await _context.JewelryItems.FirstOrDefaultAsync(m => m.Id == id);
            var folder = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot", "Images");
            Images = Directory.GetFiles(folder);

            if (JewelryItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            JewelryItem.ImageThumbnailUrl = JewelryItem.ImageUrl;
            _context.Attach(JewelryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JewelryItemExists(JewelryItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Items");
        }

        private bool JewelryItemExists(int id)
        {
            return _context.JewelryItems.Any(e => e.Id == id);
        }
    }
}
