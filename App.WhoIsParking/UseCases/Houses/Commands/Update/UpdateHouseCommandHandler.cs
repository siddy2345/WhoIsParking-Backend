using App.WhoIsParking.Interfaces.Repositories;
using Ardalis.Result;
using MediatR;

namespace App.WhoIsParking.UseCases.Houses.Commands.Update;

internal class UpdateHouseCommandHandler : IRequestHandler<UpdateHouseCommand, Result>
{
    private readonly IHouseRepository _houseRepository;

    public UpdateHouseCommandHandler(IHouseRepository houseRepository)
    {
        _houseRepository = houseRepository;
    }

    public async Task<Result> Handle(UpdateHouseCommand request, CancellationToken token)
    {
        if (request.ValidationResult != null && !request.ValidationResult.IsValid)
        {
            var validationErrors = request.ValidationResult.Errors
                .Select(e =>
                new ValidationError(e.PropertyName, e.ErrorMessage, e.ErrorCode, ValidationSeverity.Error));
            return Result.Invalid(validationErrors);
        }

        await _houseRepository.UpdateAsync(request.House, token).ConfigureAwait(false);
        return Result.NoContent();
    }
}
