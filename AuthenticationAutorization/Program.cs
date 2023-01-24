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
                    string content = "<h1>.NET CORE ������ ���� �ڵ�</h1>";
                    content += "<a href=\"Login\">�α���</a><br>";
                    content += "<a href=\"Logout\">�α׾ƿ�</a><br>";
                    content += "<a href=\"Info\">����</a><br>";
                    content += "<a href=\"Infojson\">JSON ����</a><br>";
                    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(content);
                });

                endpoints.MapGet("/Login", async context =>
                {
                    var claims = new List<Claim>
                    {
                        //new Claim(ClaimTypes.Name, "User Name");
                        new Claim(ClaimTypes.Name, "���̵�")
                    };

                    // �� ��° �Ķ���ʹ� ��Ű�� �̸�
                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await context.SignInAsync("Cookies", claimsPrincipal);

                    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    await context.Response.WriteAsync("<h3>�α���</h3>");
                });

                #region Info
                endpoints.MapGet("/Info", async context =>
                {
                    string result = string.Empty;

                    if (context.User.Identity.IsAuthenticated)
                    {
                        result = $"<h3>�α��� �̸�: {context.User.Identity.Name}</h3>";
                    }
                    else
                    {
                        result = $"<h3>�α��� ����</h3>";
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
                        // �ѱ� ���ڵ� ó�� ���� �ʱ� ���� JavaScriptEncoder.UnsafeRelaxedJsonEscaping �ɼ� �߰�
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
                    await context.Response.WriteAsync("<h3>�α׾ƿ�</h3>", Encoding.Default);
                });
                #endregion

            });

        

            app.MapRazorPages();

            app.Run();
        }
    }
}