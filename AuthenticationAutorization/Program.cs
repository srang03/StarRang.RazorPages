using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAutorization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddAuthentication("Cookies").AddCookie();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    string content = "<h1>.NET CORE 인증과 권한 코드</h1>";
                    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(content);
                });

                endpoints.MapGet("/Login", async context =>
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "User Name")
                    };

                    // 두 번째 파라미터는 스키마 이름
                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await context.SignInAsync("Cookies", claimsPrincipal);

                    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    await context.Response.WriteAsync("<h3>로그인</h3>");
                });

                endpoints.MapGet("/Info", async context =>
                {
                    string result = string.Empty;

                    if (context.User.Identity.IsAuthenticated)
                    {
                        result = $"<h3>로그인 이름: {context.User.Identity.Name}</h3>";
                    }
                    else
                    {
                        result = $"<h3>로그인 실패</h3>";
                    }

                    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(result, Encoding.Default);
                });

            });

        

            app.MapRazorPages();

            app.Run();
        }
    }
}