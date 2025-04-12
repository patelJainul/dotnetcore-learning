using CitiesManager.Core.Domain.Entities;
using ContactsManager.Core.Helper;

namespace ContactsManager.Core.DTO;

public class CityResponse
{
    public Guid CityId { get; set; }
    public string Name { get; set; } = string.Empty;

    public JsonResponse<CityResponse> ToJsonResponse()
    {
        return JsonResponse<CityResponse>.ToJsonResponse(this);
    }
}

public static class CityExtensions
{
    public static CityResponse ToCityResponse(this City city)
    {
        return new CityResponse { CityId = city.CityId, Name = city.Name };
    }
}
