using Microsoft.AspNetCore.Mvc;

using ViewAssignmentWeather.Models;

namespace ViewAssignmentWeather.Controllers
{
    public class HomeController : Controller
    {
        readonly List<CityWeather> _cityWeathers = [
            new CityWeather { CityUniqueCode = "LDN", CityName = "London", DateAndTime = DateTime.Now, TempretureFahrenheit = 33 },
            new CityWeather { CityUniqueCode = "NYC", CityName = "New York", DateAndTime = DateTime.Now, TempretureFahrenheit = 60 },
            new CityWeather { CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = DateTime.Now, TempretureFahrenheit = 82 },
        ];

        readonly Dictionary<string, string> _bgcolor = new Dictionary<string, string> {
            { "blue", "bg-blue-300" },
            { "green", "bg-[#d7ff79]" },
            { "red", "bg-[#f5c448]" }
        };

        // GET: HomeController
        [Route("/")]
        [Route("weather")]
        public ActionResult Index()
        {
            ViewBag.bgcolor = _bgcolor;
            return View(_cityWeathers);
        }

        [Route("weather/{id}")]
        public ActionResult Weather(string id)
        {
            ViewBag.bgcolor = _bgcolor;
            var selectedCity = _cityWeathers.FirstOrDefault(c => c.CityUniqueCode == id);
            return selectedCity == null ? NotFound() : View(selectedCity);
        }


    }
}
