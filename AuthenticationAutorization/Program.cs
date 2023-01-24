using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace AuthenticationAutorization
{
    /// <summary>
    /// Data Transfer Object
    /// </summary>
    public class ClaimDTO
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

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
                    content += "<a href=\"Login\">로그인</a><br>";
                    content += "<a href=\"Logout\">로그아웃</a><br>";
                    content += "<a href=\"Info\">정보</a><br>";
                    content += "<a href=\"Infojson\">JSON 정보</a><br>";
                    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(content);
                });

                endpoints.MapGet("/Login", async context =>
                {
                    var claims = new List<Claim>
                    {
                        //new Claim(ClaimTypes.Name, "User Name");
                        new Claim(ClaimTypes.Name, "아이디")
                    };

                    // 두 번째 파라미터는 스키마 이름
                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await context.SignInAsync("Cookies", claimsPrincipal);

                    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    await context.Response.WriteAsync("<h3>로그인</h3>");
                });

                #region Info
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
                #endregion

                #region InfoJson
                endpoints.MapGet("/Infojson", async context =>
                {
                    string json = string.Empty;

                    if (context.User.Identity.IsAuthenticated)
                    {
                        // json = "{\"type\" : \"Name\", \"value\": \"User Name\"}";
                        var claims = context.User.Claims.Select(c => new ClaimDTO { Type = c.Type, Value = c.Value });
                        // 한글 인코딩 처리 하지 않기 위해 JavaScriptEncoder.UnsafeRelaxedJsonEscaping 옵션 추가
                        json = JsonSerializer.Serialize<IEnumerable<ClaimDTO>>(claims, new JsonSerializerOptions { Encoder= JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
                    }
                    else
                    {
                        json += "{}";
                    }

                    context.Response.Headers["Content-Type"] = "application/json; charset=utf-8";
                    await context.Response.WriteAsync(json, Encoding.Default);
                });
                #endregion


                #region Logout
                endpoints.MapGet("/Logout", async context =>
                {
                    await context.SignOutAsync("Cookies");
                    context.Response.Headers["Content-Type"] = "application/json; charset=utf-8";
                    await context.Response.WriteAsync("<h3>로그아웃</h3>", Encoding.Default);
                });
                #endregion

            });

        

            app.MapRazorPages();

            app.Run();
        }
    }
}