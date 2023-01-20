using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarRangRazorPage.Models;
using StarRangRazorPage.Service;

namespace StarRangRazorPage.Pages.Portfolios
{
    public class IndexModel : PageModel
    {
        PortfolioServiceJsonFile _portfolioServiceJsonFile;
        public IndexModel(PortfolioServiceJsonFile portfolioServiceJsonFile)
        {
            this._portfolioServiceJsonFile = portfolioServiceJsonFile;
        }

        public IEnumerable<Portfolio> Portfolios { get; private set; }
 
        public void OnGet()
        {
            Portfolios = _portfolioServiceJsonFile.GetPortfolios();
        }

    }
}
