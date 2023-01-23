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
    public class DetailsModel : PageModel
    {
        private readonly Zero.Data.ApplicationDbContext _context;

        public DetailsModel(Zero.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
