using ContactsManager.Core.DTO;
using ContactsManager.Core.Enums;
using ContactsManager.Core.Exceptions;
using ContactsManager.Core.Helper;
using ContactsManager.Core.ServiceContracts.Cities;
using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController(ICityGetServices cityGetServices, ICityAddServices cityAddServices)
        : ControllerBase
    {
        private readonly ICityGetServices _cityGetServices = cityGetServices;
        private readonly ICityAddServices _cityAddServices = cityAddServices;

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            try
            {
                return Ok((await _cityGetServices.GetAllCitiesAsync()).ToJsonResponse());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToJsonResponse());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(CityAddRequest cityAddRequest)
        {
            try
            {
                return Ok((await _cityAddServices.AddCityAsync(cityAddRequest)).ToJsonResponse());
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.ToJsonResponse());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.ToJsonResponse());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToJsonResponse());
            }
        }
    }
}
