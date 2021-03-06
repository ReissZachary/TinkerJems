﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TinkerJems.Core;
using TinkerJems.Core.Models;

namespace TinkerJems.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly TinkerJems.Core.Data.DataContext _context;

        public DetailsModel(TinkerJems.Core.Data.DataContext context)
        {
            _context = context;
        }


        public JewelryItem JewelryItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JewelryItem = await _context.JewelryItem.FirstOrDefaultAsync(m => m.Id == id);

            if (JewelryItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
