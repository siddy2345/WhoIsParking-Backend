using Ardalis.Result;
using MediatR;

namespace App.WhoIsParking.UseCases.ParkedCars.Queries.GetAll;

public record GetAllParkedCarsCommand(DateOnly DateFrom, DateOnly DateTo, Guid TenantId, IReadOnlyCollection<int> HouseIds) : IRequest<Result<IReadOnlyCollection<ParkedCarReadAllResult>>>;