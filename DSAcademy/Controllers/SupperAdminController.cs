using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DSAcademy.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : BaseController
    {
        private readonly ISuperAdminHelper _superAdminHelper;

        public SuperAdminController(ISuperAdminHelper superAdminHelper)
        {
            _superAdminHelper = superAdminHelper;
        }

        // ─── Dashboard ────────────────────────────────────────────────────

        [HttpGet]
        public IActionResult Index()
        {
            var stats = _superAdminHelper.GetPlatformStats();
            return View(stats);
        }

        // ─── Schools ──────────────────────────────────────────────────────

        [HttpGet]
        public IActionResult Schools()
        {
            var schools = _superAdminHelper.GetAllSchools();
            return View(schools);
        }

        [HttpGet]
        public IActionResult CreateSchool()
        {
            return View(new CompanyViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSchool(CompanyViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var created = _superAdminHelper.CreateSchool(model);
            if (created)
            {
                TempData["Success"] = $"School '{model.Name}' created successfully.";
                return RedirectToAction(nameof(Schools));
            }

            ModelState.AddModelError("", "Unable to create school. Please try again.");
            return View(model);
        }

        [HttpPost]
        public JsonResult ToggleSchoolStatus(Guid companyId, bool activate)
        {
            if (companyId == Guid.Empty)
                return Json(new { isError = true, msg = "Invalid school ID." });

            var result = _superAdminHelper.ToggleSchoolStatus(companyId, activate);
            if (result)
                return Json(new { isError = false, msg = activate ? "School activated successfully." : "School deactivated successfully." });

            return Json(new { isError = true, msg = "Unable to update school status." });
        }

        [HttpPost]
        public JsonResult DeleteSchool(Guid companyId)
        {
            if (companyId == Guid.Empty)
                return Json(new { isError = true, msg = "Invalid school ID." });

            var result = _superAdminHelper.DeleteSchool(companyId);
            if (result)
                return Json(new { isError = false, msg = "School removed successfully." });

            return Json(new { isError = true, msg = "Unable to remove school." });
        }

        // ─── Theme Settings ───────────────────────────────────────────────

        [HttpGet]
        public IActionResult SchoolTheme(Guid companyId)
        {
            var theme = _superAdminHelper.GetSchoolTheme(companyId);
            if (theme == null)
            {
                TempData["Error"] = "School settings not found.";
                return RedirectToAction(nameof(Schools));
            }
            return View(theme);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SchoolTheme(CompanySettingViewModel model)
        {
            var saved = _superAdminHelper.SaveSchoolTheme(model);
            if (saved)
                TempData["Success"] = "Theme settings saved successfully.";
            else
                TempData["Error"] = "Unable to save theme settings.";

            return RedirectToAction(nameof(SchoolTheme), new { companyId = model.CompanyId });
        }

        // ─── Platform Reports ──────────────────────────────────────────────────

        [HttpGet]
        public IActionResult Report()
        {
            var report = _superAdminHelper.GetPlatformReport();
            return View(report);
        }

        [HttpGet]
        public IActionResult ExportReport()
        {
            var report = _superAdminHelper.GetPlatformReport();

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("School Name,Active,Total Students,Approved Payments,Pending Payments,Total Courses");
            foreach (var s in report.SchoolBreakdown)
            {
                sb.AppendLine($"\"{s.SchoolName}\",{s.IsActive},{s.TotalStudents},{s.ApprovedPayments},{s.PendingPayments},{s.TotalCourses}");
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", $"PlatformReport_{DateTime.Now:yyyyMMdd}.csv");
        }
    }
}
