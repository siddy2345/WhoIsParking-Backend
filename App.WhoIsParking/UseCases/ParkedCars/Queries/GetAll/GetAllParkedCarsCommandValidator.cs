using FluentValidation;

namespace App.WhoIsParking.UseCases.ParkedCars.Queries.GetAll;

internal class GetAllParkedCarsCommandValidator : AbstractValidator<GetAllParkedCarsCommand>
{
    public GetAllParkedCarsCommandValidator()
    {
        RuleFor(c => c.DateFrom)
                .GreaterThanOrEqualTo(DateOnly.MinValue)
                .LessThanOrEqualTo(c => c.DateTo);
        RuleFor(c => c.DateTo)
                .LessThanOrEqualTo(DateOnly.MaxValue);
        RuleFor(c => c.TenantId).NotEmpty().NotNull();
        RuleFor(c => c.HouseIds).NotEmpty().NotNull();
    }
}
