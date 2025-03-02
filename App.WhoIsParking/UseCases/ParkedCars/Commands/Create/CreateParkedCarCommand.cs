using Ardalis.Result;
using Domain.WhoIsParking.Models;
using FluentValidation.Results;
using MediatR;

namespace App.WhoIsParking.UseCases.ParkedCars.Commands.Create;

public record CreateParkedCarCommand(ParkedCar ParkedCar) : IRequest<Result<int>>
{
    /// <summary>
    /// ValidationResult of the FluentValidation Validator
    /// </summary>
    internal ValidationResult? ValidationResult { get; set; }
}