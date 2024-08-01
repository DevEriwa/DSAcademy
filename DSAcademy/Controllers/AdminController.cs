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
			UserManager<ApplicationUser> userManager)
		{
			_userHelper = userHelper;
			_studentHelper = studentHelper;
			_adminHelper = adminHelper;
			_applicationHelper = applicationHelper;
			_dropdownHelper = dropdownHelper;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public IActionResult Index()
        {
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
			//var username = User.Identity.Name;
			//ViewBag.Layout = _applicationHelper.GetUserLayout(username).FirstOrDefault();
			//var allTrainingCourse = _userHelper.GetAllTrainingCourseFromDB();
			return View(/*allTrainingCourse*/);
		}

		// POST ACTION FOR ALL TRAINING COST SETUP
		[HttpPost]
		public JsonResult TrainingCoursePost(string collectedTrainingData)
		{
			try
			{
				if (collectedTrainingData != null)
				{
					var rawTrainingData = JsonConvert.DeserializeObject<TrainingCourseViewModel>(collectedTrainingData);

					if (rawTrainingData.ActionType == Create_Training_Cost_ActionType)
					{
						var newTrainingCourse = _adminHelper.AddTrainignCostServices(rawTrainingData);
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
	}
}
