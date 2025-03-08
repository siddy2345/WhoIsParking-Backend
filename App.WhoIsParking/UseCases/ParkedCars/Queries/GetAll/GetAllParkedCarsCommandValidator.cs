using FluentValidation;

namespace App.WhoIsParking.UseCases.ParkedCars.Queries.GetAll;

internal class GetAllParkedCarsCommandValidator : AbstractValidator<GetAllParkedCarsCommand>
{
    public GetAllParkedCarsCommandValidator()
    {
        RuleFor(c => c.DateFrom)
                .GreaterThan(DateOnly.MinValue)
                .LessThanOrEqualTo(c => c.DateFrom);
        RuleFor(c => c.DateTo)
                .GreaterThan(c => c.DateFrom)
                .LessThanOrEqualTo(DateOnly.MaxValue);
        RuleFor(c => c.TenantId).NotEmpty().NotNull();
        RuleFor(c => c.HouseIds).NotEmpty().NotNull();
    }
}
