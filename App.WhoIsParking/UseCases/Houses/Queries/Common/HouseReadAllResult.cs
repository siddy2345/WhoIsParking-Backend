﻿namespace App.WhoIsParking.UseCases.Houses.Queries.Common;

public class HouseReadAllResult
{
    public int HouseId { get; set; }
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
}
