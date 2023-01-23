using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VisualAcademy.Data;
using VisualAcademy.Models;

namespace VisualAcademy.Pages.Buffets.Broths
{
    public class DetailsModel : PageModel
    {
        private readonly VisualAcademy.Data.ApplicationDbContext _context;

        public DetailsModel(VisualAcademy.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Broth Broth { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Broths == null)
            {
                return NotFound();
            }

            var broth = await _context.Broths.FirstOrDefaultAsync(m => m.Id == id);
            if (broth == null)
            {
                return NotFound();
            }
            else 
            {
                Broth = broth;
            }
            return Page();
        }
    }
}
