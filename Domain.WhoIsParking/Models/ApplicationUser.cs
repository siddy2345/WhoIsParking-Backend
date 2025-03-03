using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Domain.WhoIsParking.Models;

public class ApplicationUser : IdentityUser<int>
{
    [Required]
    public Guid TenantId { get; init; } = Guid.CreateVersion7();
}