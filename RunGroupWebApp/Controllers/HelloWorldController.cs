using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Controllers
{
    public class HelloWorldController : Controller
    {
        private static List<DogViewModel> dvms = new List<DogViewModel>();

        public IActionResult Index()
        {
            return View(dvms);
        }

        public string Hello()
        {
            return "Hello World!";
        }

        public IActionResult Create()
        {
            var dvm = new DogViewModel();
            return View(dvm);
        }

        public IActionResult CreateDog(DogViewModel dvm)
        {
            //return View("Index"); // Index.html 페이지만 보여줌
            dvms.Add(dvm);
            return RedirectToAction(nameof(Index)); // Index 메서드로 이동
        }
    }
}
