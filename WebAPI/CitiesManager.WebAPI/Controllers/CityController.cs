using ContactsManager.Core.DTO.Cities;
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
        ICityDeleteServices cityDeleteServices,
        ICityUpdateServices cityUpdateServices
    ) : ControllerBase
    {
        private readonly ICityGetServices _cityGetServices = cityGetServices;
        private readonly ICityAddServices _cityAddServices = cityAddServices;

        private readonly ICityDeleteServices _cityDeleteServices = cityDeleteServices;
        private readonly ICityUpdateServices _cityUpdateServices = cityUpdateServices;

        // GET: api/city/GetAllCities
        [HttpGet]
        public async Task<ActionResult<JsonResponse<List<CityResponse>>>> GetAllCities()
        {
            try
            {
                return (await _cityGetServices.GetAllCitiesAsync()).ToJsonResponse(
                    message: "Cities fetched successfully."
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToJsonResponse());
            }
        }

        // GET: api/city/GetCityById/{cityId}
        [HttpGet("{cityId:guid?}")]
        public async Task<ActionResult<JsonResponse<CityResponse>?>> GetCityById(Guid? cityId)
        {
            try
            {
                return (await _cityGetServices.GetCityByIdAsync(cityId))?.ToJsonResponse(
                    message: "City fetched successfully."
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

        // GET: api/city/GetCityByName/{cityName}
        [HttpGet("{cityName}")]
        public async Task<ActionResult<JsonResponse<List<CityResponse>>>> GetCityByName(
            string cityName
        )
        {
            try
            {
                return (await _cityGetServices.GetCitiesByNameAsync(cityName)).ToJsonResponse(
                    message: "City fetched successfully."
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

        // POST: api/city/AddCity
        [HttpPost]
        public async Task<ActionResult<JsonResponse<CityResponse>>> AddCity(
            CityAddRequest cityAddRequest,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                return CreatedAtAction(
                    nameof(GetCityById),
                    (
                        await _cityAddServices.AddCityAsync(cityAddRequest, cancellationToken)
                    ).ToJsonResponse(message: "City added successfully.")
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

        // PUT: api/city/UpdateCity/{cityId}
        [HttpPut("{cityId:guid?}")]
        public async Task<ActionResult<JsonResponse<CityResponse>?>> UpdateCity(
            Guid? cityId,
            CityUpdateRequest cityUpdateRequest,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                return (
                    await _cityUpdateServices.UpdateCityAsync(
                        cityId,
                        cityUpdateRequest,
                        cancellationToken
                    )
                )?.ToJsonResponse(message: "City updated successfully.");
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

        // DELETE: api/city/DeleteCity/{cityId}
        [HttpDelete("{cityId:guid?}")]
        public async Task<ActionResult<JsonResponse<bool>>> DeleteCity(Guid? cityId)
        {
            try
            {
                return await _cityDeleteServices.DeleteCityAsync(cityId);
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
