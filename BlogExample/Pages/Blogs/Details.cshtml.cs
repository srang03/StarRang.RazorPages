using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlogExample.Data;
using BlogExample.Models;

namespace BlogExample.Pages.Blogs
{
    public class DetailsModel : PageModel
    {
        private readonly BlogExample.Data.ApplicationDbContext _context;

        public DetailsModel(BlogExample.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Blog Blog { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Blog == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog.FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            else 
            {
                Blog = blog;
            }
            return Page();
        }
    }
}
