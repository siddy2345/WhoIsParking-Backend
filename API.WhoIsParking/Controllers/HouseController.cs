using API.WhoIsParking.Mapping;
using API.WhoIsParking.Models.House;
using API.WhoIsParking.UserClaims;
using App.WhoIsParking.UseCases.Houses.Commands.Create;
using App.WhoIsParking.UseCases.Houses.Commands.Update;
using App.WhoIsParking.UseCases.Houses.Queries.GetAll;
using App.WhoIsParking.UseCases.Houses.Queries.GetById;
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

[Route("api/houses")]
[ApiController]
public class HouseController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<HouseController> _logger;

    public HouseController(IMediator mediator, ILogger<HouseController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get house by id (only for Admins)
    /// </summary>
    /// <param name="houseId">Id to get</param>
    /// <param name="token">Token to cancel operation</param>
    /// <returns>Model</returns>
    /// <exception cref="Exception">InternalServerError if something went completely wrong in the application</exception>
    [HttpGet("{houseId:int}")]
    [Authorize(Roles = UserClaimsConstants.AdminRole)]
    [SwaggerResponseHeader(StatusCodes.Status200OK, "House retreived", nameof(HouseModel), "")]
    [SwaggerResponseHeader(StatusCodes.Status404NotFound, "House not found", "Not found", "")]
    [SwaggerResponseHeader(StatusCodes.Status401Unauthorized, "Unauthorized", "Unauthorized", "")]
    [SwaggerResponseHeader(StatusCodes.Status403Forbidden, "Forbidden", "Forbidden", "")]
    public async Task<ActionResult<HouseModel>> GetAsync([FromRoute, BindRequired] int houseId, CancellationToken token)
    {
        try
        {
            var tenantId = User.GetTenantId();

            if (tenantId == null)
                return Unauthorized();

            var command = new GetHouseCommand(houseId, tenantId.Value);

            var resultReadAllResult = await _mediator.Send(command, token).ConfigureAwait(false);

            if (resultReadAllResult.IsNotFound())
                return Result<HouseModel>.NotFound(resultReadAllResult.Errors.ToArray())
                    .ToActionResult(this);

            var viewModel = Result.Success(HouseMapping.MapToModel(resultReadAllResult.Value));

            return viewModel.ToActionResult(this);

        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Get houses (only for Admins)
    /// </summary>
    /// <param name="token">Token to cancel operation</param>
    /// <returns>ViewModels </returns>
    /// <exception cref="Exception">InternalServerError if something went completely wrong in the application</exception>
    [HttpGet]
    [Authorize(Roles = UserClaimsConstants.AdminRole)]
    [SwaggerResponseHeader(StatusCodes.Status200OK, "Houses retreived", nameof(List<HouseViewModel>), "")]
    [SwaggerResponseHeader(StatusCodes.Status400BadRequest, "Model is invalid", "BadRequest", "")]
    [SwaggerResponseHeader(StatusCodes.Status401Unauthorized, "Unauthorized", "Unauthorized", "")]
    [SwaggerResponseHeader(StatusCodes.Status403Forbidden, "Forbidden", "Forbidden", "")]
    public async Task<ActionResult<List<HouseViewModel>>> GetAllAsync(CancellationToken token)
    {
        var tenantId = User.GetTenantId();

        if (tenantId == null)
            return Unauthorized();

        return await GetHousesAsync(tenantId!.Value, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Get houses (for visitors)
    /// </summary>
    /// <param name="tenantId">Id of tenant to read houses by</param>
    /// <param name="token">Token to cancel operation</param>
    /// <returns>ViewModels </returns>
    /// <exception cref="Exception">InternalServerError if something went completely wrong in the application</exception>
    [HttpGet("{tenantId:guid}")]
    [SwaggerResponseHeader(StatusCodes.Status200OK, "Houses retreived", nameof(List<HouseViewModel>), "")]
    public async Task<ActionResult<List<HouseViewModel>>> GetAllAsync(Guid tenantId, CancellationToken token) //TenantId should be mapped with a mapping Table of QR CodeId and TenantId (so tenantId is never exposed outside backend)
    {
        return await GetHousesAsync(tenantId, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a house object (only for Admins)
    /// </summary>
    /// <param name="houseModel">The house to create</param>
    /// <param name="token">Token to cancel operation</param>
    /// <returns>Id of newly created object</returns>
    /// <exception cref="Exception">InternalServerError if something went completely wrong in the application</exception>
    [HttpPost]
    [Authorize(Roles = UserClaimsConstants.AdminRole)]
    [SwaggerResponseHeader(StatusCodes.Status201Created, "House successfully created", "Created", "")]
    [SwaggerResponseHeader(StatusCodes.Status400BadRequest, "House could not be created", "BadRequest", "")]
    [SwaggerResponseHeader(StatusCodes.Status401Unauthorized, "Unauthorized", "Unauthorized", "")]
    [SwaggerResponseHeader(StatusCodes.Status403Forbidden, "Forbidden", "Forbidden", "")]
    public async Task<ActionResult<int>> PostAsync([FromBody, BindRequired] HouseModel houseModel, CancellationToken token)
    {
        try
        {
            var tenantId = User.GetTenantId();

            if (tenantId == null)
                return Unauthorized();

            House house = HouseMapping.MapToDomainModel(houseModel);
            house.TenantId = tenantId!.Value;
            var command = new CreateHouseCommand(house);
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
    /// Updates a house object (only for Admins)
    /// </summary>
    /// <param name="houseId">The id of the house to update</param>
    /// <param name="houseModel">The house to update</param>
    /// <param name="token">Token to cancel operation</param>
    /// <returns>No content</returns>
    /// <exception cref="Exception">InternalServerError if something went completely wrong in the application</exception>
    [HttpPut("{houseId:int}")]
    [Authorize(Roles = UserClaimsConstants.AdminRole)]
    [SwaggerResponseHeader(StatusCodes.Status204NoContent, "House successfully updated", "Updated", "")]
    [SwaggerResponseHeader(StatusCodes.Status400BadRequest, "House could not be updated", "BadRequest", "")]
    [SwaggerResponseHeader(StatusCodes.Status401Unauthorized, "Unauthorized", "Unauthorized", "")]
    [SwaggerResponseHeader(StatusCodes.Status403Forbidden, "Forbidden", "Forbidden", "")]
    public async Task<ActionResult> PutAsync(int houseId, [FromBody, BindRequired] HouseModel houseModel, CancellationToken token)
    {
        if (houseModel.HouseId != houseId)
            return BadRequest("The id in the route does not match the id in the body");

        try
        {
            var tenantId = User.GetTenantId();

            if (tenantId == null)
                return Unauthorized();

            House house = HouseMapping.MapToDomainModel(houseModel);
            house.TenantId = tenantId!.Value;
            var command = new UpdateHouseCommand(house);
            Result response = await _mediator.Send(command, token).ConfigureAwait(false);

            return response.ToActionResult(this);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    #region Helper

    private async Task<ActionResult<List<HouseViewModel>>> GetHousesAsync(Guid tenantId, CancellationToken token)
    {
        try
        {
            var command = new GetAllHousesCommand(tenantId);

            var readAllResult = await _mediator.Send(command, token).ConfigureAwait(false);

            var viewModels = Result.Success(readAllResult.Value.Select(HouseMapping.MapToViewModel).ToList());

            return viewModels.ToActionResult(this);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    #endregion Helper
}
