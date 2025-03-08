namespace App.WhoIsParking.UseCases.ParkedCars.Queries.GetAll;

public class ParkedCarReadAllResult
{
    public int ParkedCarId { get; set; }

    public string Firstname { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;

    public string CarBrand { get; set; } = string.Empty;

    public string NumberPlate { get; set; } = string.Empty;

    public DateTime Arrival { get; set; }

    public string HouseAdress { get; set; } = string.Empty;

    public string HouseNumber { get; set; } = string.Empty;

    public int Zip { get; set; }
}