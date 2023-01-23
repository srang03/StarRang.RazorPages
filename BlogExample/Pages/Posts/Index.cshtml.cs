using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlogExample.Data;
using BlogExample.Models;

namespace BlogExample.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly BlogExample.Data.ApplicationDbContext _context;

        public IndexModel(BlogExample.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Post != null)
            {
                Post = await _context.Post
                .Include(p => p.Blog).ToListAsync();
            }
        }
    }
}
