using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinkerJems.Core.Models;
using TinkerJems.Web2.Data;
using System.Reflection.Metadata;
using GemBox.Document;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using System.Linq;

namespace TinkerJems.Web2.Pages
{
    public class PostModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;

        public PostModel(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        //public string Title { get; set; }
        public TinkerJemsBlogPost BlogPost { get; set; }        

        public IActionResult OnGet(string slug)
        {
           if(slug == null)
            {
                return NotFound();
            }

            BlogPost = _context.TinkerJemsBlogPost.FirstOrDefault(p => p.Slug == slug);

            if(BlogPost == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}