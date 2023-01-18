namespace StarRangRazorPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();

            var app = builder.Build();

            app.UseStaticFiles(); // ������ HTML, JS, CSS ����
            //app.UseFileServer();
            // app.MapGet("/", () => "Hello World!");
            app.MapRazorPages();
            app.Run();
        }
    }
}