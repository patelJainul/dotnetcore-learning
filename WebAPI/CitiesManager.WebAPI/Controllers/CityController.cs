using ContactsManager.Core.DTO;
using ContactsManager.Core.Exceptions;
using ContactsManager.Core.Helpers;
using ContactsManager.Core.ServiceContracts.Cities;
using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController(
        ICityGetServices cityGetServices,
        ICityAddServices cityAddServices,
        ICityDeleteServices _cityDeleteServices,
        ICityUpdateServices _cityUpdateServices
    ) : ControllerBase
    {
        private readonly ICityGetServices _cityGetServices = cityGetServices;
        private readonly ICityAddServices _cityAddServices = cityAddServices;

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            try
            {
                return Ok(
                    (await _cityGetServices.GetAllCitiesAsync()).ToJsonResponse(
                        message: "Cities fetched successfully."
                    )
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToJsonResponse());
            }
        }

        [HttpGet("{cityId:guid?}")]
        public async Task<IActionResult> GetCityById(Guid? cityId)
        {
            try
            {
                return Ok(
                    (await _cityGetServices.GetCityByIdAsync(cityId))?.ToJsonResponse(
                        message: "City fetched successfully."
                    )
                );
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.ToJsonResponse());
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

        [HttpGet("{cityName}")]
        public async Task<IActionResult> GetCityByName(string cityName)
        {
            try
            {
                return Ok(
                    (await _cityGetServices.GetCitiesByNameAsync(cityName)).ToJsonResponse(
                        message: "City fetched successfully."
                    )
                );
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.ToJsonResponse());
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

        [HttpPost]
        public async Task<IActionResult> AddCity(CityAddRequest cityAddRequest)
        {
            try
            {
                return Ok(
                    (await _cityAddServices.AddCityAsync(cityAddRequest)).ToJsonResponse(
                        message: "City added successfully."
                    )
                );
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

        [HttpPut("{cityId:guid?}")]
        public async Task<IActionResult> UpdateCity(
            Guid? cityId,
            CityUpdateRequest cityUpdateRequest,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                return Ok(
                    (
                        await _cityUpdateServices.UpdateCityAsync(
                            cityId,
                            cityUpdateRequest,
                            cancellationToken
                        )
                    )?.ToJsonResponse(message: "City updated successfully.")
                );
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.ToJsonResponse());
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

        [HttpDelete("{cityId:guid?}")]
        public async Task<IActionResult> DeleteCity(Guid? cityId)
        {
            try
            {
                return Ok(await _cityDeleteServices.DeleteCityAsync(cityId));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.ToJsonResponse());
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
