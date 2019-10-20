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
    public class BlogsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IList<TinkerJemsBlogPost> BlogPosts { get; set; }

        public TinkerJemsBlogPost BlogPost { get; set; }

        public BlogsModel(ApplicationDbContext context)
        {
            _context = context;
            BlogPosts = new List<TinkerJemsBlogPost>();
        }
        public async Task OnGetAsync()
        {
            BlogPosts = await _context.TinkerJemsBlogPost.ToListAsync();
        }
    }
}