using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZeroExample.Data;
using ZeroExample.Models;

namespace ZeroExample.Pages.Acts.Properties
{
    public class DeleteModel : PageModel
    {
        private readonly ZeroExample.Data.ApplicationDbContext _context;

        public DeleteModel(ZeroExample.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Property Property { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Properties == null)
            {
                return NotFound();
            }

            var property = await _context.Properties.FirstOrDefaultAsync(m => m.Id == id);

            if (property == null)
            {
                return NotFound();
            }
            else 
            {
                Property = property;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Properties == null)
            {
                return NotFound();
            }
            var property = await _context.Properties.FindAsync(id);

            if (property != null)
            {
                Property = property;
                _context.Properties.Remove(Property);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
