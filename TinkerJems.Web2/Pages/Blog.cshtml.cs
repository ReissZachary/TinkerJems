using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TinkerJems.Core.Models;
using TinkerJems.Web2.Data;
using TinkerJems.Web2.Services;

namespace TinkerJems.Web2.Pages
{
    public class BlogsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IList<TinkerJemsBlogPost> BlogPosts { get; set; }
        public IList<TinkerJemsBlogPost> sortedPosts { get; set; }

        private readonly IAdviceService _greatAdviceService;

        public TinkerJemsBlogPost BlogPost { get; set; }

        public BlogsModel(ApplicationDbContext context, IAdviceService adviceService)
        {
            _context = context;
            BlogPosts = new List<TinkerJemsBlogPost>();
            sortedPosts = new List<TinkerJemsBlogPost>();
            _greatAdviceService = adviceService;
        }

        private GreatAdvice advice;

        public GreatAdvice Advice
        {
            get { return advice; }
            set { advice = value; }
        }

        public async Task OnGetAsync()
        {
            BlogPosts = await _context.TinkerJemsBlogPost.ToListAsync();
            sortedPosts = BlogPosts.OrderByDescending(o => o.Posted).ToList();
            Advice = await _greatAdviceService.GetAdviceAsync();
        }
    }
}