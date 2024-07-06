using System.ComponentModel.DataAnnotations;

namespace Domain.WhoIsParking.Models;

public class ParkedCar
{
    [Required]
    public int ParkedCarId { get; set; }

    [MaxLength(20), Required]
    public string NumberPlate { get; set; } = string.Empty;

    [MaxLength(100), Required]
    public string CarBrand { get; set; } = string.Empty;

    [MaxLength(100), Required]
    public string CarModel { get; set; } = string.Empty;

    [MaxLength(100), Required]
    public string Firstname { get; set; } = string.Empty;

    [MaxLength(100), Required]
    public string Lastname { get; set; } = string.Empty;

    [Required]
    public DateTime Arrival { get; set; }

    [Required]
    public int HouseId { get; set; }

    public House? House { get; set; }
}