using MediatR.Pipeline;

namespace App.WhoIsParking.UseCases.ParkedCars.Queries.GetAll;

internal class GetAllParkedCarsPreProcessor : IRequestPreProcessor<GetAllParkedCarsCommand>
{
    private readonly GetAllParkedCarsCommandValidator validator = new();

    public async Task Process(GetAllParkedCarsCommand request, CancellationToken token)
        => await validator.ValidateAsync(request, token).ConfigureAwait(false);
}
