namespace Core.ViewModels
{
    public class SuperAdminDashboardViewModel
    {
        public int TotalSchools { get; set; }
        public int ActiveSchools { get; set; }
        public int InactiveSchools { get; set; }
        public int TotalStudents { get; set; }
        public int TotalPayments { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<CompanyViewModel> RecentSchools { get; set; } = new();
    }
}
