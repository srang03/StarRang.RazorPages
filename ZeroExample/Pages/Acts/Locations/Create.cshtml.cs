﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZeroExample.Data;
using ZeroExample.Models;

namespace ZeroExample.Pages.Acts.Locations
{
    public class CreateModel : PageModel
    {
        private readonly ZeroExample.Data.ApplicationDbContext _context;

        public CreateModel(ZeroExample.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Location Location { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Locations.Add(Location);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
