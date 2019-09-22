﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TinkerJems.Core.Models;

namespace TinkerJems.Web2.Pages
{
    public class CreateModel : PageModel
    {
        private readonly TinkerJems.Web2.Data.ApplicationDbContext _context;

        public CreateModel(TinkerJems.Web2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
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

            _context.JewelryItems.Add(JewelryItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Items");
        }
    }
}