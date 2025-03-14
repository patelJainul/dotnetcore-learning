using Entities;

namespace ServiceContracts.DTO;

public class CountryResponse
{
    public Guid CountryId { get; set; }
    public required string CountryName { get; set; }

    /// <summary>
    /// The Equals method in C# is overridden to compare CountryResponse objects based on their CountryId
    /// and CountryName properties.
    /// </summary>
    /// <param name="obj">The `obj` parameter in the `Equals` method is of type `object` and represents the
    /// object to compare with the current instance of `CountryResponse` to determine if they are
    /// equal.</param>
    /// <returns>
    /// The Equals method is being overridden to compare the CountryId and CountryName properties of the
    /// current CountryResponse object with the corresponding properties of the object passed in as a
    /// parameter. If the passed-in object is null or is not of type CountryResponse, false is returned.
    /// Otherwise, true is returned if both the CountryId and CountryName properties match, and false
    /// otherwise.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
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
