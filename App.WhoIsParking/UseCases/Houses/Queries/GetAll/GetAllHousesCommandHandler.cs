using App.WhoIsParking.Interfaces.Repositories;
using Ardalis.Result;
using MediatR;

namespace App.WhoIsParking.UseCases.Houses.Queries.GetAll;

internal class GetAllHousesCommandHandler : IRequestHandler<GetAllHousesCommand, Result<IReadOnlyCollection<HouseReadAllResult>>>
{
    private readonly IHouseRepository _houseRepository;

    public GetAllHousesCommandHandler(IHouseRepository houseRepository)
    {
        _houseRepository = houseRepository;
    }

    public async Task<Result<IReadOnlyCollection<HouseReadAllResult>>> Handle(GetAllHousesCommand request, CancellationToken token)
    {
        var result = await _houseRepository.ReadHousesByTenant(request.TenantId, token).ConfigureAwait(false);

        return Result.Success(result);
    }
}
