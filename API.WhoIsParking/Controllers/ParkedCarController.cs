using API.WhoIsParking.Mapping;
using API.WhoIsParking.Models;
using App.WhoIsParking.UseCases.ParkedCars.Commands.Create;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Domain.WhoIsParking.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace API.WhoIsParking.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParkedCarController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

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
    public async Task<ActionResult<int>> PostAsync(ParkedCarModel parkedCarModel, CancellationToken token)
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
            //Log exception
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
