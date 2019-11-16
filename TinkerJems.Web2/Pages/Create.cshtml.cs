using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using TinkerJems.Core.Models;

namespace TinkerJems.Web2.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public CreateModel(Data.ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        public string[] Images { get; set; }
        [BindProperty]
        public string NewTags { get; set; }
        public IActionResult OnGet()
        {
            var folder = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot", "Images");
            Images = Directory.GetFiles(folder);
            return Page();
        }

        [BindProperty]
        public JewelryItem JewelryItem { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            JewelryItem.ImageThumbnailUrl = JewelryItem.ImageUrl;
            _context.JewelryItems.Add(JewelryItem);
            await _context.SaveChangesAsync();
           foreach (var tagName in NewTags.Split("\r\n"))
            {
                var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName);
                if(tag == null)
                {
                    tag = new Tag { Name = tagName };
                    _context.Tags.Add(tag);
                    await _context.SaveChangesAsync();
                }
                var itemTag = new JewelryItemTag();
                itemTag.JewelryItem = JewelryItem;
                itemTag.Tag = tag;
                _context.ItemTags.Add(itemTag);                
                await _context.SaveChangesAsync();
                
            }
            return RedirectToPage("./Items");
        }
       
    }
}