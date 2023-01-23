using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlogExample.Models;

namespace BlogExample.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BlogExample.Models.Blog> Blog { get; set; }
        public DbSet<BlogExample.Models.Post> Post { get; set; }
    }
}