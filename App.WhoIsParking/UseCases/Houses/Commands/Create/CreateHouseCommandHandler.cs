using App.WhoIsParking.Interfaces.Repositories;
using Ardalis.Result;
using MediatR;

namespace App.WhoIsParking.UseCases.Houses.Commands.Create;

internal class CreateHouseCommandHandler : IRequestHandler<CreateHouseCommand, Result<int>>
{
    private readonly IHouseRepository _houseRepository;

    public CreateHouseCommandHandler(IHouseRepository houseRepository)
    {
        _houseRepository = houseRepository;
    }

    public async Task<Result<int>> Handle(CreateHouseCommand request, CancellationToken token)
    {
        if (request.ValidationResult != null && !request.ValidationResult.IsValid)
        {
            var validationErrors = request.ValidationResult.Errors
                .Select(e =>
                new ValidationError(e.PropertyName, e.ErrorMessage, e.ErrorCode, ValidationSeverity.Error));
            return Result.Invalid(validationErrors);
        }

        var result = await _houseRepository.AddAsync(request.House, token).ConfigureAwait(false);
        return Result.Success(result.HouseId);
    }
}
