using Domain.WhoIsParking.Models;
using FluentValidation;

namespace App.WhoIsParking.UseCases.ParkedCars.Commands.Create;

internal class CreateParkedCarValidator : AbstractValidator<ParkedCar>
{
    public CreateParkedCarValidator()
    {
        RuleFor(parkedCar => parkedCar.ParkedCarId).Equal(default(int)); // default of int is 0
        RuleFor(parkedCar => parkedCar.NumberPlate).NotEmpty();
        RuleFor(parkedCar => parkedCar.CarBrand).NotEmpty();
        RuleFor(parkedCar => parkedCar.Firstname).NotEmpty();
        RuleFor(parkedCar => parkedCar.Lastname).NotEmpty();
        RuleFor(parkedCar => parkedCar.Arrival)
            .GreaterThan(DateTimeOffset.MinValue)
            .LessThanOrEqualTo(DateTimeOffset.MaxValue);
        RuleFor(parkedCar => parkedCar.HouseId).GreaterThan(default(int)); 
    }
}