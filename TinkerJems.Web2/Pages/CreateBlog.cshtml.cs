using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinkerJems.Core.Models;
using TinkerJems.Web2.Data;

namespace TinkerJems.Web2.Pages
{
    public class CreateBlogModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;

        public CreateBlogModel(Data.ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        public string[] Images { get; set; }

        public IActionResult OnGet()
        {
            var folder = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", "Images");
            Images = Directory.GetFiles(folder);

            return Page();
        }

        [BindProperty]
        public TinkerJemsBlogPost BlogPost { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TinkerJemsBlogPost.Add(BlogPost);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Blogs");
        }
    }
}