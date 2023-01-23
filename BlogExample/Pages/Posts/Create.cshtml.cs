using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BlogExample.Data;
using BlogExample.Models;

namespace BlogExample.Pages.Posts
{
    public class CreateModel : PageModel
    {
        private readonly BlogExample.Data.ApplicationDbContext _context;

        public CreateModel(BlogExample.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BlogId"] = new SelectList(_context.Blog, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          //if (!ModelState.IsValid)
          //  {
            //    return Page();
            //}

            _context.Post.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
