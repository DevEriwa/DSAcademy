using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
	public interface IAdminHelper
	{
		TrainingCourse ActivateTrainignCostServices(TrainingCourseViewModel collectedData);
		TrainingCourse AddTrainignCostServices(TrainingCourseViewModel collectedData);
		TrainingCourse DeleteTrainignCostServices(TrainingCourseViewModel collectedData);
		TrainingCourse DisableTrainignCostServices(TrainingCourseViewModel collectedData);
		TrainingCourse EditTrainignCostServices(TrainingCourseViewModel collectedData);
		List<string> GetOptListByQuestionIds(int id);
		bool PostServices4Options(AnswerOptionViewModel collectedData);
		TestQuestions TestQuestionsServices(TestQuestionsViewModel collectedData);
		TrainingVideo TrainignVideoServices(TrainingVideosViewModel collectedData);
	}
}
