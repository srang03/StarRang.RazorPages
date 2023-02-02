using Microsoft.AspNetCore.Mvc;
using MVC.Core._230127.Models;

namespace MVC.Core._230127.Controllers
{
    public class DogController : Controller
    {
        private static List<Dog> dogs = new List<Dog>();
        public IActionResult Index()
        {
            return View(dogs);
        }

        public IActionResult Create()
        {
            var dog = new Dog();
            return View(dog);
        }

        public IActionResult CreateDog(Dog dog)
        {
            dogs.Add(dog);
            return RedirectToAction(nameof(Index));
        }
    }
}
