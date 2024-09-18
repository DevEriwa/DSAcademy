using Core.Config;
using Core.Db;
using Core.Enum;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
			var newCost = new TrainingCourse();
			if (collectedData != null)
			{
				newCost.Title = collectedData.Title;
				newCost.Description = collectedData.Description;
				newCost.Logo = base64;
				newCost.Duration = collectedData.Duration;
				newCost.TestDuration = collectedData.TestDuration;
				newCost.Amount = Convert.ToDecimal(collectedData.Amount);
				newCost.UserId = collectedData?.User?.Id;
				newCost.CompanyId = collectedData?.User?.CompanyId;
				newCost.ProgramStatus = collectedData?.ProgramStatus;
				_context.TrainingCourse.Add(newCost);
				_context.SaveChanges();
			}
			return newCost;
		}
		public TrainingCourse EditTrainignCostServices(TrainingCourseViewModel collectedData)
		{
			var costToEdit = _context.TrainingCourse
				.Where(c => c.Id == collectedData.Id && c.CompanyId == collectedData.User.CompanyId)
				.Include(c => c.Company).ThenInclude(u => u.CreatedBy)
				.FirstOrDefault();

			if (costToEdit == null)
			{
				return null;
			}
			costToEdit.Title = collectedData.Title ?? costToEdit.Title;
			costToEdit.Description = collectedData.Description ?? costToEdit.Description;
			costToEdit.Logo = collectedData.Logo ?? costToEdit.Logo;
			costToEdit.Duration = collectedData.Duration ?? costToEdit.Duration;
			costToEdit.TestDuration = collectedData.TestDuration ?? costToEdit.TestDuration;
			costToEdit.ProgramStatus = collectedData.ProgramStatus ?? costToEdit.ProgramStatus;
			if (decimal.TryParse(collectedData.Amount, out decimal amount) && amount != 0)
			{
				costToEdit.Amount = amount;
			}
			costToEdit.IsActive = true;
			_context.TrainingCourse.Update(costToEdit);
			_context.SaveChanges();
			return costToEdit;
		}
		public TrainingCourse DisableTrainignCostServices(TrainingCourseViewModel collectedData)
		{
			var costToTurnOff = _context.TrainingCourse.Where(c => c.Id == collectedData.Id)
			.Include(c => c.Company).ThenInclude(u => u.CreatedBy)
			.FirstOrDefault();
			if (costToTurnOff != null)
			{
				costToTurnOff.IsActive = false;
				costToTurnOff.IsDeleted = true;
				_context.TrainingCourse.Update(costToTurnOff);
				_context.SaveChanges();
				return costToTurnOff;
			}
			return null;
		}
		public TrainingCourse ActivateTrainignCostServices(TrainingCourseViewModel collectedData)
		{
			var costToTurnOn = _context.TrainingCourse.Where(c => c.Id == collectedData.Id)
			.Include(c => c.Company).ThenInclude(u => u.CreatedBy)
			.FirstOrDefault();
			if (costToTurnOn != null)
			{
				costToTurnOn.IsActive = true;
				costToTurnOn.IsDeleted = false;
				_context.TrainingCourse.Update(costToTurnOn);
				_context.SaveChanges();

				return costToTurnOn;
			}
			return null;
		}
		public TrainingCourse DeleteTrainignCostServices(TrainingCourseViewModel collectedData)
		{
			var costToDelete = _context.TrainingCourse.Where(c => c.Id == collectedData.Id)
				.Include(c => c.Company).ThenInclude(u => u.CreatedBy)
				.FirstOrDefault();
			if (costToDelete != null)
			{
				costToDelete.IsDeleted = true;
				costToDelete.IsActive = false;

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
		public List<TrainingCourse> GetAllTrainingCourses(Guid? companyId)
		{
			return _context.TrainingCourse
				.Where(t => !t.IsDeleted && t.CompanyId == companyId)
				.Include(c => c.Company)
				.ThenInclude(u => u.CreatedBy)
				.ToList();
		}
		public List<TrainingCourse> GetAllTrainingCourseForFrontend(Guid? companyId)
		{
			return _context.TrainingCourse
				.Where(t => !t.IsDeleted && t.CompanyId == companyId && t.ProgramStatus == ProgramEnum.Frontend)
				.Include(c => c.Company)
				.ThenInclude(u => u.CreatedBy)
				.ToList();
		}

		public List<TrainingCourse> GetAllTrainingCourseForBackend(Guid? companyId)
        {
			return _context.TrainingCourse.Where(t => !t.IsDeleted && t.CompanyId == companyId && t.ProgramStatus == ProgramEnum.Backend)
			.Include(c => c.Company)
			.ThenInclude(u => u.CreatedBy)
			.ToList();
		}
		public List<TrainingCourse> GetAllTrainingCourseForFullStack(Guid? companyId)
		{
			return _context.TrainingCourse.Where(t => !t.IsDeleted && t.CompanyId == companyId &&
			(t.ProgramStatus == ProgramEnum.Frontend || t.ProgramStatus == ProgramEnum.Backend))
			.Include(c => c.Company)
			.ThenInclude(u => u.CreatedBy)
			.ToList();
		}
        public TrainingCourse GetCoursePayment(int courseId, ProgramEnum status)
        {
            var trainingCourse = _context.TrainingCourse
                .Where(t => t.Id == courseId
                    && !t.IsDeleted
                    && t.CompanyId != Guid.Empty
                    && t.IsActive
                    && t.ProgramStatus == status) 
                .Include(c => c.Company)
                    .ThenInclude(u => u.CreatedBy)
                .FirstOrDefault(); 
            return trainingCourse;
        }

        public AdminDashboardViewModel? GetAdminDashboardData()
        {
            var myStudents = new List<ApplicationUserViewModel>();
            var myTeachers = new List<ApplicationUserViewModel>();
            var trainingCourse = new List<TrainingCourseViewModel>();
            var loggedInUser = Session.GetCurrentUser();
            if(loggedInUser.Id != null)
            {
                var date = DateTime.Now;
				var userName = _userHelper.FindByEmailAsync(loggedInUser.Email).Result;
				var students = _userHelper.GetStudentListAsync().Result;
                var studentCount = students.Count();
                var teacherCount = _userHelper.GetTeacher(userName.CompanyId).Count();
                var courseCount = _userHelper.GetAllTrainingCourseFromDB().Count();
               
                var allstudents = _context.ApplicationUsers
                .Where(x => !x.IsDeactivated && x.CompanyId == loggedInUser.CompanyId && (x.Role == "Student" || x.Role == "Applicant"))
                .Include(x => x.Company).ToList();
				
				var allTeacher = _context.ApplicationUsers
                .Where(x => !x.IsDeactivated && x.CompanyId == loggedInUser.CompanyId && (x.Role == "Techer"))
                .Include(x => x.Company).ToList();
				
				var allCourse = _context.TrainingCourse
                .Where(x => !x.IsDeleted && x.CompanyId == loggedInUser.CompanyId)
                .Include(x => x.Company).ToList();

                if (allstudents.Any())
                {
                    myStudents = allstudents
                        .Where(x => x.IsProgram == true)
                        .OrderByDescending(v => v.DateRegistered)
                        .Select(s => new ApplicationUserViewModel
                        {
                            Id = s.Id,
                            Address = s.Address,
                            Status = s.Status,
                            CompanyId = s.CompanyId,
                            Email = s.Email,
                            UserName = s.UserName,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            FullName = s.FullName,
                            Role = "Student",
                            PhoneNumber = s.PhoneNumber,
                            IsProgram = s.IsProgram,
                            IsAdmin = false,

                        }).ToList();


                    myTeachers = allTeacher
                         .OrderByDescending(v => v.DateRegistered)
                         .Select(s => new ApplicationUserViewModel
                         {
                             Id = s.Id,
                             Address = s.Address,
                             Status = s.Status,
                             CompanyId = s.CompanyId,
                             Email = s.Email,
                             UserName = s.UserName,
                             FirstName = s.FirstName,
                             LastName = s.LastName,
                             FullName = s.FullName,
                             Role = "Techer",
                             PhoneNumber = s.PhoneNumber,
                             IsProgram = s.IsProgram,
                             IsAdmin = false,
                         }).ToList();


                    trainingCourse = allCourse
                    .OrderByDescending(v => v.DateCreated)
                    .Select(s => new TrainingCourseViewModel
                    {
                        Id = s.Id,
						Amount = s.Amount.ToString(),
						DateCreated= s.DateCreated,
						Description = s.Description,
						Duration = s.Duration,
						ProgramStatus = s.ProgramStatus,
						Logo = s.Logo,
                        TestDuration = s.TestDuration,
						Title = s.Title,
						User = s.User,
                    }).ToList();
                }
                if (loggedInUser != null)
                {
                    var adminDashBoard = new AdminDashboardViewModel()
                    {
						StudentCount = studentCount,
						TeacherCount = teacherCount,
						CourseCount = courseCount,
						CompanyId = userName.CompanyId
                    };
                    return adminDashBoard;
                }
            }
            return null;
        }
		public ApplicationUser EditTecher(ApplicationUserViewModel techer)
		{
			var appUser = _context.ApplicationUsers
				.FirstOrDefault(c => c.Id == techer.Id);
			if (techer == null)
			{
				return null;
			}
			appUser.FirstName = techer.FirstName ?? appUser.FirstName;
			appUser.LastName = techer.LastName ?? appUser.LastName;
			appUser.Email = techer.Email ?? appUser.Email;
			appUser.Address = techer.Address ?? appUser.Address;
			appUser.PhoneNumber = techer.PhoneNumber ?? appUser.PhoneNumber;
			appUser.IsActivated = true;

			_context.ApplicationUsers.Update(appUser);
			_context.SaveChanges();
			return appUser;
		}
		public ApplicationUser DeleteTecher(string techerId)
		{
			var appUser = _context.ApplicationUsers
				.FirstOrDefault(c => c.Id == techerId);
			if (appUser == null)
			{
				return null;
			}			
			appUser.IsActivated = false;
			appUser.IsDeactivated = true;
			_context.ApplicationUsers.Update(appUser);
			_context.SaveChanges();
			return appUser;
		}
		public async Task<string> HandleFileUploadAsync(IFormFile uploadedFile)
		{
			if (uploadedFile != null && uploadedFile.Length > 0)
			{
				var fileName = Path.GetFileName(uploadedFile.FileName);
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await uploadedFile.CopyToAsync(stream);
				}

				return filePath;
			}

			return null;
		}
		public async Task<string> CreateTrainingVideoAsync(TrainingVideosViewModel videoDetails/*, IFormFile uploadedFile*/)
		{
			//var filePath = await HandleFileUploadAsync(uploadedFile);

			var newVideo = new TrainingVideo
			{
				CourseId = videoDetails.CourseId,
				VideoLink = videoDetails.VideoLink,
				VideoPath = "",
				Outline = videoDetails.Outline,
				IsActive = true,
				DateCreated = DateTime.Now,
			};

			_context.TrainingVideos.Add(newVideo);
			await _context.SaveChangesAsync();

			return "Created Successfully";
		}

		public async Task<string> EditTrainingVideoAsync(TrainingVideosViewModel videoDetails)
		{
			var videoEdited = await _context.TrainingVideos.FindAsync(videoDetails.Id);

			if (videoEdited != null)
			{
				videoEdited.CourseId = videoDetails.CourseId;
				videoEdited.VideoLink = videoDetails.VideoLink;
				videoEdited.Outline = videoDetails.Outline;

				_context.TrainingVideos.Update(videoEdited);
				await _context.SaveChangesAsync();

				return "Updated Successfully";
			}

			return "Video Not Found";
		}

		public async Task<string> DeleteTrainingVideoAsync(Guid id)
		{
			var videoDeleted = await _context.TrainingVideos.FindAsync(id);

			if (videoDeleted != null)
			{
				videoDeleted.IsActive = false;

				_context.TrainingVideos.Update(videoDeleted);
				await _context.SaveChangesAsync();

				return "Deleted Successfully";
			}

			return "Video Not Found";
		}

		//PAYMENT ACCEPT POST SERVICE
		public Payments ApproveSelectedPayment(Payments paymentData)
		{
			if (paymentData != null)
			{
				var paymentDetails = _userHelper.GetPaymentById(paymentData.Id);

				paymentDetails.Status = PaymentStatus.Approved;

				var approved = _context.Payments.Update(paymentDetails);
				_context.SaveChanges();

				if (approved != null)
				{
					_emailHelper.SendPaymentAprovalMsg(paymentDetails.User, paymentDetails.Courses.Title);
				}

				return paymentDetails;
			}
			else
			{
				return null;
			}
		}
		//PAYMENT DECLINE POST SERVICE
		public Payments DeclineSelectedPaymment(Payments paymentData)
		{
			if (paymentData != null)
			{
				var paymentDetails = _userHelper.GetPaymentById(paymentData.Id);

				paymentDetails.Status = PaymentStatus.Declined;

				var declined = _context.Payments.Update(paymentDetails);
				_context.SaveChanges();

				if (declined != null)
				{
					_emailHelper.SendPaymentDeclineMsg(paymentDetails.User, paymentDetails.Courses.Title);
				}

				return paymentDetails;
			}
			else
			{
				return null;
			}
		}
	}
}
