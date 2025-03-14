using Entities;

namespace ServiceContracts.DTO;

public class CountryResponse
{
    public Guid CountryId { get; set; }
    public required string CountryName { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || typeof(CountryResponse) != obj.GetType())
        {
            return false;
        }
        return CountryId == (obj as CountryResponse)?.CountryId
            && CountryName == (obj as CountryResponse)?.CountryName;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public static class CountryExtensions
{
    public static CountryResponse ToCountryResponse(this Country country)
    {
        return new CountryResponse
        {
            CountryId = country.CountryId,
            CountryName = country.CountryName,
        };
    }
}
