using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZeroExample.Data;
using ZeroExample.Models;

namespace ZeroExample.Pages.Acts.SubLocations
{
    public class EditModel : PageModel
    {
        private readonly ZeroExample.Data.ApplicationDbContext _context;

        public EditModel(ZeroExample.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SubLocation SubLocation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SubLocations == null)
            {
                return NotFound();
            }

            var sublocation =  await _context.SubLocations.FirstOrDefaultAsync(m => m.Id == id);
            if (sublocation == null)
            {
                return NotFound();
            }
            SubLocation = sublocation;
           ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id");
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

            _context.Attach(SubLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubLocationExists(SubLocation.Id))
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

        private bool SubLocationExists(int id)
        {
          return _context.SubLocations.Any(e => e.Id == id);
        }
    }
}
