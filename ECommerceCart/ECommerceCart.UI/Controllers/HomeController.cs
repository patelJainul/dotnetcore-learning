using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceCart.UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        [Authorize("User")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
