using Domain.WhoIsParking.Models;
using FluentValidation;

namespace Domain.WhoIsParking.Validators.ParkedCarValidator.Create;

public class CreateParkedCarValidator : AbstractValidator<ParkedCar>
{
    public CreateParkedCarValidator()
    {
        Include(new ParkedCarValidator());
        RuleFor(parkedCar => parkedCar.ParkedCarId).Equal(default(int)); // default of int is 0
        RuleFor(parkedCar => parkedCar.HouseId).GreaterThan(default(int)); 
    }
}