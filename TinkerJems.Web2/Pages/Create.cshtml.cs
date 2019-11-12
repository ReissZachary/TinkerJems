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

       
        public string NewTagName { get; set; }

        public string[] Images { get; set; }

        public IActionResult OnGet()
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //JewelryItem = await _context.JewelryItems.FirstOrDefaultAsync(m => m.Id == id);
            var folder = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot", "Images");
            Images = Directory.GetFiles(folder);

            //if (JewelryItem == null)
            //{
            //    return NotFound();
            //}
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

            return RedirectToPage("./Items");
        }

        public async Task OnPostAddTagAsync()
        {

        }
    }
}