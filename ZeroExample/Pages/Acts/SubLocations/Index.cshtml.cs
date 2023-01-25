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
    public class IndexModel : PageModel
    {
        private readonly ZeroExample.Data.ApplicationDbContext _context;

        public IndexModel(ZeroExample.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SubLocation> SubLocation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.SubLocations != null)
            {
                SubLocation = await _context.SubLocations
                .Include(s => s.LocationRef).ToListAsync();
            }
        }
    }
}
