using Ardalis.Result;
using MediatR;

namespace App.WhoIsParking.UseCases.Houses.Queries.GetAll;

public record GetAllHousesCommand(Guid TenantId): IRequest<Result<IReadOnlyCollection<HouseReadAllResult>>>;