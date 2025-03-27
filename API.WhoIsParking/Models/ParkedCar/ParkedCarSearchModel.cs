namespace API.WhoIsParking.Models.ParkedCar;

public class ParkedCarSearchModel
{
    public DateOnly DateFrom { get; set; }

    public DateOnly DateTo { get; set; }

    public required IReadOnlyCollection<int> HouseIds { get; set; }
}
