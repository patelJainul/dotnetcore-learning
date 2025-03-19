using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ContactsManager.Controllers
{
    [Route("[controller]")]
    public class PersonsController(
        IPersonsServices personsService,
        ICountriesServices countriesService
    ) : Controller
    {
        private readonly IPersonsServices _personsService = personsService;
        private readonly ICountriesServices _countriesService = countriesService;

        [Route("[action]")]
        [Route("/")]
        public IActionResult Index(
            string searchBy,
            string? searchString,
            string sortBy = nameof(PersonResponse.FirstName),
            SortOptions sortOrder = SortOptions.Ascending
        )
        {
            // Store the search and sort parameters in ViewBag for the view to use
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();
            ViewBag.Title = "Persons List";

            // Get all persons with search and sort parameters
            var persons = _personsService.GetPersons(searchBy, searchString, sortBy, sortOrder);

            return View(persons);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Create()
        {
            // Get all countries for the dropdown
            ViewBag.Countries = _countriesService.GetCountries();
            ViewBag.Title = "Create Person";
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, get countries again for the dropdown
                ViewBag.Countries = _countriesService.GetCountries();
                return View(personAddRequest);
            }

            // Check if CountryId is empty guid
            if (personAddRequest.CountryId == Guid.Empty)
            {
                personAddRequest.CountryId = null;
            }

            try
            {
                // Add the person
                PersonResponse personResponse = _personsService.AddPerson(personAddRequest);

                // Redirect to Index page
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("CountryId", ex.Message);
                ViewBag.Countries = _countriesService.GetCountries();
                return View(personAddRequest);
            }
        }

        [HttpGet]
        [Route("[action]/{personId}")]
        public IActionResult Edit(Guid personId)
        {
            // Get the person by ID
            PersonResponse? personResponse = _personsService.GetPersonById(personId);
            if (personResponse == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Convert PersonResponse to PersonUpdateRequest
            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();

            // Get all countries for the dropdown
            ViewBag.Countries = _countriesService.GetCountries();
            ViewBag.Title = "Edit Person";

            return View(personUpdateRequest);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Edit(PersonUpdateRequest personUpdateRequest)
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, get countries again for the dropdown
                ViewBag.Countries = _countriesService.GetCountries();
                return View(personUpdateRequest);
            }

            // Check if CountryId is empty guid or doesn't exist
            if (personUpdateRequest.CountryId == Guid.Empty)
            {
                personUpdateRequest.CountryId = null;
            }

            try
            {
                // Update the person
                PersonResponse personResponse = _personsService.UpdatePerson(personUpdateRequest);

                // Redirect to Index page
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("CountryId", ex.Message);
                ViewBag.Countries = _countriesService.GetCountries();
                return View(personUpdateRequest);
            }
        }

        [HttpGet]
        [Route("[action]/{personId}")]
        public IActionResult Delete(Guid personId)
        {
            // Get the person by ID
            PersonResponse? personResponse = _personsService.GetPersonById(personId);
            if (personResponse == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Title = "Delete Person";
            return View(personResponse);
        }

        [HttpPost]
        [Route("[action]/{personId}")]
        public IActionResult Delete(Guid personId, PersonResponse personResponse)
        {
            // Delete the person
            PersonResponse deletedPerson = _personsService.DeletePerson(personId);

            // Redirect to Index page
            return RedirectToAction(nameof(Index));
        }
    }
}
