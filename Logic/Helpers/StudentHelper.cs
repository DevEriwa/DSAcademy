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
using System.Xml.Linq;

namespace Logic.Helpers
{
    public class StudentHelper : IStudentHelper
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IEmailHelper _emailHelper;

        public StudentHelper(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, AppDbContext context, IUserHelper userHelper, IEmailHelper emailHelper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _userHelper = userHelper;
            _emailHelper = emailHelper;
        }
        public List<TrainingCourse> GetAllTrainingCourseDB()
        {
            var allTrainingCourse = _context.TrainingCourse.Where(t => !t.IsDeleted && t.IsActive).ToList();
            if (allTrainingCourse.Any())
            {
                return allTrainingCourse;
            }
            return allTrainingCourse;
        }


		public async Task<Payments> UploadMaualPaymentProve(PaymentViewModel prove, string userId)
		{
			if (prove != null)
			{
				var myPayment = new Payments
				{
					Source = TransactionType.Transfer,
					InvoiceNumber = Generate().ToString(),
					ProveOfPayment = prove.ProveOfPayment,
					UserId = userId,
					CourseId = prove.Id,
					Status = PaymentStatus.Pending,
					DateCreated = DateTime.Now,
				};

				var saveCheck = await _context.Payments.AddAsync(myPayment);
				_context.SaveChanges();
				if (saveCheck != null)
				{
					return myPayment;
				}
			}
			return null;
		}
		//public ApplicantDocumment UpdateApplicantDocumentService(ApplicantDocumment applicantDetailsForUpdate)
		//{
		//    var applicantSubmittedDocument = new ApplicantDocumment
		//    {
		//        StudentId = Session.GetCurrentUser().Id,
		//        BVN = applicantDetailsForUpdate.BVN,
		//        FirstGuarantor = applicantDetailsForUpdate.FirstGuarantor,
		//        SecondGuarantor = applicantDetailsForUpdate.SecondGuarantor,
		//        NepaBill = applicantDetailsForUpdate.NepaBill,
		//        SignedContract = applicantDetailsForUpdate.SignedContract,
		//        IsApproved = false,
		//        DateCreated = DateTime.Now,
		//    };
		//    if (applicantSubmittedDocument != null)
		//    {
		//        _context.ApplicantDocumments.Add(applicantSubmittedDocument);
		//        _context.SaveChanges();
		//        return applicantSubmittedDocument;
		//    }
		//    return null;
		//}
		//public TestResult ListOfAnsweredQuestions(TestQuestionsViewModel collectedData)
		//{
		//    try
		//    {
		//        var correctAnswer = new List<TestQuestions>();
		//        var question = collectedData.AnsweredQuestions.Select(x => x.questionId).ToList();
		//        var answers = collectedData.AnsweredQuestions.Select(x => x.selectedAnswer).ToList();
		//        var questions = _context.TestQuestions.Where(x => x.Id != 0 && question.Contains(x.Id)).ToList();
		//        var courseId = _context.TestQuestions.Where(x => x.Id != 0 && question.Contains(x.Id)).FirstOrDefault().CourseId;
		//        if (questions.Any())
		//        {
		//            foreach (var ans in collectedData.AnsweredQuestions)
		//            {
		//                var myResult = questions.Where(q => q.Id == ans.questionId && q.Answer == ans.selectedAnswer).FirstOrDefault();
		//                if (myResult != null)
		//                {
		//                    correctAnswer.Add(myResult);
		//                }
		//            }
		//        }
		//        var resultCount = correctAnswer.Count();
		//        if (collectedData.ActionType == GeneralAction.CREATE)
		//        {
		//            var resultOne = new TestResult()
		//            {
		//                UserId = Session.GetCurrentUser().Id,
		//                CourseId = courseId,
		//                ResultOne = (resultCount * 10),
		//                TestOneChecker = true,
		//                ResultTwo = 0,
		//                TestTwoChecker = false,
		//                Total = (resultCount * 10) / 2,
		//            };

		//            var firstTestResult = _context.Add(resultOne);
		//            _context.SaveChanges();
		//            return resultOne;
		//        }
		//        else if (collectedData.ActionType == GeneralAction.UPDATE)
		//        {
		//            var resultTwo = _context.TestResults.Where(t => t.Id == collectedData.Id).FirstOrDefault();
		//            if (!resultTwo.TestTwoChecker)
		//            {
		//                if (resultTwo != null)
		//                {
		//                    resultTwo.ResultTwo = (resultCount * 10);
		//                    var result2Average = (resultCount * 10) / 2;
		//                    resultTwo.TestTwoChecker = true;
		//                    resultTwo.Total = (resultTwo.Total + result2Average);

		//                    var firstTestResult = _context.Update(resultTwo);
		//                    _context.SaveChanges();

		//                    return resultTwo;
		//                }
		//            }
		//            return resultTwo;
		//        }
		//        return null;

		//    }
		//    catch (Exception ex)
		//    {

		//        throw ex;
		//    }
		//}
		public TestResult GetStudentResult(int? Id, string userID)
        {
            try
            {

                var myResult = _context.TestResults.Where(r => r.CourseId == Id && r.UserId == userID).FirstOrDefault();
                if (myResult != null)
                {
                    return myResult;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public TestResult CheckIfTestOneHasBeebTakenByCurrentUser(int? id, string username)
        {
            try
            {
                if (id != null && username != null)
                {
                    var user = _userHelper.FindByUserNameAsync(username).Result;
                    var testOneStatusCheck = _context.TestResults.Where(r => r.UserId == user.Id && r.CourseId == id).FirstOrDefault();
                    if (testOneStatusCheck != null)
                    {
                        return testOneStatusCheck;
                    }
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TrainingCourse GetTrainingCourseById(int? id)
        {
            try
            {
                var courses = _context.TrainingCourse.Where(t => t.Id == id && !t.IsDeleted && t.IsActive).FirstOrDefault();
                if (courses != null)
                {
                    return courses;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //public bool DeosApplicantDocummentExist(string username)
        //{
        //    if (username != null)
        //    {
        //        var loggedInstudentDetails = _userManager.Users.Where(s => s.UserName == username).FirstOrDefault();
        //        var applicantHasApplied = _context.ApplicantDocumments.Where(x => x.StudentId == loggedInstudentDetails.Id).Any();
        //        if (applicantHasApplied)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;

        //}
        //public ApplicantDocumment UpdateApplicantDocumentsInfo(ApplicantDocumment applicantDocuments)
        //{
        //    try
        //    {
        //        if (applicantDocuments != null)
        //        {
        //            var studentDetails = _context.ApplicantDocumments.Where(d => d.Id == applicantDocuments.Id).FirstOrDefault();
        //            if (studentDetails != null)
        //            {
        //                studentDetails.BVN = applicantDocuments.BVN;
        //                studentDetails.FirstGuarantor = applicantDocuments.FirstGuarantor;
        //                studentDetails.SecondGuarantor = applicantDocuments.SecondGuarantor;
        //                studentDetails.NepaBill = applicantDocuments.NepaBill;
        //                studentDetails.SignedContract = applicantDocuments.SignedContract;
        //                studentDetails.IsApproved = false;

        //                _context.Update(studentDetails);
        //                _context.SaveChanges();
        //                return studentDetails;
        //            }
        //            return null;
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        //public ProjectTopic UploadProjectTopicServices(ProjectTopicViewModel topics, string userId)
        //{
        //    if (topics != null)
        //    {
        //        var topicstDetails = new ProjectTopic
        //        {
        //            UserId = userId,
        //            CourseId = topics.CourseId,
        //            Title = topics.Title,
        //            Description = topics.Description,
        //            IsApproved = false,
        //            LinksIsAdded = false,
        //            Status = ProjectStatus.New,
        //            DatePosted = DateTime.Now

        //        };
        //        var newlyAddTopic = _context.ProjectTopics.Add(topicstDetails);
        //        _context.SaveChanges();
        //        if (newlyAddTopic != null)
        //        {
        //            return topicstDetails;
        //        }
        //    }
        //    return null;

        //}
        //public ProjectTopic ProjectLinksUpdateServices(ProjectTopicViewModel topics)
        //{
        //    if (topics != null)
        //    {
        //        var myApprovedTopic = _userHelper.GetProjectTopicById(topics.Id);

        //        if (myApprovedTopic != null)
        //        {
        //            myApprovedTopic.GitLink = topics.GitLink;
        //            myApprovedTopic.RedmineLink = topics.RedmineLink;
        //            myApprovedTopic.LinksIsAdded = true;
        //            myApprovedTopic.Status = ProjectStatus.Started;

        //            var result = _context.ProjectTopics.Update(myApprovedTopic);
        //            _context.SaveChanges();

        //            return myApprovedTopic;
        //        }
        //    }

        //    return null;
        //}
        //public ProjectTopic GetProjectLinksServices(int id)
        //{
        //    if (id != 0)
        //    {
        //        var myLinks = _context.ProjectTopics.Where(t => t.Id == id).FirstOrDefault();
        //        if (myLinks != null)
        //        {
        //            return myLinks;
        //        }
        //    }
        //    return null;

        //}
        //public List<ProjectTopic> GetListOfStutentsProjectTopic(string userId)
        //{
        //    var allStutentsProjectTopic = _context.ProjectTopics.Where(p => p.UserId == userId).Include(c => c.Course).ToList();
        //    if (allStutentsProjectTopic.Any())
        //    {
        //        return allStutentsProjectTopic;
        //    }
        //    return null;
        //}
        //public List<ProjectTopic> GetListOfStutentsApprovedProjectTopic(string userId)
        //{
        //    var allStutentsProjectTopic = _context.ProjectTopics.Where(p => p.UserId == userId && p.IsApproved).Include(c => c.Course).ToList();
        //    if (allStutentsProjectTopic.Any())
        //    {
        //        return allStutentsProjectTopic;
        //    }
        //    return null;
        //}
        //public async Task<Payments> UploadMaualPaymentProve(PaymentsViewModel prove, string userId)
        //{
        //    if (prove != null)
        //    {
        //        var myPayment = new Payments
        //        {
        //            Source = TransactionType.Transfer,
        //            InvoiceNumber = Generate().ToString(),
        //            ProveOfPayment = prove.ProveOfPayment,
        //            UserId = userId,
        //            CourseId = prove.Id,
        //            Status = PaymentStatus.Pending,
        //            Date = DateTime.Now,
        //        };

        //        var saveCheck = await _context.Payments.AddAsync(myPayment);
        //        _context.SaveChanges();
        //        if (saveCheck != null)
        //        {
        //            return myPayment;
        //        }
        //    }
        //    return null;
        //}
        public static int Generate()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            return rand.Next(100000000, 999999999);
        }
        //public List<JobViewModels> GetListOfAvailableJobs(string userID)
        //{
        //    var result = new List<JobViewModels>();
        //    var availableJobs = _context.Jobs.Where(t => t.IsActive).ToList();

        //    if (availableJobs.Any())
        //    {
        //        var appliedChecker = GetListOfJobIdsUserHaveAppliedFor(userID);
        //        result = availableJobs.Select(x => new JobViewModels()
        //        {
        //            Id = x.Id,
        //            CompanyName = x.CompanyName,
        //            Title = x.Title,
        //            Salary = x.Salary,
        //            Type = x.Type,
        //            Description = x.Description,
        //            Responsibilities = x.Responsibilities,
        //            Requirements = x.Requirements,
        //            IsActive = x.IsActive,
        //            DateCreated = x.DateCreated.ToString("R"),
        //            DescriptionList = _userHelper.SplitStringToList(x.Description),
        //            Applied = CheckIfUserHaveAppliedForThisJob(appliedChecker, x.Id),
        //        }).OrderByDescending(i => i.Id).ToList();
        //        return result;
        //    }
        //    return null;
        //}
        //public List<JobViewModels> GetListOfAvailableJobsByJobType(string userID, JobType? id)
        //{
        //    var result = new List<JobViewModels>();
        //    if (userID != null && id != null && id > 0)
        //    {
        //        var availableJobs = _context.Jobs.Where(t => t.IsActive && t.Type == id).ToList();

        //        if (availableJobs.Any())
        //        {
        //            var appliedChecker = GetListOfJobIdsUserHaveAppliedFor(userID);
        //            result = availableJobs.Select(x => new JobViewModels()
        //            {
        //                Id = x.Id,
        //                CompanyName = x.CompanyName,
        //                Title = x.Title,
        //                Salary = x.Salary,
        //                Type = x.Type,
        //                Description = x.Description,
        //                Responsibilities = x.Responsibilities,
        //                Requirements = x.Requirements,
        //                IsActive = x.IsActive,
        //                DateCreated = x.DateCreated.ToString("R"),
        //                DescriptionList = _userHelper.SplitStringToList(x.Description),
        //                Applied = CheckIfUserHaveAppliedForThisJob(appliedChecker, x.Id),
        //            }).OrderByDescending(i => i.Id).ToList();
        //            return result;
        //        }
        //        return result;
        //    }
        //    return result;
        //}
        //public async Task<JobApplication> JobApplicationServices(int jobId, ApplicationUserViewModel user)
        //{
        //    if (user != null && jobId > 0)
        //    {
        //        var jobApplication = new JobApplication
        //        {
        //            UserId = user.Id,
        //            JobId = jobId,
        //            DateApplied = DateTime.Now,
        //        };

        //        var saveCheck = await _context.JobApplications.AddAsync(jobApplication);
        //        _context.SaveChanges();
        //        if (saveCheck != null)
        //        {
        //            var jobDetails = _context.Jobs.Where(j => j.Id == jobId).FirstOrDefault();
        //            _emailHelper.SendMailToAdminOnJobApplication(user, jobDetails);
        //            return jobApplication;
        //        }
        //    }
        //    return null;
        //}
        //public List<int> GetListOfJobIdsUserHaveAppliedFor(string userID)
        //{
        //    var getListJobId = _context.JobApplications.Where(t => t.UserId == userID).ToList();
        //    var appliedJobIDs = getListJobId.Select(x => x.JobId).ToList();
        //    if (appliedJobIDs.Any())
        //    {
        //        return appliedJobIDs;
        //    }
        //    return null;
        //}
        public bool CheckIfUserHaveAppliedForThisJob(List<int> getListJobId, int id)
        {
            if (getListJobId != null && getListJobId.Contains(id))
            {
                return true;
            }
            return false;
        }
        //public bool CheckIfUserIsQualifiedToApplyForJobs(string userId)
        //{
        //    if (userId != null)
        //    {
        //        var projectCompletetionCheck = _context.ProjectTopics.Where(t => t.UserId == userId && t.Status == ProjectStatus.Accepted).FirstOrDefault();
        //        {
        //            if (projectCompletetionCheck != null)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}
        /// <summary>
        /// Building up all the content in students dashboard
        /// </summary>
        /// <param name="userName">DashboardBuildingServices</param>
        /// <returns>StudentDashBoardViewModel</returns>
        public StudentDashBoardViewModel DashboardBuildingServices()
        {
            var adminDashboardData = new StudentDashBoardViewModel();
            var loggedInUser = Session.GetCurrentUser();
            if (loggedInUser != null)
            {
                adminDashboardData.Users = loggedInUser;
                adminDashboardData.TestResult = _context.TestResults.Where(r => r.UserId == loggedInUser.Id).Include(c => c.Course).ToList();
                //adminDashboardData.ListOfRecentJobs = _context.Jobs.Where(j => j.IsActive).OrderByDescending(j => j.DateCreated).Take(2).ToList();
               //adminDashboardData.IsProjectCompleted = CheckIfUserIsQualifiedToApplyForJobs(loggedInUser.Id);
                adminDashboardData.TotalNumberCourses = _context.TrainingCourse.Where(c => c.IsActive && !c.IsDeleted).Count();
                adminDashboardData.TotalNumberExamTaken = _context.TestResults.Where(c => c.UserId == loggedInUser.Id && c.TestOneChecker && c.TestTwoChecker).Count();
                return adminDashboardData;
            }
            return adminDashboardData;
        }
        //public EmployementData EMploymentDataServer(EmployementDataViewModel employmentData, string userId)
        //{
        //    if (employmentData != null && userId != null)
        //    {
        //        var employmentInfo = new EmployementData
        //        {
        //            UserId = userId,
        //            CompanyName = employmentData.CompanyName,
        //            CompanyAddress = employmentData.CompanyAddress,
        //            CompanyState = employmentData.CompanyState,
        //            CompanyEmail = employmentData.CompanyEmail,
        //            CompanyHrPhoneNO = employmentData.CompanyHrPhoneNO,
        //            MonthlyPaymentDate = employmentData.MonthlyPaymentDate,
        //            JobTitle = employmentData.JobTitle,
        //            OfferLetter = employmentData.OfferLetter,
        //            Salary = employmentData.Salary,
        //            MonthlyDeduction = employmentData.MonthlyDeduction,
        //            IsApproved = false,
        //            DateCreated = DateTime.Now,
        //        };

        //        var saveCheck = _context.EmployementData.Add(employmentInfo);
        //        _context.SaveChanges();
        //        if (saveCheck != null)
        //        {
        //            return employmentInfo;
        //        }
        //    }
        //    return null;
        //}
        //public InterviewTestResult InterviewAnswerComputation(InterviewQuestionsViewModel collectedData)
        //{
        //    try
        //    {
        //        var user = Session.GetCurrentUser();
        //        var correctAnswer = new List<InterviewQuestions>();
        //        var question = collectedData.InterviewAnsweredQuestions.Select(x => x.questionId).ToList();
        //        var answers = collectedData.InterviewAnsweredQuestions.Select(x => x.selectedAnswer).ToList();
        //        var questions = _context.InterviewQuestions.Where(x => x.Id != 0 && question.Contains(x.Id)).ToList();
        //        if (questions.Any())
        //        {
        //            foreach (var ans in collectedData.InterviewAnsweredQuestions)
        //            {
        //                var myResult = questions.Where(q => q.Id == ans.questionId && q.Answer == ans.selectedAnswer).FirstOrDefault();
        //                if (myResult != null)
        //                {
        //                    correctAnswer.Add(myResult);
        //                }
        //            }
        //        }

        //        var resultCount = correctAnswer.Count();
        //        if (correctAnswer != null)
        //        {
        //            var result = new InterviewTestResult()
        //            {
        //                UserId = user.Id,
        //                TotalScore = (resultCount * 5),
        //                DateWritten = DateTime.Now,
        //            };

        //            _context.InterviewTestResults.Add(result);
        //            _context.SaveChanges();
        //            return result;
        //        }
        //        return null;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
    }
}
