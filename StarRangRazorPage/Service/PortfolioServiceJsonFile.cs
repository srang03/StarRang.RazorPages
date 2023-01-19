using StarRangRazorPage.Models;
using System.Text.Json;

namespace StarRangRazorPage.Service
{
    public class PortfolioServiceJsonFile
    {
        public IEnumerable<Portfolio> GetPortfolios()
        {
            var jsonFileName = @"C:\StarRang.RazorPages\StarRang.RazorPages\StarRangRazorPage\wwwroot\Portfolio\Portfolios.json";

            using (var sr = File.OpenText(jsonFileName))
            {
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var result =  JsonSerializer.Deserialize<Portfolio[]>(sr.ReadToEnd(), options);
                return result;
            }   
        }
    }
}
