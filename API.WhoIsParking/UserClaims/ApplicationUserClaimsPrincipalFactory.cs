using Domain.WhoIsParking.Constants;
using Domain.WhoIsParking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace API.WhoIsParking.UserClaims;
/// <summary>
/// Creates the Claim and adds it, everytime a user logs in
/// </summary>
public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager, 
        IOptions<IdentityOptions> optionsAccessor) 
        : base(userManager, optionsAccessor)
    {
        _userManager = userManager;
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
        var claimsIdentity = await base.GenerateClaimsAsync(user).ConfigureAwait(false);
        claimsIdentity.AddClaim(new Claim(UserClaimsConstants.TenantId, user.TenantId.ToString()));

        if(roles.Any(r => r == UserClaimsConstants.AdminRole))
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, roles.ToList().First(r => r == UserClaimsConstants.AdminRole)));

        return claimsIdentity;
    }
}
