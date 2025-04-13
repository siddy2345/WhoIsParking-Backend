using Domain.WhoIsParking.Constants;
using Domain.WhoIsParking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.WhoIsParking.Data.EntitiesConfig;

public class DatabaseSeeder
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IConfiguration _configuration;

    public DatabaseSeeder(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task SeedAsync()
    {
        if (await _userManager.Users.AsNoTracking().AnyAsync().ConfigureAwait(false)) return;
        
        string? adminPassword = _configuration.GetValue<string>("AdminPassword");
        if (string.IsNullOrEmpty(adminPassword))
        {
            throw new InvalidOperationException(
                "Admin password is not configured. Please set 'AdminPassword' in user secrets (development) or environment variables (production).");
        }
        var tenantId = Guid.CreateVersion7();

        ApplicationUser adminUser = new()
        {
            Id = 1,
            UserName = "admin@admin.com",
            NormalizedUserName = "ADMIN@ADMIN.COM",
            Email = "admin@admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.CreateVersion7().ToString(),
            ConcurrencyStamp = Guid.CreateVersion7().ToString(),
            TenantId = tenantId
        };

        ApplicationRole adminRole = new()
        {
            Id = 1,
            Name = UserClaimsConstants.AdminRole,
            NormalizedName = UserClaimsConstants.AdminRole.ToUpper(),
            ConcurrencyStamp = Guid.CreateVersion7().ToString(),
            TenantId = tenantId
        };

        var resultRole = await _roleManager.CreateAsync(adminRole).ConfigureAwait(false);

        if (!resultRole.Succeeded)
        {
            throw new Exception("Failed to seed admin role: " +
                string.Join(", ", resultRole.Errors.Select(e => e.Description)));
        }

        var resultUser = await _userManager.CreateAsync(adminUser, adminPassword).ConfigureAwait(false);

        if (!resultUser.Succeeded)
        {
            throw new Exception("Failed to seed admin user: " +
                string.Join(", ", resultRole.Errors.Select(e => e.Description)));
        }

        var resultUserToRole = await _userManager.AddToRoleAsync(adminUser, adminRole.Name).ConfigureAwait(false);

        if (!resultUserToRole.Succeeded)
        {
            throw new Exception("Failed to seed admin user to admin role: " +
                string.Join(", ", resultRole.Errors.Select(e => e.Description)));
        }
    }
}
