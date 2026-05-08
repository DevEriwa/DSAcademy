using Core.Models;
using Core.Enum;

namespace Core.ViewModels
{
    /// <summary>Report data for a single school tenant — used by School Admin.</summary>
    public class SchoolReportViewModel
    {
        public string? SchoolName { get; set; }
        public Guid? CompanyId { get; set; }

        // Students
        public int TotalStudents { get; set; }
        public int ActiveStudents { get; set; }
        public int InactiveStudents { get; set; }

        // Payments
        public int TotalPayments { get; set; }
        public int ApprovedPayments { get; set; }
        public int PendingPayments { get; set; }
        public int DeclinedPayments { get; set; }

        // Courses
        public int TotalCourses { get; set; }

        // Recent payments for table
        public List<Payments> RecentPayments { get; set; } = new();

        // Student enrollment by month (last 6 months)
        public List<MonthCount> EnrollmentByMonth { get; set; } = new();
    }

    /// <summary>Platform-wide report for SuperAdmin.</summary>
    public class PlatformReportViewModel
    {
        public int TotalSchools { get; set; }
        public int ActiveSchools { get; set; }
        public int TotalStudents { get; set; }
        public int TotalPayments { get; set; }
        public int ApprovedPayments { get; set; }
        public int PendingPayments { get; set; }
        public int TotalCourses { get; set; }

        public List<SchoolPaymentSummary> SchoolBreakdown { get; set; } = new();
    }

    public class MonthCount
    {
        public string Month { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class SchoolPaymentSummary
    {
        public string? SchoolName { get; set; }
        public int TotalStudents { get; set; }
        public int ApprovedPayments { get; set; }
        public int PendingPayments { get; set; }
        public int TotalCourses { get; set; }
        public bool IsActive { get; set; }
    }
}
