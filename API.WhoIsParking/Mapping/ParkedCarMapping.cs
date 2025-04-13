using API.WhoIsParking.Models.ParkedCar;
using App.WhoIsParking.UseCases.ParkedCars.Queries.GetAll;
using Domain.WhoIsParking.Models;

namespace API.WhoIsParking.Mapping;

public static class ParkedCarMapping
{
    public static ParkedCar MapToDomainModel(this ParkedCarModel parkedCar)
    {
        return new ParkedCar
        {
            ParkedCarId = parkedCar.ParkedCarId,
            Arrival = parkedCar.Arrival,
            TimeZoneInfo = parkedCar.TimeZoneInfo,
            CarBrand = parkedCar.CarBrand,
            NumberPlate = parkedCar.NumberPlate,
            Firstname = parkedCar.Firstname,
            Lastname = parkedCar.Lastname,
            HouseId = parkedCar.HouseId,
        };
    }

    public static ParkedCarViewModel MapToViewModel(this ParkedCarReadAllResult readAllResult)
    {
        return new ParkedCarViewModel
        {
            ParkedCarId = readAllResult.ParkedCarId,
            Arrival = readAllResult.Arrival,
            CarBrand = readAllResult.CarBrand,
            Firstname = readAllResult.Firstname,
            Lastname = readAllResult.Lastname,
            HouseAdress = readAllResult.HouseAdress,
            HouseNumber = readAllResult.HouseNumber,
            NumberPlate = readAllResult.NumberPlate,
            Zip = readAllResult.Zip
        };
    }
}
