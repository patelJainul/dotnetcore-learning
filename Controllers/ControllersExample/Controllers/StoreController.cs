using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    [Controller]
    public class Store : Controller
    {
        // GET: StoreController
        public IActionResult Index()
        {
            return Content("Hello from Store Controller", "text/plain");
        }

    }
}
