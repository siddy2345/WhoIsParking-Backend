using API.WhoIsParking.Models.House;
using App.WhoIsParking.UseCases.Houses.Queries.GetAll;
using Domain.WhoIsParking.Models;

namespace API.WhoIsParking.Mapping;

public static class HouseMapping
{
    public static House MapToDomainModel(this HouseModel houseModel)
    {
        return new House()
        {
            HouseId = houseModel.HouseId,
            City = houseModel.City,
            Number = houseModel.Number,
            Street = houseModel.Street,
            Zip = houseModel.Zip
        };
    }

    public static HouseViewModel MapToViewModel(this HouseReadAllResult houseReadAllResult)
    {
        return new HouseViewModel()
        {
            HouseId = houseReadAllResult.HouseId,
            Number = houseReadAllResult.Number,
            Street = houseReadAllResult.Street,
        };
    }
}
