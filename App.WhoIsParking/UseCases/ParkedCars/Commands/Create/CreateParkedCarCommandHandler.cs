using App.WhoIsParking.Interfaces.Repositories;
using MediatR;

namespace App.WhoIsParking.UseCases.ParkedCars.Commands.Create;

internal class CreateParkedCarCommandHandler : IRequestHandler<CreateParkedCarCommand>
{
    private readonly IParkedCarRepository _parkedCarRepository;

    public CreateParkedCarCommandHandler(IParkedCarRepository parkedCarRepository)
    {
        _parkedCarRepository = parkedCarRepository;
    }

    public async Task Handle(CreateParkedCarCommand request, CancellationToken token)
    {
        await _parkedCarRepository.AddAsync(request.ParkedCar, token).ConfigureAwait(false);
    }
}
