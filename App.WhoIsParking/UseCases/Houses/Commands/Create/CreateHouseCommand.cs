using Ardalis.Result;
using Domain.WhoIsParking.Models;
using FluentValidation.Results;
using MediatR;

namespace App.WhoIsParking.UseCases.Houses.Commands.Create;

public record CreateHouseCommand(House House) : IRequest<Result<int>>
{
    /// <summary>
    /// ValidationResult of the FluentValidation Validator
    /// </summary>
    internal ValidationResult? ValidationResult { get; set; }
}
