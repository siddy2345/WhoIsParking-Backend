using API.WhoIsParking.Models;
using App.WhoIsParking.UseCases.ParkedCars.Commands.Create;
using Domain.WhoIsParking.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
    public async Task<IActionResult> PostAsync(ParkedCarModel parkedCarModel, CancellationToken token)
    {
        try
        {
            ParkedCar parkedCar = new ParkedCar()
            {
                Arrival = parkedCarModel.Arrival,
                CarBrand = parkedCarModel.CarBrand,
                CarModel = parkedCarModel.CarModel,
                Firstname = parkedCarModel.Firstname,
                Lastname = parkedCarModel.Lastname,
                NumberPlate = parkedCarModel.NumberPlate,
                HouseId = parkedCarModel.HouseId
            };

            var command = new CreateParkedCarCommand(parkedCar);
            var res = await _mediator.Send<int>(command, token);

        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
        
        return Created();
    }
}
