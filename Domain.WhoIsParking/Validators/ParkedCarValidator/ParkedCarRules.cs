using Domain.WhoIsParking.Models;
using FluentValidation;

namespace Domain.WhoIsParking.Validators.ParkedCarValidator;

internal class ParkedCarRules : AbstractValidator<ParkedCar>
{
    public ParkedCarRules()
    {
        RuleFor(parkedCar => parkedCar.NumberPlate).NotEmpty();
        RuleFor(parkedCar => parkedCar.CarBrand).NotEmpty();
        RuleFor(parkedCar => parkedCar.Firstname).NotEmpty();
        RuleFor(parkedCar => parkedCar.Lastname).NotEmpty();
        RuleFor(parkedCar => parkedCar.Arrival)
                .GreaterThan(DateTime.MinValue)
                .LessThanOrEqualTo(DateTime.MaxValue);
        RuleFor(parkedCar => parkedCar.TimeZoneInfo).NotEmpty();     
    }
}
