using Domain.WhoIsParking.Models;
using FluentValidation;

namespace Domain.WhoIsParking.Validators.HouseValidator.Update;

public class UpdateHouseValidator : AbstractValidator<House>
{
    public UpdateHouseValidator()
    {
        Include(new HouseRules());
        RuleFor(house => house.HouseId).GreaterThan(default(int));
    }
}
