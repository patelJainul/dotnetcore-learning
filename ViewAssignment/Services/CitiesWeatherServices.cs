using Models;

using ServiceContractors;

namespace Services;

public class CitiesWeatherServices : ICitiesWeatherServices
{
    private readonly List<CityWeather> _cityWeathers;

    public CitiesWeatherServices()
    {
        _cityWeathers =
        [
            new CityWeather {
                CityUniqueCode = "LDN",
                CityName = "London",
                DateAndTime = DateTime.Now,
                TempretureFahrenheit = 33
            },
            new CityWeather {
                CityUniqueCode = "NYC",
                CityName = "New York",
                DateAndTime = DateTime.Now,
                TempretureFahrenheit = 60
            },
            new CityWeather {
                CityUniqueCode = "PAR",
                CityName = "Paris",
                DateAndTime = DateTime.Now,
                TempretureFahrenheit = 82
            },
        ];
    }


    public List<CityWeather> GetCityWeathers()
    {
        return _cityWeathers;
    }

    public CityWeather? GetCityWeathersById(string CityUniqueCode)
    {
        return _cityWeathers.FirstOrDefault(c => c.CityUniqueCode == CityUniqueCode);
    }


}
