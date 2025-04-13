﻿using Domain.WhoIsParking.Models;
using FluentValidation;

namespace Domain.WhoIsParking.Validators.HouseValidator.Create;

public class CreateHouseValidator : AbstractValidator<House>
{
    public CreateHouseValidator()
    {
        Include(new HouseValidator());
        RuleFor(house => house.HouseId).Equal(default(int));
    }
}
