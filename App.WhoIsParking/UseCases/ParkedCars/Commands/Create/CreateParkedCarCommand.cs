using Domain.WhoIsParking.Models;
using MediatR;

namespace App.WhoIsParking.UseCases.ParkedCars.Commands.Create;

public record CreateParkedCarCommand(ParkedCar ParkedCar) : IRequest;