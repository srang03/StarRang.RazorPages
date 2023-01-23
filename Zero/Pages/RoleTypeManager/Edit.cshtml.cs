using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zero.Data;
using Zero.Models;

namespace Zero.Pages.RoleTypeManager
{
    public class EditModel : PageModel
    {
        private readonly Zero.Data.ApplicationDbContext _context;

        public EditModel(Zero.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RoleType RoleType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.RoleType == null)
            {
                return NotFound();
            }

            var roletype =  await _context.RoleType.FirstOrDefaultAsync(m => m.Id == id);
            if (roletype == null)
            {
                return NotFound();
            }
            RoleType = roletype;
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

            _context.Attach(RoleType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleTypeExists(RoleType.Id))
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

        private bool RoleTypeExists(int id)
        {
          return _context.RoleType.Any(e => e.Id == id);
        }
    }
}
