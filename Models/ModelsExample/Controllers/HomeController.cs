using Microsoft.AspNetCore.Mvc;

namespace ModelsExample.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public IActionResult Index()
        {
            return Content("Hello from HomeController");
        }

    }
}
