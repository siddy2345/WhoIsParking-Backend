using API.WhoIsParking.Models;
using Domain.WhoIsParking.Models;

namespace API.WhoIsParking.Mapping
{
    public static class ParkedCarMapping
    {
        public static ParkedCar MapToDomainModel(this ParkedCarModel parkedCar)
        {
            return new ParkedCar() 
            {
                ParkedCarId = parkedCar.ParkedCarId,
                Arrival = parkedCar.Arrival,
                CarBrand = parkedCar.CarBrand,
                CarModel = parkedCar.CarModel,
                NumberPlate = parkedCar.NumberPlate,
                Firstname = parkedCar.Firstname,
                Lastname = parkedCar.Lastname,
                HouseId = parkedCar.HouseId,
            };
        }
    }
}
