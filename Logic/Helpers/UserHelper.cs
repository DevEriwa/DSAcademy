using Core.Db;
using Core.Enum;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Logic.Helpers
{
	public class UserHelper: IUserHelper
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        public UserHelper(
            UserManager<ApplicationUser> 
            userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
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
        public List<TrainingVideo> GetTrainingVideos()
        {
            var allVideos = _context.TrainingVideos.Where(v => v.IsActive).Include(c => c.Course).ToList();
            if (allVideos.Any())
            {
                return allVideos;
            }
            return allVideos;
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
    }
}
