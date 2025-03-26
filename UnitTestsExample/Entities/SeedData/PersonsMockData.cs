namespace Entities.SeedData;

public class PersonsMockData
{
    public static List<Person> GetPersons()
    {
        return
        [
            new Person
            {
                PersonId = Guid.Parse("d00a0458-7794-4d9c-b6b8-7eb4c70f9188"),
                FirstName = "Malcolm",
                LastName = "Blanc",
                Email = "mblanc0@jigsy.com",
                DateOfBirth = DateTime.Parse("1984-04-01"),
                Gender = "Male",
                Address = "2443 Derek Way",
                CountryId = Guid.Parse("9de3383f-8f19-4457-99fc-55f1225f73ad"),
                ReceiveNewsLetters = false,
            },
            new Person
            {
                PersonId = Guid.Parse("2e539996-f778-4178-ac9c-6961d4b09e3e"),
                FirstName = "Perla",
                LastName = "Fishbourn",
                Email = "pfishbourn1@princeton.edu",
                DateOfBirth = DateTime.Parse("1974-09-01"),
                Gender = "Female",
                Address = "3847 Helena Point",
                CountryId = Guid.Parse("04be259f-882a-4b5b-a794-7d6163b455a0"),
                ReceiveNewsLetters = false,
            },
            new Person
            {
                PersonId = Guid.Parse("a7501f6d-59ba-40fd-888a-497b31feee33"),
                FirstName = "Lory",
                LastName = "Fuidge",
                Email = "lfuidge2@addtoany.com",
                DateOfBirth = DateTime.Parse("1985-05-27"),
                Gender = "Female",
                Address = "3873 Mallard Pass",
                CountryId = Guid.Parse("94841d04-3c8a-483e-8a7c-964e7c4ab980"),
                ReceiveNewsLetters = false,
            },
            new Person
            {
                PersonId = Guid.Parse("417287cc-d010-489c-bc03-18dfcf78ef61"),
                FirstName = "Fredelia",
                LastName = "Escalero",
                Email = "fescalero3@accuweather.com",
                DateOfBirth = DateTime.Parse("1987-04-11"),
                Gender = "Female",
                Address = "4758 Hintze Park",
                CountryId = Guid.Parse("329b11fe-ca2b-448f-9048-a88bf8a08bf4"),
                ReceiveNewsLetters = false,
            },
            new Person
            {
                PersonId = Guid.Parse("64518ec5-3dc7-4e62-8568-b4ead3acb1ac"),
                FirstName = "Boniface",
                LastName = "enzley",
                Email = "benzley4@tuttocitta.it",
                DateOfBirth = DateTime.Parse("1991-10-09"),
                Gender = "Male",
                Address = "83143 Meadow Valley Hill",
                CountryId = Guid.Parse("c9904974-2bb3-4006-a592-de8325b1a259"),
                ReceiveNewsLetters = true,
            },
            new Person
            {
                PersonId = Guid.Parse("d860aace-44d7-436e-864a-d04215322860"),
                FirstName = "Valle",
                LastName = "Poultney",
                Email = "vpoultney5@delicious.com",
                DateOfBirth = DateTime.Parse("1975-06-20"),
                Gender = "Male",
                Address = "42454 Gerald Crossing",
                CountryId = Guid.Parse("0c080288-b37d-4bfc-a6e7-0b5c27f15b58"),
                ReceiveNewsLetters = false,
            },
            new Person
            {
                PersonId = Guid.Parse("99ed9d3f-108b-4736-b585-bfece71a9052"),
                FirstName = "Garwin",
                LastName = "Wakenshaw",
                Email = "gwakenshaw6@ihg.com",
                DateOfBirth = DateTime.Parse("1995-05-08"),
                Gender = "Male",
                Address = "1 Sauthoff Lane",
                CountryId = Guid.Parse("4b6ed032-832b-4ced-9936-bcd23b990ab1"),
                ReceiveNewsLetters = false,
            },
            new Person
            {
                PersonId = Guid.Parse("7a6c6d56-f750-4ecf-96f9-a030dd88044e"),
                FirstName = "Iormina",
                LastName = "Pedrielli",
                Email = "ipedrielli7@mysql.com",
                DateOfBirth = DateTime.Parse("1976-03-15"),
                Gender = "Female",
                Address = "549 Bartelt Trail",
                CountryId = Guid.Parse("fdb3abe1-2a52-4257-a5e6-b308c6f6d8f2"),
                ReceiveNewsLetters = false,
            },
            new Person
            {
                PersonId = Guid.Parse("b15a98f1-7b80-4239-9d1e-197600a4d962"),
                FirstName = "Duffie",
                LastName = "Jahan",
                Email = "djahan8@cornell.edu",
                DateOfBirth = DateTime.Parse("1987-02-06"),
                Gender = "Male",
                Address = "4331 Hallows Pass",
                CountryId = Guid.Parse("7cb1b263-45ff-4fe7-bec7-a036d57fcf4b"),
                ReceiveNewsLetters = false,
            },
            new Person
            {
                PersonId = Guid.Parse("3396831c-f56a-4dc6-acd8-8427b0acd6a2"),
                FirstName = "Jose",
                LastName = "Rolf",
                Email = "jrolf9@europa.eu",
                DateOfBirth = DateTime.Parse("1989-12-16"),
                Gender = "Male",
                Address = "8685 Thierer Park",
                CountryId = Guid.Parse("d2f95305-4080-48b5-9815-041701f0dd8a"),
                ReceiveNewsLetters = false,
            },
        ];
    }
}
