using App.WhoIsParking.Interfaces.Repositories;
using Ardalis.Result;
using MediatR;

namespace App.WhoIsParking.UseCases.ParkedCars.Commands.Create;

internal class CreateParkedCarCommandHandler : IRequestHandler<CreateParkedCarCommand, Result<int>>
{
    private readonly IParkedCarRepository _parkedCarRepository;

    public CreateParkedCarCommandHandler(IParkedCarRepository parkedCarRepository)
    {
        _parkedCarRepository = parkedCarRepository;
    }

    public async Task<Result<int>> Handle(CreateParkedCarCommand request, CancellationToken token)
    {
        if(request.ValidationResult != null && !request.ValidationResult.IsValid)
        {
            var validationErrors = request.ValidationResult.Errors
                .Select(e => 
                new ValidationError(e.PropertyName, e.ErrorMessage, e.ErrorCode, ValidationSeverity.Error));
            return Result.Invalid(validationErrors);
        }

        var res = await _parkedCarRepository.AddAsync(request.ParkedCar, token).ConfigureAwait(false);
        return Result.Success(res.ParkedCarId);
    }
}
