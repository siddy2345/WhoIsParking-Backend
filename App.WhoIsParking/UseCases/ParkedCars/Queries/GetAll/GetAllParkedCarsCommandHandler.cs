using App.WhoIsParking.Interfaces.Repositories;
using Ardalis.Result;
using MediatR;

namespace App.WhoIsParking.UseCases.ParkedCars.Queries.GetAll;

internal class GetAllParkedCarsCommandHandler : IRequestHandler<GetAllParkedCarsCommand, Result<IReadOnlyCollection<ParkedCarReadAllResult>>>
{
    private readonly IParkedCarRepository _parkedCarRepository;

    public GetAllParkedCarsCommandHandler(IParkedCarRepository parkedCarRepository)
    {
        _parkedCarRepository = parkedCarRepository;
    }

    public async Task<Result<IReadOnlyCollection<ParkedCarReadAllResult>>> Handle(GetAllParkedCarsCommand request, CancellationToken token)
    {
        var result = await _parkedCarRepository.ReadParkedCars(request, token).ConfigureAwait(false);

        return Result.Success(result);
    }
}
