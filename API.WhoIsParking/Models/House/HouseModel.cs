using System.ComponentModel.DataAnnotations;

namespace API.WhoIsParking.Models.House;

public class HouseModel
{
    public int HouseId { get; set; }

    [MaxLength(100), Required]
    public string Street { get; set; } = string.Empty;

    [MaxLength(20), Required]
    public string Number { get; set; } = string.Empty;

    [Required]
    public int Zip { get; set; }

    [MaxLength(100), Required]
    public string City { get; set; } = string.Empty;
}
