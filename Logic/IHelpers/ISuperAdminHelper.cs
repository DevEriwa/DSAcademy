using Core.ViewModels;

namespace Logic.IHelpers
{
    public interface ISuperAdminHelper
    {
        // ─── Schools (Tenants) ────────────────────────────────────────────
        List<CompanyViewModel> GetAllSchools();
        CompanyViewModel? GetSchoolById(Guid companyId);
        bool CreateSchool(CompanyViewModel model);
        bool ToggleSchoolStatus(Guid companyId, bool activate);
        bool DeleteSchool(Guid companyId);

        // ─── Theme / Branding ─────────────────────────────────────────────
        CompanySettingViewModel? GetSchoolTheme(Guid companyId);
        bool SaveSchoolTheme(CompanySettingViewModel model);

        // ─── Platform Stats ───────────────────────────────────────────────
        SuperAdminDashboardViewModel GetPlatformStats();

        // ─── Platform Reports ─────────────────────────────────────────────
        PlatformReportViewModel GetPlatformReport();
    }
}
