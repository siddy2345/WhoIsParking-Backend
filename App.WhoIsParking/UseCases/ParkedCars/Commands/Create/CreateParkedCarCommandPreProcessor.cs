using MediatR.Pipeline;

namespace App.WhoIsParking.UseCases.ParkedCars.Commands.Create;

internal class CreateParkedCarCommandPreProcessor : IRequestPreProcessor<CreateParkedCarCommand>
{
    public Task Process(CreateParkedCarCommand request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
        //throw new NotImplementedException();
    }
}