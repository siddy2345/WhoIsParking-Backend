using App.WhoIsParking.Interfaces.Repositories;
using Domain.WhoIsParking.Validators.ParkedCarValidator.Create;
using MediatR.Pipeline;

namespace App.WhoIsParking.UseCases.ParkedCars.Commands.Create;

internal class CreateParkedCarCommandPreProcessor : IRequestPreProcessor<CreateParkedCarCommand>
{
    private readonly CreateParkedCarValidator _validator = new();
    private readonly IHouseRepository _houseRepository;

    public CreateParkedCarCommandPreProcessor(IHouseRepository houseRepository)
    {
        _houseRepository = houseRepository;
    }

    public async Task Process(CreateParkedCarCommand request, CancellationToken token)
    {
        request.ValidationResult = await _validator.ValidateAsync(request.ParkedCar, token)
            .ConfigureAwait(false);

        // add tenantId from the house if validation succeeded
        if (request.ValidationResult != null && request.ValidationResult.IsValid)
        {
            var tenantId = await _houseRepository.ReadTenantIdByHouseId(request.ParkedCar.HouseId, token).ConfigureAwait(false);
            request.ParkedCar.TenantId = tenantId;
        }
    }
}