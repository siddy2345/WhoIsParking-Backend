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
    public string Firstname { get; set; } = string.Empty;

    [MaxLength(100), Required]
    public string Lastname { get; set; } = string.Empty;

    [Required]
    public DateTime Arrival { get; set; }

    [Required]
    public string TimeZoneInfo { get; set; } = string.Empty;

    [Required]
    public Guid TenantId { get; init; } = Guid.CreateVersion7(); //TODO: must either be fed by frontend by adding it to the QR Code
                                                                 //or get it through House id if you want to show all houses in dropdwon for the user.
                                                                 //In that case you could send the tenantid as response for hous GETAll
                                                                 //or if tenantid should be "secret" somehow, then get it in the backend with an inexpensive db call

    [Required]
    public int HouseId { get; set; }

    public House? House { get; set; }
}