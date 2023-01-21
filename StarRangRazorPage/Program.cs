using StarRangRazorPage.Service;

namespace StarRangRazorPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddTransient<PortfolioServiceJsonFile>();

            var app = builder.Build();

            app.UseStaticFiles(); // 정적인 HTML, JS, CSS 실행
            //app.UseFileServer();
            // app.MapGet("/", () => "Hello World!");
            app.MapRazorPages();
            app.MapBlazorHub();
            app.Run();
        }
    }
}