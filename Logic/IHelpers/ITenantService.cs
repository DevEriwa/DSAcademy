namespace Logic.IHelpers
{
    /// <summary>
    /// Resolves the current tenant (school) context from the logged-in user's claims.
    /// Inject this wherever you need to scope data to the current school.
    /// </summary>
    public interface ITenantService
    {
        /// <summary>Gets the CompanyId of the currently logged-in user's school. Returns null for SuperAdmin.</summary>
        Guid? GetCurrentTenantId();

        /// <summary>Returns true if the current user is a SuperAdmin (no tenant scope).</summary>
        bool IsSuperAdmin();
    }
}
