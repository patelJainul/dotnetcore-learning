using Microsoft.AspNetCore.Mvc;
using ModelsAssignmetEcommerce.Models;

namespace ModelsAssignmetEcommerce.Controllers
{
    public class OrderController : Controller
    {
        // GET: OrderController
        [HttpPost]
        public ActionResult Index(Order order)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return Content(string.Join(", ", errors));
            }

            return Json(new
            {
                orderNo = order.OrderNo
            });
        }

    }
}
