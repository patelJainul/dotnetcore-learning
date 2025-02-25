using Models;

namespace ServiceContractors;

public interface ICitiesWeatherServices
{

    public List<CityWeather> GetCityWeathers();
    public CityWeather? GetCityWeathersById(string CityUniqueCode);

}
