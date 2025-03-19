using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.Controllers
{
    public class AboutController : Controller
    {
        [Route("[controller]")]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        [Route("[controller]/Developer")]
        public IActionResult Developer()
        {
            ViewBag.Title = "Developer Profile";
            return View();
        }
    }
}
