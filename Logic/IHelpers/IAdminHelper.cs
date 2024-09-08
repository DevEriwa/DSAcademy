using Core.Enum;
using Core.Models;
using Core.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Logic.IHelpers
{
	public interface IAdminHelper
	{
		TrainingCourse ActivateTrainignCostServices(TrainingCourseViewModel collectedData);
		TrainingCourse AddTrainignCostServices(TrainingCourseViewModel collectedData, string base64);
		TrainingCourse DeleteTrainignCostServices(TrainingCourseViewModel collectedData);
		TrainingCourse DisableTrainignCostServices(TrainingCourseViewModel collectedData);
		TrainingCourse EditTrainignCostServices(TrainingCourseViewModel collectedData);
		List<string> GetOptListByQuestionIds(int id);
		bool PostServices4Options(AnswerOptionViewModel collectedData);
		TestQuestions TestQuestionsServices(TestQuestionsViewModel collectedData);
		TrainingVideo TrainignVideoServices(TrainingVideosViewModel collectedData);
		List<TrainingCourse> GetAllTrainingCourseForFrontend(Guid? companyId);
		List<TrainingCourse> GetAllTrainingCourseForBackend(Guid? companyId);
		List<TrainingCourse> GetAllTrainingCourseForFullStack(Guid? companyId);
		//TrainingCourse CoursePayment(int courseId);
		AdminDashboardViewModel? GetAdminDashboardData();
        List<TrainingCourse> GetAllTrainingCourses(Guid? companyId);
        TrainingCourse GetCoursePayment(int courseId, ProgramEnum status);
		Task<string> CreateTrainingVideoAsync(TrainingVideosViewModel videoDetails);
		Task<string> EditTrainingVideoAsync(TrainingVideosViewModel videoDetails);
		Task<string> DeleteTrainingVideoAsync(Guid id);
	}
}
