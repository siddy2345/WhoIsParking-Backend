namespace App.WhoIsParking.UseCases.Houses.Queries.Common;

public class HouseReadResult : HouseReadAllResult
{
    public int Zip { get; set; }
    public string City { get; set; } = string.Empty;
}
