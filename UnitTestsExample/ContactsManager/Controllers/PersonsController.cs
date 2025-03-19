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
            ViewBag.Title = "Persons List";
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();

            // Get all persons with search and sort parameters
            var persons = _personsService.GetPersons(searchBy, searchString, sortBy, sortOrder);

            // Add info toast notification when search is performed
            if (!string.IsNullOrEmpty(searchBy) && !string.IsNullOrEmpty(searchString))
            {
                TempData["InfoMessage"] =
                    $"Showing search results for '{searchString}' in {searchBy}";
            }

            return View(persons);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Create()
        {
            // Get all countries for the dropdown
            ViewBag.Title = "Create Person";
            ViewBag.Countries = _countriesService.GetCountries();
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Create(PersonAddRequest personAddRequest)
        {
            ViewBag.Title = "Create Person";
            if (!ModelState.IsValid)
            {
                // If validation fails, get countries again for the dropdown
                ViewBag.Countries = _countriesService.GetCountries();
                // Add error toast notification for validation errors
                TempData["ErrorMessage"] = "Please correct the validation errors.";
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

                // Add success toast notification
                TempData["SuccessMessage"] =
                    $"Person '{personResponse.FirstName} {personResponse.LastName}' created successfully.";

                // Redirect to Index page
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("CountryId", ex.Message);
                ViewBag.Countries = _countriesService.GetCountries();
                // Add error toast notification
                TempData["ErrorMessage"] = ex.Message;
                return View(personAddRequest);
            }
        }

        [HttpGet]
        [Route("[action]/{personId}")]
        public IActionResult Edit(Guid personId)
        {
            ViewBag.Title = "Edit Person";
            // Get the person by ID
            PersonResponse? personResponse = _personsService.GetPersonById(personId);
            if (personResponse == null)
            {
                // Add error toast notification
                TempData["ErrorMessage"] = "Person not found.";
                return RedirectToAction(nameof(Index));
            }

            // Convert PersonResponse to PersonUpdateRequest
            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();

            // Get all countries for the dropdown
            ViewBag.Countries = _countriesService.GetCountries();

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
                // Add error toast notification
                TempData["ErrorMessage"] = "Please correct the validation errors.";
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

                // Add success toast notification
                TempData["SuccessMessage"] =
                    $"Person '{personResponse.FirstName} {personResponse.LastName}' updated successfully.";

                // Redirect to Index page
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("CountryId", ex.Message);
                ViewBag.Countries = _countriesService.GetCountries();
                // Add error toast notification
                TempData["ErrorMessage"] = ex.Message;
                return View(personUpdateRequest);
            }
        }

        [HttpGet]
        [Route("[action]/{personId}")]
        public IActionResult Delete(Guid personId)
        {
            ViewBag.Title = "Delete Person";
            // Get the person by ID
            PersonResponse? personResponse = _personsService.GetPersonById(personId);
            if (personResponse == null)
            {
                // Add error toast notification
                TempData["ErrorMessage"] = "Person not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(personResponse);
        }

        [HttpPost]
        [Route("[action]/{personId}")]
        public IActionResult Delete(Guid personId, PersonResponse personResponse)
        {
            ViewBag.Title = "Delete Person";
            try
            {
                // Delete the person
                PersonResponse deletedPerson = _personsService.DeletePerson(personId);

                // Add success toast notification
                TempData["SuccessMessage"] =
                    $"Person '{deletedPerson.FirstName} {deletedPerson.LastName}' deleted successfully.";

                // Redirect to Index page
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Add error toast notification
                TempData["ErrorMessage"] = $"Error deleting person: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
