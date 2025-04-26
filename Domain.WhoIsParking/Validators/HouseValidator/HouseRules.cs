using Domain.WhoIsParking.Models;
using FluentValidation;

namespace Domain.WhoIsParking.Validators.HouseValidator;

internal class HouseRules : AbstractValidator<House>
{
    public HouseRules()
    {
        RuleFor(house => house.City).NotEmpty();
        RuleFor(house => house.Zip).GreaterThan(default(int));
        RuleFor(house => house.Street).NotEmpty();
        RuleFor(house => house.Number).NotEmpty();
    }
}
