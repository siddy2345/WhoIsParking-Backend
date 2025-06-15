using App.WhoIsParking.UseCases.Houses.Queries.Common;
using Ardalis.Result;
using MediatR;

namespace App.WhoIsParking.UseCases.Houses.Queries.GetById;

public record GetHouseCommand(int HouseId, Guid tenantId): IRequest<Result<HouseReadResult>>;