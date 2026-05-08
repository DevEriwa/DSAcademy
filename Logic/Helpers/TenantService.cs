using Logic.IHelpers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Logic.Helpers
{
    /// <summary>
    /// Resolves the current tenant from the HTTP context claims.
    /// Register as Scoped in DI.
    /// </summary>
    public class TenantService : ITenantService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetCurrentTenantId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity!.IsAuthenticated)
                return null;

            // SuperAdmin has no tenant scope
            if (IsSuperAdmin())
                return null;

            var companyIdClaim = user.FindFirstValue("CompanyId");
            if (Guid.TryParse(companyIdClaim, out var companyId))
                return companyId;

            return null;
        }

        public bool IsSuperAdmin()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.IsInRole("SuperAdmin") ?? false;
        }
    }
}
