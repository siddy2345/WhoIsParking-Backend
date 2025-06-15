using App.WhoIsParking.Interfaces.Repositories;
using App.WhoIsParking.UseCases.Houses.Queries.Common;
using Ardalis.Result;
using MediatR;

namespace App.WhoIsParking.UseCases.Houses.Queries.GetById;

internal class GetHouseCommandHandler : IRequestHandler<GetHouseCommand, Result<HouseReadResult>>
{
    private readonly IHouseRepository _houseRepository;

    public GetHouseCommandHandler(IHouseRepository houseRepository)
    {
        _houseRepository = houseRepository;
    }

    public async Task<Result<HouseReadResult>> Handle(GetHouseCommand request, CancellationToken token)
    {
        var result = await _houseRepository.ReadHouseById(request.HouseId, request.tenantId, token).ConfigureAwait(false);

        if (result == null) return Result.NotFound("Das Haus wurde leider nicht gefunden.");

        return Result.Success(result);
    }
}
