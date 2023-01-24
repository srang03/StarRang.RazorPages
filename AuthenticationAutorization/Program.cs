using AuthenticationAutorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            //builder.Services.AddAuthentication("Cookies").AddCookie();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            builder.Services.AddControllers();
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
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    string content = "<h1>.NET CORE 인증과 권한 코드</h1>";
                    content += "<a href=\"Login\">로그인</a><br>";
                    content += "<a href=\"Login/User\">유저 로그인</a><br>";
                    content += "<a href=\"Login/Administrator\">관리자 로그인</a><br>";
                    content += "<a href=\"Logout\">로그아웃</a><br>";
                    content += "<a href=\"Info\">정보</a><br>";
                    content += "<a href=\"InfoDetails\">Detail 정보</a><br>";
                    content += "<a href=\"Infojson\">JSON 정보</a><br>";
                    content += "<hr> <a href=\"Landing/Index\">랜딩페이지</a><br>";
                    content += "<a href=\"Greeting\">환영페이지</a><br>";
                    content += "<a href=\"DashBoard/Index\">관리페이지</a><br>";
                    content += "<a href=\"api/AuthService\">Web API</a><br>";
                    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(content);
                });
                #region Login
                endpoints.MapGet("/Login", async context =>
                {
                    var claims = new List<Claim>
                    {
                        //new Claim(ClaimTypes.Name, "User Name");
                        new Claim(ClaimTypes.Name, "아이디")
                    };

                    // 두 번째 파라미터는 스키마 이름
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    await context.Response.WriteAsync("<h3>로그인</h3>");
                });
                #endregion


                #region /Login/{라우터 토큰}
                endpoints.MapGet("/Login/{Username}", async context =>
                       {
                           var UserName = context.Request.RouteValues["UserName"].ToString();
                           var claims = new List<Claim>
                           {

                                new Claim(ClaimTypes.Name, UserName),
                                new Claim(ClaimTypes.Email, UserName + "@a.com"),
                                new Claim(ClaimTypes.Role, "Users"),
                                new Claim("원하는 이름", "원하는 값"),
                           };

                           if(UserName == "Administrator")
                           {
                               claims.Add(new Claim(ClaimTypes.Role, "Administrators"));
                           }
                           // 두 번째 파라미터는 스키마 이름
                           var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                           var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                           await context.SignInAsync(
                               CookieAuthenticationDefaults.AuthenticationScheme, 
                               claimsPrincipal, 
                               new AuthenticationProperties {  IsPersistent = false });

                           context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                           await context.Response.WriteAsync("<h3>로그인</h3>");
                       });
                #endregion

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

                #region InfoDetails
                endpoints.MapGet("/InfoDetails", async context =>
                {
                    string result = string.Empty;

                    if (context.User.Identity.IsAuthenticated)
                    {
                        foreach (var claim in context.User.Claims)
                        {
                            result += $"{claim.Type} = {claim.Value}<br/>";
                        }

                        // 특정한 롤에 포함되어있는지 확인
                        if (context.User.IsInRole("Administrators") && context.User.IsInRole("Users"))
                        {
                            result += $"<h3>관리자 로그인</h3>";
                        }
                        else
                        {
                            result += $"<h3>일반 로그인</h3>";
                        }

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
                        json = JsonSerializer.Serialize<IEnumerable<ClaimDTO>>(claims, new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
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
                    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    context.Response.Headers["Content-Type"] = "application/json; charset=utf-8";
                    await context.Response.WriteAsync("<h3>로그아웃</h3>", Encoding.Default);
                });
                #endregion

            });

            app.MapDefaultControllerRoute();

            app.MapRazorPages();

            app.Run();
        }
    }

    #region MVC Controller
    // 누구나 접근이 가능하다.
    [AllowAnonymous]
    public class LandingController: Controller
    {
        public IActionResult Index()
        {
            return Content("누구나 접근이 가능");

        }

        // 인증된 사용자만 접근이 가능하다.
        [Authorize]
        [Route("/Greeting")]
        public IActionResult Greeting()
        {
            var roleName = HttpContext.User.IsInRole("Administrators") ? "관리자" : "사용자";
            return Content($"<em>{roleName}</em>님 반갑습니다.", "text/html", Encoding.Default);
        }

    }

    [Authorize(Roles = "Administrators")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return Content("관리자님 반갑습니다.");
        }
    }
}
#endregion

#region WebAPI
[ApiController]
[Route("api/[Controller]")]
public class AuthServiceController: ControllerBase
{
[HttpGet]
    [Authorize(Roles = "Administrators")]
    public IEnumerable<ClaimDTO> Get()
    {
        return HttpContext.User.Claims.Select(c => new ClaimDTO { Type =c.Type, Value= c.Value });
    }
}
#endregion