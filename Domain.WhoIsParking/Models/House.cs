using System.ComponentModel.DataAnnotations;

namespace Domain.WhoIsParking.Models;

public class House
{
    [Required]
    public int HouseId { get; set; }

    [MaxLength(100), Required]
    public string Street {  get; set; } = string.Empty;

    [MaxLength(20), Required]
    public string Number { get; set; } = string.Empty;

    [Required]
    public int Zip { get; set; }

    [MaxLength(100), Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public Guid TenantId { get; init; } = Guid.CreateVersion7();

    public IEnumerable<ParkedCar> ParkedCars { get; set; } = Enumerable.Empty<ParkedCar>();
}
