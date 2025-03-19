using Entities;

namespace Services.SeedData;

public class CountriesMockData
{
    public static List<Country> GetCountries()
    {
        return
        [
            new Country
            {
                CountryId = Guid.Parse("9de3383f-8f19-4457-99fc-55f1225f73ad"),
                CountryName = "Canada",
            },
            new Country
            {
                CountryId = Guid.Parse("04be259f-882a-4b5b-a794-7d6163b455a0"),
                CountryName = "Mexico",
            },
            new Country
            {
                CountryId = Guid.Parse("94841d04-3c8a-483e-8a7c-964e7c4ab980"),
                CountryName = "United States",
            },
            new Country
            {
                CountryId = Guid.Parse("329b11fe-ca2b-448f-9048-a88bf8a08bf4"),
                CountryName = "Brazil",
            },
            new Country
            {
                CountryId = Guid.Parse("c9904974-2bb3-4006-a592-de8325b1a259"),
                CountryName = "Argentina",
            },
            new Country
            {
                CountryId = Guid.Parse("0c080288-b37d-4bfc-a6e7-0b5c27f15b58"),
                CountryName = "Chile",
            },
            new Country
            {
                CountryId = Guid.Parse("4b6ed032-832b-4ced-9936-bcd23b990ab1"),
                CountryName = "Colombia",
            },
            new Country
            {
                CountryId = Guid.Parse("fdb3abe1-2a52-4257-a5e6-b308c6f6d8f2"),
                CountryName = "Ecuador",
            },
            new Country
            {
                CountryId = Guid.Parse("7cb1b263-45ff-4fe7-bec7-a036d57fcf4b"),
                CountryName = "Venezuela",
            },
            new Country
            {
                CountryId = Guid.Parse("d2f95305-4080-48b5-9815-041701f0dd8a"),
                CountryName = "Peru",
            },
        ];
    }
}
