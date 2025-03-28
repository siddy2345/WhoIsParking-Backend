using System.Security.Claims;

namespace API.WhoIsParking.UserClaims;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetTenantId(this ClaimsPrincipal user)
    {
        var tenantIdClaim = user.FindFirst("TenantId");
        return tenantIdClaim != null && Guid.TryParse(tenantIdClaim.Value, out Guid tenantId) 
            ? tenantId 
            : null;
    }
}
