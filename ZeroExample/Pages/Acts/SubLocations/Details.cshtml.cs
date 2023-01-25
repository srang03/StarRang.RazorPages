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
    public class DetailsModel : PageModel
    {
        private readonly ZeroExample.Data.ApplicationDbContext _context;

        public DetailsModel(ZeroExample.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
