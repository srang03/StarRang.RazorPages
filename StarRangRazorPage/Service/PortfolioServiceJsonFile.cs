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

        public void AddRating(int id, int rating)
        {
            var Portfolios = GetPortfolios();
            int[] arr = new int[] { 1, 2, 3, 4 };
           if(Portfolios.First(p => p.Id == id).Ratings == null)
            {
                Portfolios.First(p => p.Id == id).Ratings = new int[] { rating };
            }
            else
            {
                arr.Append(30);
                var temp  = arr.ToList();
                temp.Add(30);
                var ratings = Portfolios.First(p => p.Id == id).Ratings.ToList();
                ratings.Add(rating);
                Portfolios.First(p => p.Id == id).Ratings = ratings.ToArray();
            }
            

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Portfolio>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions 
                    { 
                        SkipValidation = true, Indented = true 
                    }), Portfolios);
            }
        }
    }
}
