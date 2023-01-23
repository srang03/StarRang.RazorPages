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
    public class IndexModel : PageModel
    {
        private readonly BlogExample.Data.ApplicationDbContext _context;

        public IndexModel(BlogExample.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Blog> Blog { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Blog != null)
            {
                Blog = await _context.Blog.ToListAsync();
            }
        }
    }
}
