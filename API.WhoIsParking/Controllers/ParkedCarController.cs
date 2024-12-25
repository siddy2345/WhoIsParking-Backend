using API.WhoIsParking.Mapping;
using API.WhoIsParking.Models;
using App.WhoIsParking.UseCases.ParkedCars.Commands.Create;
using Domain.WhoIsParking.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace API.WhoIsParking.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParkedCarController : ControllerBase
{
    private readonly IMediator _mediator;

    public ParkedCarController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a parked car object
    /// </summary>
    /// <param name="parkedCarModel">The parked car to create</param>
    /// <param name="token">Token to cancel operation</param>
    /// <returns>Id of newly created object</returns>
    /// <exception cref="Exception">Bad request if the model did not meet the expectation</exception>
    [HttpPost]
    [SwaggerResponseHeader(StatusCodes.Status201Created, "Parked car successfully registerd", "Created","")]
    [SwaggerResponseHeader(StatusCodes.Status400BadRequest, "Parked car could not be registered", "BadRequest","")]
    public async Task<IActionResult> PostAsync(ParkedCarModel parkedCarModel, CancellationToken token)
    {
        try
        {
            ParkedCar parkedCar = ParkedCarMapping.MapToDomainModel(parkedCarModel);
            var command = new CreateParkedCarCommand(parkedCar);
            var response = await _mediator.Send<int>(command, token);

            var location = Url.Action(null, null, new { id = response }, Request.Scheme);
            return Created(location, new { id = response });
        }
        catch (Exception e)
        {

            return BadRequest(new { Error = e.Message });
        }

    }
}
