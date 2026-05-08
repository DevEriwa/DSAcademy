using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Logic.Helpers
{
    public class SuperAdminHelper : ISuperAdminHelper
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuperAdminHelper(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ─── Schools ──────────────────────────────────────────────────────

        public List<CompanyViewModel> GetAllSchools()
        {
            return _context.Companies
                .Where(c => !c.Deleted)
                .OrderByDescending(c => c.DateCreated)
                .Select(c => new CompanyViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone,
                    CompanyLogo = c.CompanyLogo,
                    CompanyMotto = c.CompanyMotto,
                    Active = c.Active,
                    DateCreated = c.DateCreated
                }).ToList();
        }

        public CompanyViewModel? GetSchoolById(Guid companyId)
        {
            var c = _context.Companies.FirstOrDefault(x => x.Id == companyId && !x.Deleted);
            if (c == null) return null;

            return new CompanyViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Mobile = c.Mobile,
                CompanyLogo = c.CompanyLogo,
                CompanyMotto = c.CompanyMotto,
                CompanyAddress = c.Address,
                Active = c.Active,
                DateCreated = c.DateCreated
            };
        }

        public bool CreateSchool(CompanyViewModel model)
        {
            try
            {
                var company = new Company
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    Mobile = model.Mobile,
                    Address = model.CompanyAddress,
                    CompanyMotto = model.CompanyMotto,
                    Active = true,
                    Deleted = false,
                    DateCreated = DateTime.Now
                };
                _context.Companies.Add(company);

                // Create default settings for this school
                var settings = new CompanySetting
                {
                    CompanyId = company.Id,
                    EnableBasePackage = true,
                    EnableSMS = false,
                    PrimaryColor = "#0A192F",
                    SecondaryColor = "#FFB300",
                    SidebarColor = "#112B50",
                    FontFamily = "Outfit",
                    DarkMode = false
                };
                _context.CompanySettings.Add(settings);
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool ToggleSchoolStatus(Guid companyId, bool activate)
        {
            var company = _context.Companies.FirstOrDefault(c => c.Id == companyId && !c.Deleted);
            if (company == null) return false;
            company.Active = activate;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteSchool(Guid companyId)
        {
            var company = _context.Companies.FirstOrDefault(c => c.Id == companyId);
            if (company == null) return false;
            company.Deleted = true;
            company.Active = false;
            _context.SaveChanges();
            return true;
        }

        // ─── Theme ────────────────────────────────────────────────────────

        public CompanySettingViewModel? GetSchoolTheme(Guid companyId)
        {
            var s = _context.CompanySettings
                .Include(x => x.Company)
                .FirstOrDefault(x => x.CompanyId == companyId);

            if (s == null) return null;

            return new CompanySettingViewModel
            {
                CompanyId = s.CompanyId,
                CompanyName = s.Company?.Name ?? string.Empty,
                EnableSMS = s.EnableSMS,
                EnableBasePackage = s.EnableBasePackage,
                DashboardUrl = s.DashboardUrl,
                EnableCustomInvoice = s.EnableCustomInvoice,
                PrimaryColor = s.PrimaryColor ?? "#0A192F",
                SecondaryColor = s.SecondaryColor ?? "#FFB300",
                SidebarColor = s.SidebarColor ?? "#112B50",
                FontFamily = s.FontFamily ?? "Outfit",
                DarkMode = s.DarkMode
            };
        }

        public bool SaveSchoolTheme(CompanySettingViewModel model)
        {
            try
            {
                var s = _context.CompanySettings.FirstOrDefault(x => x.CompanyId == model.CompanyId);
                if (s == null)
                {
                    s = new CompanySetting { CompanyId = model.CompanyId };
                    _context.CompanySettings.Add(s);
                }

                s.PrimaryColor = model.PrimaryColor;
                s.SecondaryColor = model.SecondaryColor;
                s.SidebarColor = model.SidebarColor;
                s.FontFamily = model.FontFamily;
                s.DarkMode = model.DarkMode;
                s.EnableSMS = model.EnableSMS;
                s.EnableBasePackage = model.EnableBasePackage;
                s.DashboardUrl = model.DashboardUrl;
                s.EnableCustomInvoice = model.EnableCustomInvoice;

                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        // ─── Platform Stats ───────────────────────────────────────────────

        public SuperAdminDashboardViewModel GetPlatformStats()
        {
            var allSchools = _context.Companies.Where(c => !c.Deleted).ToList();
            var totalStudents = _userManager.Users.Count(u => !u.IsAdmin && !u.IsDeactivated);
            var totalPayments = _context.Payments.Count();
            var totalRevenue = _context.Payments
                .Where(p => p.Status == Core.Enum.PaymentStatus.Approved)
                .Sum(p => (decimal?)p.Amount) ?? 0m;

            return new SuperAdminDashboardViewModel
            {
                TotalSchools = allSchools.Count,
                ActiveSchools = allSchools.Count(c => c.Active),
                InactiveSchools = allSchools.Count(c => !c.Active),
                TotalStudents = totalStudents,
                TotalPayments = totalPayments,
                TotalRevenue = totalRevenue,
                RecentSchools = allSchools
                    .OrderByDescending(c => c.DateCreated)
                    .Take(5)
                    .Select(c => new CompanyViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Email = c.Email,
                        Active = c.Active,
                        DateCreated = c.DateCreated
                    }).ToList()
            };
        }

        // ─── Platform Report ──────────────────────────────────────────────────
        public PlatformReportViewModel GetPlatformReport()
        {
            var schools = _context.Companies.Where(c => !c.Deleted).ToList();
            var allPayments = _context.Payments.ToList();
            var allStudents = _userManager.Users.Where(u => !u.IsAdmin).ToList();
            var allCourses = _context.TrainingCourse.Where(c => !c.IsDeleted && c.IsActive).ToList();

            var breakdown = schools.Select(s => new SchoolPaymentSummary
            {
                SchoolName = s.Name,
                IsActive = s.Active,
                TotalStudents = allStudents.Count(u => u.CompanyId == s.Id),
                ApprovedPayments = allPayments.Count(p => p.CompanyId == s.Id && p.Status == Core.Enum.PaymentStatus.Approved),
                PendingPayments = allPayments.Count(p => p.CompanyId == s.Id && p.Status == Core.Enum.PaymentStatus.Pending),
                TotalCourses = allCourses.Count(c => c.CompanyId == s.Id)
            }).OrderByDescending(x => x.TotalStudents).ToList();

            return new PlatformReportViewModel
            {
                TotalSchools = schools.Count,
                ActiveSchools = schools.Count(s => s.Active),
                TotalStudents = allStudents.Count,
                TotalPayments = allPayments.Count,
                ApprovedPayments = allPayments.Count(p => p.Status == Core.Enum.PaymentStatus.Approved),
                PendingPayments = allPayments.Count(p => p.Status == Core.Enum.PaymentStatus.Pending),
                TotalCourses = allCourses.Count,
                SchoolBreakdown = breakdown
            };
        }
    }
}
