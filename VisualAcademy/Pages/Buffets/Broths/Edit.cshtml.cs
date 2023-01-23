using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VisualAcademy.Data;
using VisualAcademy.Models;

namespace VisualAcademy.Pages.Buffets.Broths
{
    public class EditModel : PageModel
    {
        private readonly VisualAcademy.Data.ApplicationDbContext _context;

        public EditModel(VisualAcademy.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Broth Broth { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Broths == null)
            {
                return NotFound();
            }

            var broth =  await _context.Broths.FirstOrDefaultAsync(m => m.Id == id);
            if (broth == null)
            {
                return NotFound();
            }
            Broth = broth;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Broth).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrothExists(Broth.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BrothExists(int id)
        {
          return _context.Broths.Any(e => e.Id == id);
        }
    }
}
