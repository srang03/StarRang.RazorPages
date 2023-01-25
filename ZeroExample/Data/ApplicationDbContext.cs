using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZeroExample.Models;

namespace ZeroExample.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Property> Properties { get; set; } = null!;

        public DbSet<Location> Locations { get; set; } = null!;

        public DbSet<SubLocation> SubLocations { get; set; } = null!;   
    }
}