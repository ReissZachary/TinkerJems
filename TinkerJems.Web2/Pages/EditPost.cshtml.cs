using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TinkerJems.Core.Models;
using TinkerJems.Web2.Data;

namespace TinkerJems.Web2.Pages
{
    [Authorize(Policy = SeedData.SecurityPolicy_Edit)]
    public class EditPostModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public EditPostModel(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public TinkerJemsBlogPost BlogPost { get; set; }
        public string[] Images { get; set; }
        public string[] BlogPosts { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BlogPost = await _context.TinkerJemsBlogPost.FindAsync(id);
            var folder = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot", "Images");
            Images = Directory.GetFiles(folder);
            var postsFolder = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot", "BlogPosts");
            BlogPosts = Directory.GetFiles(postsFolder);

            if (BlogPost == null)
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

            BlogPost.Posted = DateTime.Now;
            _context.Attach(BlogPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(BlogPost.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Blog");
        }

        private bool BlogPostExists(int id)
        {
            return _context.TinkerJemsBlogPost.Any(e => e.ID == id);
        }
    }
}