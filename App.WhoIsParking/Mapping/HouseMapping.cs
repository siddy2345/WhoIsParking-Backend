using Domain.WhoIsParking.Models;

namespace App.WhoIsParking.Mapping;

public static class HouseMapping
{
    public static void MapToOriginal(this House destination, House source)
    {
        destination.HouseId = source.HouseId;
        destination.City = source.City;
        destination.Number = source.Number;
        destination.Street = source.Street;
        destination.Zip = source.Zip;
    }
}
