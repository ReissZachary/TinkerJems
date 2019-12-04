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
    public class SelectedCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IQueryable<JewelryItem> JewelryItems { get; set; }

        public string Category { get; set; }

        public SelectedCategoryModel(ApplicationDbContext context)
        {
            _context = context;
            //JewelryItems = new IQueryable<JewelryItem>();
        }
        public async Task OnGetAsync(string category)
        {
            Category = category + "s";
            JewelryItems = _context.JewelryItems.Where(j => j.Category == category);
        }
    }
}