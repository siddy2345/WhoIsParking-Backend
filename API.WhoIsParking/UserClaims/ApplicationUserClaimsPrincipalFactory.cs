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
    public ApplicationUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager, 
        IOptions<IdentityOptions> optionsAccessor) 
        : base(userManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var claimsIdentity = await base.GenerateClaimsAsync(user).ConfigureAwait(false);
        claimsIdentity.AddClaim(new Claim("TenantId", user.TenantId.ToString()));
        return claimsIdentity;
    }
}
