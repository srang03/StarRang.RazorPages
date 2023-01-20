using StarRangRazorPage.Models;
using System.Text.Json;

namespace StarRangRazorPage.Service
{
    public class PortfolioServiceJsonFile
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private string JsonFileName {
            get
            {
                return Path.Combine(_webHostEnvironment.WebRootPath, "Portfolio", "Portfolios.json");
            }
        }

        public PortfolioServiceJsonFile(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<Portfolio> GetPortfolios()
        {

            using (var sr = File.OpenText(JsonFileName))
            {
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var result =  JsonSerializer.Deserialize<Portfolio[]>(sr.ReadToEnd(), options);
                return result;
            }   
        }
    }
}
