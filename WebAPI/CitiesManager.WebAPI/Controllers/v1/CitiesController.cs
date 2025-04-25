using ContactsManager.Core.DTO.Cities;
using ContactsManager.Core.Exceptions;
using ContactsManager.Core.Helpers;
using ContactsManager.Core.ServiceContracts.Cities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.WebAPI.Controllers.v1;

/// <summary>
/// Handles HTTP requests related to
/// cities, including retrieving, adding, updating, and deleting city information.
/// </summary>
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class CitiesController(
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

    /// <summary>
    /// To retrieve all cities and return list of cities in a JSON response.
    /// </summary>
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

    /// <summary>
    /// To retrieves a city by its ID and returns a JSON response with the city's details.
    /// </summary>
    /// <param name="cityId">
    /// The `cityId` parameter is a `Guid` that represents the unique identifier of the city to be
    /// retrieved.
    /// </param>
    /// <param name="cancellationToken"></param>
    [HttpGet("{cityId:guid?}")]
    public async Task<ActionResult<JsonResponse<CityResponse>?>> GetCityById(
        Guid? cityId,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            return (
                await _cityGetServices.GetCityByIdAsync(cityId, cancellationToken)
            )?.ToJsonResponse(message: "City fetched successfully.");
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

    /// <summary>
    /// To retrieves a city by its name and returns a JSON response with the city's details.
    /// </summary>
    /// <param name="cityName">
    /// The `cityName` parameter is a string that represents the name of the city to be
    /// retrieved.
    /// </param>
    /// <param name="cancellationToken"></param>
    [HttpGet("{cityName}")]
    public async Task<ActionResult<JsonResponse<List<CityResponse>>>> GetCityByName(
        string cityName,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            return (
                await _cityGetServices.GetCitiesByNameAsync(cityName, cancellationToken)
            ).ToJsonResponse(message: "City fetched successfully.");
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


    /// <summary>
    /// To add a new city and returns a newly added city.
    /// </summary>
    /// <param name="cityAddRequest">
    /// The `cityAddRequest` parameter is an object of type `CityAddRequest` that contains the
    /// information needed to add a new city.
    /// </param>
    /// <param name="cancellationToken"></param>
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

    /// <summary>
    /// To updates an existing city by its ID and returns a updated city.
    /// </summary>
    /// <param name="cityId">
    /// The `cityId` parameter is a `Guid` that represents the unique identifier of the city to be
    /// updated.
    /// </param>
    /// <param name="cityUpdateRequest">
    /// The `cityUpdateRequest` parameter is an object of type `CityUpdateRequest` that contains the
    /// information needed to update an existing city.
    /// </param>
    /// <param name="cancellationToken"></param>
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

    /// <summary>
    /// To delete a city by its ID and returns a JSON response indicating success or failure.
    /// </summary>
    /// <param name="cityId">
    /// The `cityId` parameter is a nullable `Guid` that represents the unique identifier of the city to be
    /// deleted.
    /// </param>
    /// <param name="cancellationToken"></param>
    [HttpDelete("{cityId:guid?}")]
    public async Task<ActionResult<JsonResponse<bool>>> DeleteCity(
        Guid? cityId,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            return await _cityDeleteServices.DeleteCityAsync(cityId, cancellationToken);
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
