using FluentValidation;
using MediatR.Pipeline;

namespace App.WhoIsParking.UseCases.ParkedCars.Commands.Create;

internal class CreateParkedCarCommandPreProcessor : IRequestPreProcessor<CreateParkedCarCommand>
{
    private readonly CreateParkedCarValidator validator = new();

    public async Task Process(CreateParkedCarCommand request, CancellationToken token)
    => request.ValidationResult = await validator.ValidateAsync(request.ParkedCar, token)
            .ConfigureAwait(false);
}