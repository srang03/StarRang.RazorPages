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
    public class DeleteModel : PageModel
    {
        private readonly BlogExample.Data.ApplicationDbContext _context;

        public DeleteModel(BlogExample.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }
            else 
            {
                Post = post;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }
            var post = await _context.Post.FindAsync(id);

            if (post != null)
            {
                Post = post;
                _context.Post.Remove(Post);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
