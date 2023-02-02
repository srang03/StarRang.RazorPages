using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Models;
using System.Reflection.Metadata.Ecma335;

namespace RunGroupWebApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {
            
        }

    }
}
