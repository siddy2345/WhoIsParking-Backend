using App.WhoIsParking.Interfaces.Repositories;
using App.WhoIsParking.Mapping;
using Domain.WhoIsParking.Validators.HouseValidator.Update;
using FluentValidation;
using MediatR.Pipeline;

namespace App.WhoIsParking.UseCases.Houses.Commands.Update;

internal class UpdateHouseCommandPreProcessor : IRequestPreProcessor<UpdateHouseCommand>
{
    private readonly UpdateHouseValidator _validator = new();

    private readonly IHouseRepository _houseRepository;

    public UpdateHouseCommandPreProcessor(IHouseRepository houseRepository)
    {
        _houseRepository = houseRepository;
    }

    public async Task Process(UpdateHouseCommand request, CancellationToken token)
    {
        request.ValidationResult = await _validator.ValidateAsync(request.House, token)
        .ConfigureAwait(false);

        if (request.ValidationResult != null && !request.ValidationResult.IsValid) 
            return;

        var originalBooking = await _houseRepository.GetAggregateAsync(request.House.HouseId, token).ConfigureAwait(false);

        if (originalBooking == null) 
            throw new ValidationException("Das angegebene Haus wurde nicht gefunden.");

        originalBooking.MapToOriginal(request.House);
    }
}
