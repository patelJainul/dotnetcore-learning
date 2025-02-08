using ControllersExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    public class HomeController : Controller
    {
        // [Route("/")]
        // GET: HomeController
        public IActionResult Index()
        {
            // return new ContentResult()
            // {
            //     Content = "Hello from Home Controller",
            //     ContentType = "text/plain"
            // };

            // Person person = new(Guid.NewGuid(), "John Doe ");
            // return Json(person);

            return RedirectToAction("Index", nameof(Store));
        }

    }
}
