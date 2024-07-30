using App.WhoIsParking.Interfaces.Repositories;
using MediatR;

namespace App.WhoIsParking.UseCases.ParkedCars.Commands.Create;

internal class CreateParkedCarCommandHandler : IRequestHandler<CreateParkedCarCommand, int>
{
    private readonly IParkedCarRepository _parkedCarRepository;

    public CreateParkedCarCommandHandler(IParkedCarRepository parkedCarRepository)
    {
        _parkedCarRepository = parkedCarRepository;
    }

    public async Task<int> Handle(CreateParkedCarCommand request, CancellationToken token)
    {
        var res = await _parkedCarRepository.AddAsync(request.ParkedCar, token).ConfigureAwait(false);

        return res.ParkedCarId;
    }
}
