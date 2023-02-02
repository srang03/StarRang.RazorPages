using Microsoft.AspNetCore.Mvc;

namespace DotNetNote230202.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
            //return Content("Demo Page2");
        }
    }
}
