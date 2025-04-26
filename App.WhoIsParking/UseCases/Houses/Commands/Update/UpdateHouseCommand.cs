using Ardalis.Result;
using Domain.WhoIsParking.Models;
using FluentValidation.Results;
using MediatR;

namespace App.WhoIsParking.UseCases.Houses.Commands.Update;

public record UpdateHouseCommand(House House) : IRequest<Result>
{
    /// <summary>
    /// ValidationResult of the FluentValidation Validator
    /// </summary>
    internal ValidationResult? ValidationResult { get; set; }
}
