using Microsoft.AspNetCore.Mvc;
using ModelsExample.Models;

namespace ModelsExample.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public IActionResult Index(Person person)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = [.. ModelState.Values.SelectMany(value => value.Errors.Select(error => error.ErrorMessage))];

                return BadRequest(errors);
            }
            return Json(person);
        }

    }
}
