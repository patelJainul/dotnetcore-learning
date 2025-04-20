using CitiesManager.Core.Domain.Entities;
using ContactsManager.Core.Helpers;

namespace ContactsManager.Core.DTO.Cities;

public class CityResponse
{
    public Guid CityId { get; set; }
    public string Name { get; set; } = string.Empty;

    public JsonResponse<CityResponse> ToJsonResponse(string? message = "")
    {
        return JsonResponse<CityResponse>.ToJsonResponse(this, message);
    }
}

public static class CityExtensions
{
    public static CityResponse ToCityResponse(this City city)
    {
        return new CityResponse { CityId = city.CityId, Name = city.Name };
    }
}
