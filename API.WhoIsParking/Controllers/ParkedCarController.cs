using API.WhoIsParking.Mapping;
using API.WhoIsParking.Models.ParkedCar;
using API.WhoIsParking.UserClaims;
using App.WhoIsParking.UseCases.ParkedCars.Commands.Create;
using App.WhoIsParking.UseCases.ParkedCars.Queries.GetAll;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Domain.WhoIsParking.Constants;
using Domain.WhoIsParking.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Filters;

namespace API.WhoIsParking.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParkedCarController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ParkedCarController> _logger;

    public ParkedCarController(IMediator mediator, ILogger<ParkedCarController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Creates a parked car object
    /// </summary>
    /// <param name="parkedCarModel">The parked car to create</param>
    /// <param name="token">Token to cancel operation</param>
    /// <returns>Id of newly created object</returns>
    /// <exception cref="Exception">InternalServerError if something went completely wrong in the application</exception>
    [HttpPost]
    [SwaggerResponseHeader(StatusCodes.Status201Created, "Parked car successfully registerd", "Created","")]
    [SwaggerResponseHeader(StatusCodes.Status400BadRequest, "Parked car could not be registered", "BadRequest","")]
    public async Task<ActionResult<int>> PostAsync([FromBody, BindRequired] ParkedCarModel parkedCarModel, CancellationToken token)
    {
        try
        {
            ParkedCar parkedCar = ParkedCarMapping.MapToDomainModel(parkedCarModel);
            var command = new CreateParkedCarCommand(parkedCar);
            Result<int> response = await _mediator.Send(command, token).ConfigureAwait(false);

            return response.ToActionResult(this);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Get ParkedCars (only for Admins)
    /// </summary>
    /// <param name="searchModel">Search model to get parked cars by</param>
    /// <param name="token">Token to cancel operation</param>
    /// <returns>Id of newly created object</returns>
    /// <exception cref="Exception">InternalServerError if something went completely wrong in the application</exception>
    [HttpPost("search")]
    [Authorize(Roles = UserClaimsConstants.AdminRole)]
    [SwaggerResponseHeader(StatusCodes.Status201Created, "Parked cars retreived", nameof(List<ParkedCarViewModel>), "")]
    [SwaggerResponseHeader(StatusCodes.Status400BadRequest, "Model is invalid", "BadRequest", "")]
    [SwaggerResponseHeader(StatusCodes.Status401Unauthorized, "Unauthorized", "Unauthorized", "")]
    [SwaggerResponseHeader(StatusCodes.Status403Forbidden, "Forbidden", "Forbidden", "")]
    public async Task<ActionResult<List<ParkedCarViewModel>>> GetAllAsync([FromBody, BindRequired] ParkedCarSearchModel searchModel, CancellationToken token)
    {
        try
        {   
            var tenantId = User.GetTenantId();

            if(tenantId == null) 
                return Unauthorized();

            var command = new GetAllParkedCarsCommand(
                searchModel.DateFrom, searchModel.DateTo, tenantId.Value, searchModel.HouseIds);

            var resultReadAllResult = await _mediator.Send(command, token).ConfigureAwait(false);

            var viewModels = Result.Success(resultReadAllResult.Value.Select(ParkedCarMapping.MapToViewModel).ToList());

            return viewModels.ToActionResult(this);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
