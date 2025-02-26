using Microsoft.AspNetCore.Mvc;
using Models;
using ServiceContractors;

namespace ViewAssignmentWeather.Controllers
{
  public class HomeController : Controller
  {
    private readonly ICitiesWeatherServices _citiesServices;

    public HomeController(ICitiesWeatherServices citiesServices)
    {
      _citiesServices = citiesServices;
    }

    readonly Dictionary<string, string> _bgcolor = new Dictionary<string, string>
    {
      { "blue", "bg-blue-300" },
      { "green", "bg-[#d7ff79]" },
      { "red", "bg-[#f5c448]" },
    };

    // GET: HomeController
    [Route("/")]
    [Route("weather")]
    public ActionResult Index()
    {
      var cityWeathers = _citiesServices.GetCityWeathers();
      ViewBag.bgcolor = _bgcolor;
      return View(cityWeathers);
    }

    [Route("weather/{id}")]
    public ActionResult Weather(string id)
    {
      var cityWeather = _citiesServices.GetCityWeathersById(id);
      return cityWeather == null ? NotFound() : View(cityWeather);
    }
  }
}
