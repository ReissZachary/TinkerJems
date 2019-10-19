using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinkerJems.Core.Models;
using TinkerJems.Web2.Data;

namespace TinkerJems.Web2.Pages
{
    public class PostModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PostModel(ApplicationDbContext context)
        {
            _context = context;
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

            if(BlogPost == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}