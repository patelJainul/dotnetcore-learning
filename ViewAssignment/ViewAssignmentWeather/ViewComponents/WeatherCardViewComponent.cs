using Microsoft.AspNetCore.Mvc;

using ViewAssignmentWeather.Models;

namespace ViewAssignmentWeather.ViewComponents;

public class WeatherCardViewComponent : ViewComponent
{
    async public Task<IViewComponentResult> InvokeAsync(CityWeather item)
    {
        return View(item);
    }

}
