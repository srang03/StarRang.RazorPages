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
    public class DeleteModel : PageModel
    {
        private readonly VisualAcademy.Data.ApplicationDbContext _context;

        public DeleteModel(VisualAcademy.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Broths == null)
            {
                return NotFound();
            }
            var broth = await _context.Broths.FindAsync(id);

            if (broth != null)
            {
                Broth = broth;
                _context.Broths.Remove(Broth);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
