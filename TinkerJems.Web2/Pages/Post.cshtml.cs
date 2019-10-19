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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
           if(id == null)
            {
                return NotFound();
            }

            
            
            BlogPost = await _context.TinkerJemsBlogPost.FindAsync(id);

            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            var doc = DocumentModel.Load("/Users/Zachary Reiss/source/repos/TinkerJems/TinkerJems.Web2/wwwroot/BlogPosts/" + BlogPost.Body);
            BlogPost.Body = doc.Content.ToString();

            if(BlogPost == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}