using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zero.Data;
using Zero.Models;

namespace Zero.Pages.RoleTypeManager
{
    public class DeleteModel : PageModel
    {
        private readonly Zero.Data.ApplicationDbContext _context;

        public DeleteModel(Zero.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public RoleType RoleType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.RoleType == null)
            {
                return NotFound();
            }

            var roletype = await _context.RoleType.FirstOrDefaultAsync(m => m.Id == id);

            if (roletype == null)
            {
                return NotFound();
            }
            else 
            {
                RoleType = roletype;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.RoleType == null)
            {
                return NotFound();
            }
            var roletype = await _context.RoleType.FindAsync(id);

            if (roletype != null)
            {
                RoleType = roletype;
                _context.RoleType.Remove(RoleType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
