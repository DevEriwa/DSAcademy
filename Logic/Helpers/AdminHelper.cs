using Core.Config;
using Core.Db;
using Core.Enum;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helpers
{
	public class AdminHelper : IAdminHelper
	{
		private readonly IUserHelper _userHelper;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly AppDbContext _context;
		private readonly IEmailHelper _emailHelper;
		private readonly IGeneralConfiguration _generalConfiguration;
		public AdminHelper(
			IUserHelper userHelper, 
			UserManager<ApplicationUser> userManager,
			AppDbContext context, 
			IEmailHelper emailHelper, 
			IGeneralConfiguration generalConfiguration)
		{
			_userHelper = userHelper;
			_userManager = userManager;
			_context = context;
			_emailHelper = emailHelper;
			_generalConfiguration = generalConfiguration;
		}

		public TrainingCourse AddTrainignCostServices(TrainingCourseViewModel collectedData, string base64)
		{
			if (collectedData != null)
			{
				var newCost = new TrainingCourse
				{
					Title = collectedData.Title,
					Description = collectedData.Description,
					Logo = base64,
					Duration = collectedData.Duration,
					TestDuration = collectedData.TestDuration,
					Amount = Convert.ToDecimal(collectedData.Amount),
					IsActive = true,
					IsDeleted = false,
					DateCreated = DateTime.Now,
				};
				_context.TrainingCourse.Add(newCost);
				_context.SaveChanges();

				return newCost;
			}
			return null;
		}
		public TrainingCourse EditTrainignCostServices(TrainingCourseViewModel collectedData)
		{
			var costToEdit = _context.TrainingCourse.Where(c => c.Id == collectedData.Id).FirstOrDefault();
			if (costToEdit != null)
			{
				if (collectedData.Title != null)
				{
					costToEdit.Title = collectedData.Title;
				}
				if (collectedData.Description != null)
				{
					costToEdit.Description = collectedData.Description;
				}
				if (collectedData.Logo != null)
				{
					costToEdit.Logo = collectedData.Logo;
				}
				if (collectedData.Duration != null)
				{
					costToEdit.Duration = collectedData.Duration;
				}
				if (collectedData.TestDuration != null)
				{
					costToEdit.TestDuration = collectedData.TestDuration;
				}
				if (collectedData.Amount != null)
				{
					costToEdit.Amount = Convert.ToDecimal(collectedData.Amount);
				}
				costToEdit.IsActive = true;

				_context.TrainingCourse.Update(costToEdit);
				_context.SaveChanges();

				return costToEdit;
			}
			return null;
		}
		public TrainingCourse DisableTrainignCostServices(TrainingCourseViewModel collectedData)
		{
			var costToTurnOff = _context.TrainingCourse.Where(c => c.Id == collectedData.Id).FirstOrDefault();
			if (costToTurnOff != null)
			{
				costToTurnOff.IsActive = false;

				_context.TrainingCourse.Update(costToTurnOff);
				_context.SaveChanges();

				return costToTurnOff;
			}
			return null;
		}
		public TrainingCourse ActivateTrainignCostServices(TrainingCourseViewModel collectedData)
		{
			var costToTurnOn = _context.TrainingCourse.Where(c => c.Id == collectedData.Id).FirstOrDefault();
			if (costToTurnOn != null)
			{
				costToTurnOn.IsActive = true;

				_context.TrainingCourse.Update(costToTurnOn);
				_context.SaveChanges();

				return costToTurnOn;
			}
			return null;
		}
		public TrainingCourse DeleteTrainignCostServices(TrainingCourseViewModel collectedData)
		{
			var costToDelete = _context.TrainingCourse.Where(c => c.Id == collectedData.Id).FirstOrDefault();
			if (costToDelete != null)
			{
				costToDelete.IsDeleted = true;

				_context.TrainingCourse.Update(costToDelete);
				_context.SaveChanges();

				return costToDelete;
			}
			return null;
		}
		public TrainingVideo TrainignVideoServices(TrainingVideosViewModel collectedData)
		{
			if (collectedData.ActionType == GeneralAction.CREATE)
			{
				var newVideo = new TrainingVideo
				{
					CourseId = collectedData.CourseId,
					VideoLink = collectedData.VideoLink,
					Outline = collectedData.Outline,
					IsActive = true,
					DateCreated = DateTime.Now,
				};

				_context.TrainingVideos.Add(newVideo);
				_context.SaveChanges();

				return newVideo;
			}
			else if (collectedData.ActionType == GeneralAction.EDIT)
			{
				var videoEdited = _userHelper.GetVideosById(collectedData.Id);

				videoEdited.CourseId = collectedData.CourseId;
				videoEdited.VideoLink = collectedData.VideoLink;
				videoEdited.Outline = collectedData.Outline;

				_context.TrainingVideos.Update(videoEdited);
				_context.SaveChanges();

				return videoEdited;
			}
			else if (collectedData.ActionType == GeneralAction.DELETE)
			{
				var videoDeleted = _userHelper.GetVideosById(collectedData.Id);

				videoDeleted.IsActive = false;

				_context.TrainingVideos.Update(videoDeleted);
				_context.SaveChanges();

				return videoDeleted;
			}
			return null;
		}
		public TestQuestions TestQuestionsServices(TestQuestionsViewModel collectedData)
		{
			if (collectedData.ActionType == GeneralAction.CREATE)
			{
				var newQuestion = new TestQuestions
				{
					CourseId = collectedData.CourseId,
					Question = collectedData.Question,
					Answer = collectedData.Answer,
					IsActive = true,
					IsDeleted = false,
					DateCreated = DateTime.Now,
				};

				_context.TestQuestions.Add(newQuestion);
				_context.SaveChanges();

				return newQuestion;
			}
			else if (collectedData.ActionType == GeneralAction.EDIT)
			{
				var questionEdited = _userHelper.GetQuestionsById(collectedData.Id);

				questionEdited.CourseId = collectedData.CourseId;
				questionEdited.Question = collectedData.Question;
				questionEdited.Answer = collectedData.Answer;

				_context.TestQuestions.Update(questionEdited);
				_context.SaveChanges();

				return questionEdited;
			}
			else if (collectedData.ActionType == GeneralAction.ACTIVATE)
			{
				var questionActivate = _userHelper.GetQuestionsById(collectedData.Id);

				questionActivate.IsActive = true;

				_context.TestQuestions.Update(questionActivate);
				_context.SaveChanges();

				return questionActivate;
			}
			else if (collectedData.ActionType == GeneralAction.DEACTIVATE)
			{
				var questionDeactivate = _userHelper.GetQuestionsById(collectedData.Id);

				questionDeactivate.IsActive = false;

				_context.TestQuestions.Update(questionDeactivate);
				_context.SaveChanges();

				return questionDeactivate;
			}
			else if (collectedData.ActionType == GeneralAction.DELETE)
			{
				var questionDeleted = _userHelper.GetQuestionsById(collectedData.Id);

				questionDeleted.IsDeleted = true;

				_context.TestQuestions.Update(questionDeleted);
				_context.SaveChanges();

				return questionDeleted;
			}
			return null;
		}
		public List<string> GetOptListByQuestionIds(int id)
		{
			var optList = new List<string>();
			var optListDetails = _context.AnswerOptions.Where(a => a.QuestionId == id).OrderByDescending(o => o.Id).ToList();
			if (optListDetails.Any())
			{
				optList = optListDetails.Select(a => a.Option).ToList();
				return optList;
			}
			return null;
		}
		public bool PostServices4Options(AnswerOptionViewModel collectedData)
		{
			var oldOptions = _context.AnswerOptions.Where(a => a.QuestionId == collectedData.QuestionId).ToList();
			if (oldOptions.Any())
			{
				_context.AnswerOptions.RemoveRange(oldOptions);
			}
			if (collectedData != null)
			{
				var newOptionOne = new AnswerOptions
				{
					QuestionId = collectedData.QuestionId,
					Option = collectedData.OptionOne,
				};

				_context.AnswerOptions.AddRange(newOptionOne);

				var newOptionTwo = new AnswerOptions
				{
					QuestionId = collectedData.QuestionId,
					Option = collectedData.OptionTwo,
				};

				_context.AnswerOptions.AddRange(newOptionTwo);

				var newOptionThree = new AnswerOptions
				{
					QuestionId = collectedData.QuestionId,
					Option = collectedData.OptionThree,
				};

				_context.AnswerOptions.AddRange(newOptionThree);

				var newOptionFour = new AnswerOptions
				{
					QuestionId = collectedData.QuestionId,
					Option = collectedData.OptionFour,
				};

				_context.AnswerOptions.AddRange(newOptionFour);

				_context.SaveChanges();


				return true;
			}
			return false;
		}
	}
}
