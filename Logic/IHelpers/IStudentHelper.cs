using Core.Models;
using Core.ViewModels;

namespace Logic.IHelpers
{
    public interface IStudentHelper
    {
        StudentDashBoardViewModel DashboardBuildingServices();
		List<TrainingCourse> GetAllTrainingCourseDB();
		Task<Payments> UploadMaualPaymentProve(PaymentViewModel prove, string userId);
	}
}
