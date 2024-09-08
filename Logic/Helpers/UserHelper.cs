using Core.Db;
using Core.Enum;
using Core.Models;
using Core.ViewModels;
using Hangfire;
using Logic.IHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RedisCache;
using System.Net;

namespace Logic.Helpers
{
	public class UserHelper: BaseHelper, IUserHelper
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
		private readonly INotificationHelper _notificationHelper;

		public UserHelper(
			UserManager<ApplicationUser>
			userManager,
			ICacheService cacheService,
			AppDbContext context
,
			INotificationHelper notificationHelper) : base(context, cacheService)
		{
			_userManager = userManager;
			_context = context;
			_notificationHelper = notificationHelper;
		}

		public string GetFullNameByUserNameAsync(string userName)
        {
            var user = _userManager.Users.Where(u => u.UserName == userName)?.Include(c => c.Company).FirstOrDefaultAsync().Result;
            var fullName = user.LastName + " " + user.FirstName;
            return fullName;
        }
        public async Task<ApplicationUser> FindByUserNameAsync(string userName)
        {
            return _userManager.Users.Where(u => u.UserName == userName)?.Include(c => c.Company).FirstOrDefaultAsync().Result;
        }
        public async Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber)
        {
            return await _userManager.Users.Where(s => s.PhoneNumber == phoneNumber)?.Include(c => c.Company)?.FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.Users.Where(s => s.Email == email)?.Include(c => c.Company)?.FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> FindByIdAsync(string Id)
        {
            return await _userManager.Users.Where(s => s.Id == Id)?.Include(c => c.Company)?.FirstOrDefaultAsync();
        }
        public List<ApplicationUserViewModel> GetAllOnboardApplicantsFromDB()
        {
            var applicantsList = new List<ApplicationUserViewModel>();
            var allApplicants = _context.ApplicationUsers.Where(b => b.IsDeactivated == false && b.IsAdmin == false).OrderByDescending(d => d.DateRegistered).ToList();
            if (allApplicants.Any())
            {
                applicantsList = allApplicants.Select(s => new ApplicationUserViewModel()
                {
                    Id = s.Id,
                    UserName = s.UserName,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    FullName = s.FirstName,
                    PhoneNumber = s.PhoneNumber,
                    Email = s.Email,
                    ApplicantResideInEnugu = s.ApplicantResideInEnugu,
                    HasLaptop = s.HasLaptop,
                    HasAnyProgrammingExp = s.HasAnyProgrammingExp,
                    ProgrammingLanguagesExps = s.ProgrammingLanguagesExps,
                    ReasonForProgramming = s.ReasonForProgramming,
                    Status = s.Status,
                }).ToList();
                return applicantsList;
            }
            return applicantsList;
        }
        public async Task<UserVerification> CreateUserToken(string userEmail)
        {
            try
            {
                var user = await FindByEmailAsync(userEmail);
                if (user != null)
                {
                    UserVerification userVerification = new UserVerification()
                    {
                        UserId = user.Id,
                    };
                    await _context.AddAsync(userVerification);
                    await _context.SaveChangesAsync();
                    return userVerification;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UserVerification> GetUserToken(Guid token)
        {
            return await _context.UserVerifications.Where(t => !t.Used && t.Token == token)?.Include(s => s.User).FirstOrDefaultAsync();
        }
        public async Task<bool> MarkTokenAsUsed(UserVerification userVerification)
        {
            try
            {
                var VerifiedUser = _context.UserVerifications.Where(s => s.UserId == userVerification.User.Id && s.Used != true).FirstOrDefault();
                if (VerifiedUser != null)
                {
                    userVerification.Used = true;
                    userVerification.DateUsed = DateTime.Now;
                    _context.Update(userVerification);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TrainingCourse> GetAllTrainingCourseFromDB()
        {
            var allTrainingCourse = _context.TrainingCourse.Where(t => !t.IsDeleted && t.CompanyId != Guid.Empty && t.IsActive == true)
            .Include(c => c.Company)
            .ThenInclude(u => u.CreatedBy)
            .ToList();
            if (allTrainingCourse.Any())
            {
                return allTrainingCourse;
            }
            return allTrainingCourse;
        }
        public TrainingCourse GetTrainingCourseById(int? Id)
        {
            var allTrainingCourse = _context.TrainingCourse.Where(t => t.Id == Id).FirstOrDefault();
            if (allTrainingCourse != null)
            {
                return allTrainingCourse;
            }
            return allTrainingCourse;
        }
        public Payments GetPaymentById(int? Id)
        {
            var payment = _context.Payments.Where(t => t.Id == Id).Include(c => c.Courses).Include(s => s.User).FirstOrDefault();
            if (payment != null)
            {
                return payment;
            }
            return payment;
        }
        public List<Payments> GetPaymentList()
        {
            var allPayments = _context.Payments.Include(p => p.User).Include(p => p.Courses).OrderByDescending(d => d.DateCreated).ToList();
            if (allPayments.Any())
            {
                return allPayments;
            }
            return allPayments;
        }
		public TrainingVideosViewModel GetTrainingVideos()
		{
			var allVideos = _context.TrainingVideos
				.Where(v => v.IsActive)
				.Include(c => c.Course)
				.ToList();
			var viewModel = new TrainingVideosViewModel
			{
				Videos = allVideos, 									
			};

			return viewModel;
		}

		public List<TrainingVideo> GetStudentPaidTrainingVideos(string userID)
        {

            var getListOfCourseStudentPaidFor = _context.Payments.Where(t => t.UserId == userID && t.Status == PaymentStatus.Approved).ToList();
            var paidCourseIDs = getListOfCourseStudentPaidFor.Select(x => x.CourseId).ToList();

            var paidCourseVideos = _context.TrainingVideos.Where(x => x.CourseId != 0 && x.IsActive && paidCourseIDs.Contains(x.CourseId)).Include(c => c.Course).ToList();
            if (paidCourseVideos.Any())
            {
                return paidCourseVideos;
            }
            return null;
        }
        public List<int?> GetListOfCourseIdStudentPaid4(string userID)
        {
            var list = new List<int?>();
            var getListOfCourseStudentPaidFor = _context.Payments.Where(t => t.UserId == userID && t.Status == PaymentStatus.Approved).ToList();
            var paidCourseIDs = getListOfCourseStudentPaidFor.Select(x => x.CourseId).ToList();
            if (paidCourseIDs.Any())
            {
                list = paidCourseIDs;
                return list;
            }
            return list;
        }
        public TrainingVideo GetVideosById(Guid Id)
        {
            var videos = _context.TrainingVideos.Where(t => t.Id == Id && t.IsActive).FirstOrDefault();
            if (videos != null)
            {
                return videos;
            }
            return videos;
        }
		public TestQuestions GetQuestionsById(int? Id)
		{
			var question = _context.TestQuestions.Where(t => t.Id == Id && !t.IsDeleted).FirstOrDefault();
			if (question != null)
			{
				return question;
			}
			return question;
		}
		public List<TestQuestions> GetTestQuestions()
		{
			var allQuestions = _context.TestQuestions.Where(q => !q.IsDeleted).OrderByDescending(a => a.Id).Include(c => c.Course).ToList();
			if (allQuestions.Any())
			{
				return allQuestions;
			}
			return allQuestions;
		}
		public List<TestQuestionsViewModel> GetTestQuestionsForPage1(int? Id)
		{
			var result = new List<TestQuestionsViewModel>();
			var testQuestion4Page1 = _context.TestQuestions.Where(q => q.CourseId == Id && q.IsActive && !q.IsDeleted).Include(c => c.Course).OrderBy(q => q.Id).Take(10).ToList();
			if (testQuestion4Page1.Any())
			{
				result = testQuestion4Page1.Select(x => new TestQuestionsViewModel()
				{
					OptionList = GetOptListByQuestionIds(x.Id),
					Question = x.Question,
					Answer = x.Answer,
					CourseId = (int)x.CourseId,
					Id = x.Id,
					IsActive = x.IsActive,
					IsDeleted = x.IsDeleted,
					DateCreated = x.DateCreated

				}).ToList();
				return result;
			}
			return result;
		}
		public List<TestQuestionsViewModel> GetTestQuestionsForPage2(int? Id)
		{
			var result = new List<TestQuestionsViewModel>();
			var testQuestion4Page2 = _context.TestQuestions.Where(q => q.CourseId == Id && q.IsActive && !q.IsDeleted).Include(c => c.Course).OrderByDescending(q => q.Id).Take(10).ToList();
			if (testQuestion4Page2.Any())
			{
				result = testQuestion4Page2.Select(x => new TestQuestionsViewModel()
				{
					OptionList = GetOptListByQuestionIds(x.Id),
					Question = x.Question,
					Answer = x.Answer,
					CourseId = (int)x.CourseId,
					Id = x.Id,
					IsActive = x.IsActive,
					IsDeleted = x.IsDeleted,
					DateCreated = x.DateCreated
				}).ToList();
				return result;
			}
			return result;
		}
		public List<string> GetOptListByQuestionIds(int? id)
		{
			var optList = new List<string>();
			var optListDetails = _context.AnswerOptions.Where(a => a.QuestionId == id).ToList();
			if (optListDetails.Any())
			{
				optList = optListDetails.Select(a => a.Option).ToList();
				return optList;
			}
			return null;
		}
        public async Task<List<ApplicationUser>> GetStudentListAsync()
        {
            var loggedInUser = Session.GetCurrentUser();
            if (loggedInUser != null)
            {
                var query = _context.ApplicationUsers
                .Where(c => !c.IsDeactivated && c.CompanyId == loggedInUser.CompanyId && (c.Role == "Student" || c.Role == "Applicant"))
                .OrderByDescending(d => d.DateRegistered);

                return await query.ToListAsync();
            }
            return new List<ApplicationUser>();
        }
        public List<ApplicationUserViewModel> GetTeacher()
        {
            var loggedInUser = Session.GetCurrentUser();

            return _context.ApplicationUsers
                 .Where(x => !x.IsDeactivated && x.CompanyId == loggedInUser.CompanyId && x.IsActivated && (x.Role == "Techer"))
                 .Include(y => y.Company)

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
                 Role = s.Role,
                 PhoneNumber = s.PhoneNumber,
                 IsProgram = s.IsProgram,
                 IsAdmin = false,
             }).ToList();
        }
        public ApplicationUser FindById(string Id)
        {
            return _context.ApplicationUsers.Where(s => s.Id == Id)?.Include(s => s.Company).FirstOrDefault();
        }
		public string GetRoleLayout()
		{
			var loggedInUser = Session.GetCurrentUser();
			if (loggedInUser.CompanyId == null)
			{
				loggedInUser = UpdateSessionAsync(loggedInUser.UserName).Result;

			}
			if (loggedInUser != null)
			{
				var superAdmin = loggedInUser.Roles.Contains(Session.Constants.SuperAdminRole);
				if (superAdmin)
				{
					return Session.Constants.SuperAdminLayout;
				}
				else if (!superAdmin)
				{
					var isCompanyAdmin = loggedInUser.Roles.Contains(Session.Constants.AdminRole);
					if (isCompanyAdmin)
					{
						return Session.Constants.AdminLayout;
					}
					else
					{
						var isStudent = loggedInUser.Roles.Contains(Session.Constants.StudentRole);
						if (isStudent)
						{
							return Session.Constants.StudentsLayout;
						}
					}
				}
			}
			return Session.Constants.DefaultLayout;
		}
		public void CreateUserLoginLog(string ipAddress, string userId, string systemName)
		{
			try
			{
				if (userId != null)
				{
					var userDetails = new UserLoginLog()
					{
						DeviceName = systemName,
						IPAddress = ipAddress,
						LoginDate = DateTime.Now,
						UserId = userId,
					};
					_context.UserLoginLogs.Add(userDetails);
					_context.SaveChanges();
				}
			}
			catch (Exception exp)
			{

				throw exp;
			}
		}
		//public IPagedList<UserLogsViewModel> GetLoggedInUsers(IPageListModel<UserLogsViewModel> model, DateTime daliyReportdate, int page)
		//{
		//	var userLogsViewModel = _context.UserLoginLogs.Where(c => c.UserId != null && c.IPAddress != null)
		//		.Include(x => x.User)
		//		.ThenInclude(x => x.CompanyBranch)
		//		.ThenInclude(x => x.Company)
		//		.AsQueryable();
		//	if (!string.IsNullOrEmpty(model.Keyword))
		//	{
		//		userLogsViewModel = userLogsViewModel.Where(v =>
		//			v.DeviceName.ToLower().Contains(model.Keyword.ToLower()) ||
		//			v.User.FirstName.ToLower().Contains(model.Keyword.ToLower()) ||
		//			v.User.CompanyBranch.Company.Name.ToLower().Contains(model.Keyword.ToLower()));
		//	}
		//	if (model.StartDate.HasValue)
		//	{
		//		userLogsViewModel = userLogsViewModel.Where(v => v.LoginDate >= model.StartDate);
		//	}
		//	if (model.EndDate.HasValue)
		//	{
		//		userLogsViewModel = userLogsViewModel.Where(v => v.LoginDate <= model.EndDate);
		//	}
		//	if (daliyReportdate != DateTime.MinValue)
		//	{
		//		userLogsViewModel = userLogsViewModel.Where(v => v.LoginDate.Date == daliyReportdate.Date);
		//	}
		//	var sentMessage = userLogsViewModel
		//		.OrderByDescending(v => v.LoginDate)
		//		 .Take(200)
		//	.Select(c => new UserLogsViewModel
		//	{
		//		IpAddress = c.IPAddress,
		//		CompanyName = c.User.CompanyBranch.Company.Name,
		//		UserName = c.User.FullName,
		//		DeviceName = c.DeviceName,
		//		LoggedDate = c.LoginDate,
		//	}).ToPagedList(page, 25);
		//	model.Model = sentMessage;
		//	return sentMessage;
		//}
		public async Task<ApplicationUser?> UpdateSessionAsync(string userEmail)
		{
			if (string.IsNullOrEmpty(userEmail))
			{
				return null;
			}

			try
			{
				var user = await _context.ApplicationUsers
					.Where(s => s.Email == userEmail)
					.Include(x => x.Company)
					.FirstOrDefaultAsync()
					.ConfigureAwait(false);

				if (user == null)
				{
					return null;
				}

				// Temporarily store createdBy details to avoid serialization issues
				string createdById = user?.Company?.CreatedById ?? string.Empty;
				ApplicationUser? createdBy = user.Company?.CreatedBy;
				if (user.Company != null)
				{
					user.Company.CreatedBy = null;
				}

				user.Roles = (List<string>)await _userManager.GetRolesAsync(user).ConfigureAwait(false);

				user.UserRole = user.Roles.Contains(Session.Constants.SuperAdminRole) ? Session.Constants.SuperAdminRole :
								user.Roles.Contains(Session.Constants.AdminRole) ? Session.Constants.AdminRole :
								user.Roles.Contains(Session.Constants.StudentRole) ? Session.Constants.StudentRole :
								Session.Constants.StudentRole;

				if (user.Company != null && user.CompanyId != Guid.Empty)
				{
					var garageSystemSettings = await _notificationHelper.GetNotificationSettings(user.CompanyId).ConfigureAwait(false);
					AppHttpContext.Current.Session.SetString("garageSystemSettings", JsonConvert.SerializeObject(garageSystemSettings));
				}

				user.PageStatisticsViews = GetPageStatistics(user.CompanyId, user.UserRole);

				AppHttpContext.Current.Session.SetString("myuser", JsonConvert.SerializeObject(user));

				//var roleId = _context.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault()?.RoleId;
				//if (roleId != null)
				//{
				//	var screenRoles = _context.ScreenRoles.Where(x => x.CompanyBranchId == user.CompanyBranchId && x.RoleId == roleId).ToList();
				//	if (screenRoles.Any())
				//	{
				//		AppHttpContext.Current.Session.SetString("accessRight", JsonConvert.SerializeObject(screenRoles));
				//	}
				//}

				if (createdById != string.Empty && createdBy != null)
				{
					user.Company.CreatedBy = createdBy;
					user.Company.CreatedById = createdById;
				}

				var ip = AppHttpContext.Current.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
				if (ip == "::1")
				{
					ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(addr => addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString() ?? "Unknown IP";
				}

				var systemName = AppHttpContext.Current.Request.Headers["User-Agent"].ToString();
				CreateUserLoginLog(ip, user.Id, systemName);

				return user;
			}
			catch (Exception e)
			{
				//LogCritical($"Failed to update session with this error: {e.InnerException?.Message ?? e.Message}");
				return null;
			}
		}

		public List<PageStatisticsView>? GetPageStatistics(Guid? companyId, string userRole)
		{
			var statReport = new List<PageStatisticsView>();
			if (companyId != Guid.Empty && userRole != null)
			{
				var reportsCount = _context.View_PageStatisticsViews.Where(x => x.CompanyId == companyId && x.RoleName == userRole).ToList();
				statReport = reportsCount.OrderByDescending(d => d.CountOfPage).Take(5).Select(f => new PageStatisticsView()
				{
					PageUrl = f.PageUrl,
					CompanyId = f.CompanyId,
					CountOfPage = f.CountOfPage,
					BranchName = f.BranchName,
					CompanyName = f.CompanyName,
					PageName = f.PageName,
					RoleName = f.RoleName
				}).ToList();
				return statReport;
			}
			return statReport;
		}
		public void GetAllUserAccessedPage(string fullUrl, string pageUrl, string pageName)
		{
			var loggedInUser = Session.GetCurrentUser();
			if (loggedInUser != null && loggedInUser.CompanyId != null)
			{
				var pagestat = new PageStatistics()
				{
					PageUrl = pageUrl,
					FullPageUrl = fullUrl,
					UserId = loggedInUser.Id,
					CompanyId = loggedInUser.CompanyId,
					PageDate = DateTime.Now,
					PageName = pageName,
					RoleName = loggedInUser.UserRole,
				};
				LogPageStatisticInHangfire(pagestat);
			}
		}
		public void SavePageStatistics(PageStatistics sat)
		{
			if (sat != null)
			{
				_context.PageStatistics.Add(sat);
				_context.SaveChanges();
			}
		}
		public void LogPageStatisticInHangfire(PageStatistics sat)
		{
			BackgroundJob.Enqueue(() => SavePageStatistics(sat));
		}

	}
}
