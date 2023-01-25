using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZeroExample.Data;
using ZeroExample.Models;

namespace ZeroExample.Pages.Acts.SubLocations
{
    public class DeleteModel : PageModel
    {
        private readonly ZeroExample.Data.ApplicationDbContext _context;

        public DeleteModel(ZeroExample.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public SubLocation SubLocation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SubLocations == null)
            {
                return NotFound();
            }

            var sublocation = await _context.SubLocations.FirstOrDefaultAsync(m => m.Id == id);

            if (sublocation == null)
            {
                return NotFound();
            }
            else 
            {
                SubLocation = sublocation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.SubLocations == null)
            {
                return NotFound();
            }
            var sublocation = await _context.SubLocations.FindAsync(id);

            if (sublocation != null)
            {
                SubLocation = sublocation;
                _context.SubLocations.Remove(SubLocation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
