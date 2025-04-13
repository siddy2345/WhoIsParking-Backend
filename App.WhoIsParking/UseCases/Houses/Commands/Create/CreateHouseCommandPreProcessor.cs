using Domain.WhoIsParking.Validators.HouseValidator.Create;
using MediatR.Pipeline;

namespace App.WhoIsParking.UseCases.Houses.Commands.Create;

internal class CreateHouseCommandPreProcessor : IRequestPreProcessor<CreateHouseCommand>
{
    private readonly CreateHouseValidator _validator = new();

    public async Task Process(CreateHouseCommand request, CancellationToken token)
        => request.ValidationResult = await _validator.ValidateAsync(request.House, token)
        .ConfigureAwait(false);
}
