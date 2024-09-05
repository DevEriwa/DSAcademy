using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
    public interface IUserHelper
    {
		Task<UserVerification> CreateUserToken(string userEmail);
		Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber);
        Task<ApplicationUser> FindByUserNameAsync(string userName);
		List<ApplicationUserViewModel> GetAllOnboardApplicantsFromDB();
		List<TrainingCourse> GetAllTrainingCourseFromDB();
		List<string> GetOptListByQuestionIds(int? id);
		Payments GetPaymentById(int? Id);
		List<Payments> GetPaymentList();
		TestQuestions GetQuestionsById(int? Id);
		List<TrainingVideo> GetStudentPaidTrainingVideos(string userID);
		List<TestQuestions> GetTestQuestions();
		List<TestQuestionsViewModel> GetTestQuestionsForPage1(int? Id);
		List<TestQuestionsViewModel> GetTestQuestionsForPage2(int? Id);
		TrainingCourse GetTrainingCourseById(int? Id);
		List<TrainingVideo> GetTrainingVideos();
		Task<UserVerification> GetUserToken(Guid token);
		TrainingVideo GetVideosById(Guid Id);
		Task<List<ApplicationUser>> GetStudentListAsync();
		List<ApplicationUserViewModel> GetTeacher(Guid? companyId);
		ApplicationUser FindById(string Id);
		Task<Company> FindCompanyByUserId(string id);
		ApplicationUser GetTecherById(string? techerId);

	}
}
