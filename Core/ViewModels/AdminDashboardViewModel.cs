namespace Core.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int StudentCount { get; set; }
        public int TeacherCount { get; set; }
        public int CourseCount { get; set; }
        public List<ApplicationUserViewModel>? Student { get; set; }
        public List<ApplicationUserViewModel>? Teacher { get; set; }
        public List<TrainingCourseViewModel>? Courses { get; set; }
        public Guid? CompanyId { get; set; }
        public string? FullName { get; set; }
    }
}
