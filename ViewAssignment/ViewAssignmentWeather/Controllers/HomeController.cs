using Microsoft.AspNetCore.Mvc;
using ViewAssignmentWeather.Models;

namespace ViewAssignmentWeather.Controllers
{
    public class HomeController : Controller
    {
        readonly List<CityWeather> cityWeathers = [
            new CityWeather { CityUniqueCode = "LDN", CityName = "London", DateAndTime = DateTime.Now, TempretureFahrenheit = 33 },
            new CityWeather { CityUniqueCode = "NYC", CityName = "New York", DateAndTime = DateTime.Now, TempretureFahrenheit = 60 },
            new CityWeather { CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = DateTime.Now, TempretureFahrenheit = 82 },
        ];
        // GET: HomeController
        [Route("/")]
        [Route("weather")]
        public ActionResult Index()
        {
            return View(cityWeathers);
        }

        [Route("weather/{id}")]
        public ActionResult Weather(string id)
        {
            var selectedCity = cityWeathers.FirstOrDefault(c => c.CityUniqueCode == id);
            if (selectedCity == null)
            {
                return NotFound();
            }
            return View(selectedCity);
        }


    }
}
