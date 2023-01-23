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
    public class IndexModel : PageModel
    {
        private readonly VisualAcademy.Data.ApplicationDbContext _context;

        public IndexModel(VisualAcademy.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Broth> Broth { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Broths != null)
            {
                Broth = await _context.Broths.ToListAsync();
            }
        }
    }
}
