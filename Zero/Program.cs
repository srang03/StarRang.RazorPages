using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zero.Data;

namespace Zero
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();
            var serviceProvider = builder.Services.BuildServiceProvider().GetService<IServiceProvider>();
            var app = builder.Build();
        
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            CreateBuiltInData(serviceProvider).Wait();

            app.Run();
        }

        private static async Task CreateBuiltInData(IServiceProvider serviceProvider)
        {
            var _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _context.Database.EnsureCreated();
            // Default RoleTypes Add
            if (!_context.RoleType.Any())
            {
                _context.RoleType.Add(new Models.RoleType { Name = "Director", Active = true });
                _context.RoleType.Add(new Models.RoleType { Name = "Manager", Active = true });
                _context.RoleType.Add(new Models.RoleType { Name = "Supervisor", Active = true });
                _context.RoleType.Add(new Models.RoleType { Name = "Agent", Active = true });

                await _context.SaveChangesAsync();
            }
        }
    }
}