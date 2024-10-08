﻿using Core.Db;
using Core.Enum;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Logic.AppHttpContext;
using Microsoft.EntityFrameworkCore;


namespace DSAcademy.Controllers
{
	//[SessionTimeout]
	//[Authorize(Roles = "Admin,SuperAdmin")]
	public class AdminController : Controller
    {
		private readonly IUserHelper _userHelper;
		private readonly IStudentHelper _studentHelper;
		private readonly IAdminHelper _adminHelper;
		private readonly IApplicationHelper _applicationHelper;
		private readonly IDropdownHelper _dropdownHelper;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly AppDbContext _context;
		private readonly IEmailHelper _emailHelper;
		private const string Create_Training_Cost_ActionType = "CreateTrainingCourse";
		private const string Edit_Training_Cost_ActionType = "EditTrainingCourse";
		private const string Activate_Training_Cost_ActionType = "ActivateTrainingCourse";
		private const string Deactivate_Training_Cost_ActionType = "DeactivateTrainingCourse";
		private const string Delete_Training_Cost_ActionType = "DeleteTrainingCourse";
        public AdminController(
            IUserHelper userHelper,
            IStudentHelper studentHelper,
            IAdminHelper adminHelper,
            IApplicationHelper applicationHelper,
            IDropdownHelper dropdownHelper,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            AppDbContext context)
        {
            _userHelper = userHelper;
            _studentHelper = studentHelper;
            _adminHelper = adminHelper;
            _applicationHelper = applicationHelper;
            _dropdownHelper = dropdownHelper;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
		public IActionResult Index()
        {
			var adminDashboard = _adminHelper.GetAdminDashboardData();
			if(adminDashboard != null)
			{
               return View(adminDashboard);
            }
            return View();
        }
		[HttpGet]
		public IActionResult OnboardingStudent()
		{
			var onboardingStudentList = _userHelper.GetAllOnboardApplicantsFromDB();
			return View(onboardingStudentList);
		}

		// POST ACTION FOR ACCEPT & REJECT APPLICATION
		[HttpPost]
		public JsonResult ApplicationStatusPost(string applicantDetails)
		{
			try
			{
				if (applicantDetails == null)
				{
					return Json(new { isError = true, msg = "Aborted" });
				}
				var applicantData = JsonConvert.DeserializeObject<ApplicationUser>(applicantDetails);
				if (applicantData != null && applicantData.Status == ApplicationStatus.Accepted)
				{
					var applicant = _applicationHelper.AcceptSelectedApplication(applicantData);
					if (applicant != null)
					{
						var removeUserFromRole = _userManager.RemoveFromRoleAsync(applicant, "Applicant").Result;
						if (removeUserFromRole.Succeeded)
						{
							var addToRole = _userManager.AddToRoleAsync(applicant, "Student").Result;
							if (addToRole.Succeeded)
							{
								return Json(new { isError = false, msg = "Application accepted successfully" });
							}
						}
					}
				}
				else if (applicantData != null && applicantData.Status == ApplicationStatus.Rejected)
				{
					var applicant = _applicationHelper.RejectSelectedApplication(applicantData);
					if (applicant != null)
					{
						return Json(new { isError = false, msg = "Application rejected successfully" });
					}
				}
				return Json(new { isError = true, msg = "Failed" });
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpGet]
		public JsonResult GetApplicationProfileById(string Id)
		{
			try
			{
				if (Id != null)
				{
					var application = _userManager.FindByIdAsync(Id).Result;
					return Json(application);
				}
				else
				{
					return Json(new { isError = true, msg = "Not Found" });
				}
			}
			catch (Exception ex)
			{
				return Json(new { isError = true, msg = "An unexpected error occured " + ex.Message });
			}
		}
		// TRAINING COST SETUP
		public async Task<IActionResult> TrainingCourse()
		{
			var username = User.Identity.Name;
			ViewBag.Program = _dropdownHelper.GetProgramEnumsList();
			ViewBag.Layout = _applicationHelper.GetUserLayout(username).FirstOrDefault();
			var allTrainingCourse = _userHelper.GetAllTrainingCourseFromDB();
			return View(allTrainingCourse);
		}

		public async Task<IActionResult> TrainingVideos()
		{
			ViewBag.AllCourses = _dropdownHelper.DropdownOfCourses();
			var username = User.Identity.Name;
			ViewBag.Layout = _applicationHelper.GetUserLayout(username).FirstOrDefault();

			// Fetch the view model
			var viewModel = _userHelper.GetTrainingVideos();

			// Return the view with the view model
			return View(viewModel);
		}
		[HttpPost]
		public JsonResult TrainingCoursePost(string collectedTrainingData, string base64)
		{
			try
			{
				var userName = _userHelper.FindByEmailAsync(User.Identity.Name).Result;
				if (collectedTrainingData != null)
				{
					var rawTrainingData = JsonConvert.DeserializeObject<TrainingCourseViewModel>(collectedTrainingData);
					rawTrainingData.User = userName;
					if (rawTrainingData.ActionType == Create_Training_Cost_ActionType)
					{
						var newTrainingCourse = _adminHelper.AddTrainignCostServices(rawTrainingData, base64);
						if (newTrainingCourse != null)
						{
							return Json(new { isError = false, msg = "Added Successfully" });
						}
					}
					else if (rawTrainingData.ActionType == Edit_Training_Cost_ActionType)
					{
						var costToEdit = _adminHelper.EditTrainignCostServices(rawTrainingData);
						if (costToEdit != null)
						{
							return Json(new { isError = false, msg = "Update Successfully" });
						}
					}
					else if (rawTrainingData.ActionType == Activate_Training_Cost_ActionType)
					{

						if (rawTrainingData.Id != null)
						{
							var costToActivate = _adminHelper.ActivateTrainignCostServices(rawTrainingData);
							if (costToActivate != null)
							{
								return Json(new { isError = false, msg = "Activated Successfully" });
							}
						}
					}
					else if (rawTrainingData.ActionType == Deactivate_Training_Cost_ActionType)
					{
						if (rawTrainingData.Id != null)
						{
							var costToDisable = _adminHelper.DisableTrainignCostServices(rawTrainingData);
							if (costToDisable != null)
							{
								return Json(new { isError = false, msg = "Deactivated Successfully" });
							}
						}
					}
					else if (rawTrainingData.ActionType == Delete_Training_Cost_ActionType)
					{
						if (rawTrainingData.Id != null)
						{
							var costToDelete = _adminHelper.DeleteTrainignCostServices(rawTrainingData);
							if (costToDelete != null)
							{
								return Json(new { isError = false, msg = "Deleted Successfully" });
							}
						}
					}
				}
				return Json(new { isError = true, msg = "Failed" });
			}
			catch (Exception)
			{
				throw;
			}
		}

		// TRAINING COST SETUP  GET ACTION
		[HttpGet]
		public JsonResult GetTrainingCourseById(int? Id)
		{
			try
			{
				if (Id != null)
				{
					var editedTrainingCourse = _userHelper.GetTrainingCourseById(Id);
					return Json(editedTrainingCourse);
				}
				return Json(new { isError = true, msg = "Failed" });

			}
			catch (Exception ex)
			{
				return Json(new { isError = true, msg = "An unexpected error occured " + ex.Message });
			}
		}
        [HttpGet]
        public ActionResult GetStudentLink(Guid? companyId)
        {
            try
            {
				if(companyId == Guid.Empty)
                    return Json(new { isError = true, msg = "Error occured" });

                var company = _context.Companies.FirstOrDefault(x => x.Id == companyId);
                if (company != null)
                {
                    string linkToClick = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Accounts/RegisterStudent?id={company.Id}";
                    return Content(linkToClick);
                }
                return Json(new { isError = true, msg = "Failed" });
            }
            catch (Exception ex)
            {
                return Json(new { isError = true, msg = "An unexpected error occured " + ex.Message });
            }
        }
		[HttpGet]
		public IActionResult Teacher()
		{
			var company = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
			var allTeacher =  _userHelper.GetTeacher(company.CompanyId);
			if (allTeacher != null)
			{
				return View(allTeacher);
			}
			return View();
		}
		[HttpPost]
		public async Task<JsonResult> AddTeacher(string teacherDetails)
		{
			try
			{
				if (teacherDetails != null)
				{
					var addTeacher = JsonConvert.DeserializeObject<ApplicationUserViewModel>(teacherDetails);
					var emailCheck = await _userHelper.FindByEmailAsync(addTeacher.Email);
					if (emailCheck != null)
					{
						return Json(new { isError = true, msg = "Email already exists" });
					}
					var phoneNumberCheck = await _userHelper.FindByPhoneNumberAsync(addTeacher.PhoneNumber);
					if (phoneNumberCheck != null)
					{
						return Json(new { isError = true, msg = "PhoneNumber already exists" });
					}
					var user = _userHelper.FindByEmailAsync(User.Identity.Name).Result;
					var company = await _userHelper.FindCompanyByUserId(user.Id);
					addTeacher.CompanyId = company.Id;
					var newTeacher = await _applicationHelper.RegisterTeacher(addTeacher);
					if (newTeacher != null)
					{
						var addToRole = _userManager.AddToRoleAsync(newTeacher, "Techer").Result;
						var userToken = await  _emailHelper.CreateUserToken(addTeacher.Email);
						if (addToRole.Succeeded & userToken != null)
						{
							string linkToClick = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Accounts/EmailVerified?token={userToken.Token}";
							_emailHelper.TeacherVerificationEmail(addTeacher.Email, linkToClick);
							return Json(new { isError = false, msg = "Registeration successful" });
						}
					}
				}
				return Json(new { isError = true, msg = "Error occurred" });
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		[HttpGet]
		public JsonResult GetTecherById(string? techerId)
		{
			try
			{
				if (techerId != null)
				{
					var editedTecher = _userHelper.GetTecherById(techerId);
					return Json(editedTecher);
				}
				return Json(new { isError = true, msg = "Failed" });
			}
			catch (Exception ex)
			{
				return Json(new { isError = true, msg = "An unexpected error occured " + ex.Message });
			}
		}
		[HttpPost]
		public JsonResult EditTecher(string teacherDetails)
		{
			try
			{
				if (teacherDetails != null)
				{
					var techer = JsonConvert.DeserializeObject<ApplicationUserViewModel>(teacherDetails);
                    if (techer != null)
					{
						var techerInfor = _adminHelper.EditTecher(techer);
						if (techerInfor != null)
						{
							return Json(new { isError = false, msg = "Update successfully" });
						}
					}
				}
				return Json(new { isError = true, msg = "Failed" });
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		public JsonResult DeleteTecher(string techerId)
		{
			try
			{
				if (techerId != null)
				{
					var techerInfor = _adminHelper.DeleteTecher(techerId);
					if (techerInfor != null)
					{
						return Json(new { isError = false, msg = "Deleted successfully" });
					}
				}
				return Json(new { isError = true, msg = "Failed" });
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public JsonResult GetPaymentById(int? Id)
		{
			try
			{
				if (Id != null)
				{
					var payment4Action = _userHelper.GetPaymentById(Id);
					return Json(payment4Action);
				}
				return Json(new { isError = true, msg = "Failed" });

			}
			catch (Exception ex)
			{

				return Json(new { isError = true, msg = "An unexpected error occured " + ex.Message });
			}
		}


		[HttpGet]
		public IActionResult Payments()
		{
			var username = User.Identity.Name;
			ViewBag.Layout = _applicationHelper.GetUserLayout(username).FirstOrDefault();
			var payment = _userHelper.GetPaymentList();
			return View(payment);
		}

		// POST ACTION FOR ALL TRAINING COST SETUP
		[HttpPost]
		public JsonResult PaymentPostAction(string collectedPaymentID)
		{
			try
			{
				if (collectedPaymentID != null)
				{
					var payment = JsonConvert.DeserializeObject<Payments>(collectedPaymentID);

					if (payment.Status == PaymentStatus.Approved)
					{

						if (payment.Id > 0)
						{
							var paymentActivate = _adminHelper.ApproveSelectedPayment(payment);
							if (paymentActivate != null)
							{
								return Json(new { isError = false, msg = "Approved Successfully" });
							}
						}

					}
					else if (payment.Status == PaymentStatus.Declined)
					{
						if (payment.Id != null)
						{
							var paymentToDecline = _adminHelper.DeclineSelectedPaymment(payment);
							if (paymentToDecline != null)
							{
								return Json(new { isError = false, msg = "Declined Successfully" });
							}
						}
					}
				}
				return Json(new { isError = true, msg = "Failed" });
			}
			catch (Exception ex)
			{
				return Json(new { isError = true, msg = "An unexpected error occured " + ex.Message });
			}
		}
	}
}
