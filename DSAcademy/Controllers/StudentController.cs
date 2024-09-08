using Core.Config;
using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using Newtonsoft.Json;
using static Logic.AppHttpContext;

namespace DSAcademy.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDropdownHelper _dropdownHelper;
        private readonly IApplicationHelper _applicationHelper;
        private readonly IUserHelper _userHelper;
        private readonly IStudentHelper _studentHelper;
        private readonly IPaystackHelper _paystackHelper;
        private readonly IGeneralConfiguration _generalConfiguration;
        private readonly IEmailService _emailService;

        public StudentController
            (AppDbContext context,
            UserManager<ApplicationUser> userManger,
            SignInManager<ApplicationUser> signInManager,
            IDropdownHelper dropdownHelper,
            IApplicationHelper applicationHelper,
            IUserHelper userHelper,
            IStudentHelper studentHelper,
            IPaystackHelper paystackHelper,
            IGeneralConfiguration generalConfiguration,
            IEmailService emailService)
        {
            _context = context;
            _userManger = userManger;
            _signInManager = signInManager;
            _dropdownHelper = dropdownHelper;
            _applicationHelper = applicationHelper;
            _userHelper = userHelper;
            _studentHelper = studentHelper;
            _paystackHelper = paystackHelper;
            _generalConfiguration = generalConfiguration;
            _emailService = emailService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var dashBoardData = _studentHelper.DashboardBuildingServices();
            return View(dashBoardData);
        }

		[HttpGet]
		public JsonResult GetCourseOutLineById(Guid id)
		{
			try
			{
				if (id != Guid.Empty)
				{
					var courseOutLine = _userHelper.GetVideosById(id).Outline;
					return Json(courseOutLine);
				}
				return Json(new { isError = true, msg = "Failed" });

			}
			catch (Exception ex)
			{
				return Json(new { isError = true, msg = "An unexpected error occured " + ex.Message });
			}
		}

		[HttpPost]
		public JsonResult GetPaymentDetailss(string paymentDetails)
		{
			// Debugging to check if server receives data correctly
			Console.WriteLine($"Received paymentDetails JSON string: {paymentDetails}");

			if (!string.IsNullOrEmpty(paymentDetails))
			{
				var PaymentViewModel = JsonConvert.DeserializeObject<PaymentViewModel>(paymentDetails);
				if (PaymentViewModel?.Amount != null || PaymentViewModel.Amount > 0)
				{
					if (PaymentViewModel != null)
					{
						var paymentData = _userHelper.GetTrainingCourseById(PaymentViewModel.CourseId);
						if (paymentData != null)
						{
							return Json(new { isError = false, msg = "Payment Updated Successfully" });
						}
					}
				}
			}

			return Json(new { isError = true, msg = "Please fill the form correctly." });
		}

		[HttpPost]
		public JsonResult GetPaymentDetails(string paymentDetails)
		{
			// Log received data for debugging purposes
			Console.WriteLine($"Received paymentDetails JSON string: {paymentDetails}");

			if (string.IsNullOrWhiteSpace(paymentDetails))
			{
				return Json(new { isError = true, msg = "Please fill the form correctly." });
			}

			PaymentViewModel paymentViewModel;
			try
			{
				paymentViewModel = JsonConvert.DeserializeObject<PaymentViewModel>(paymentDetails);
			}
			catch (JsonException ex)
			{
				Console.WriteLine($"Error deserializing paymentDetails: {ex.Message}");
				return Json(new { isError = true, msg = "Invalid payment details format." });
			}

			if (paymentViewModel == null || paymentViewModel.CourseId <= 0)
			{
				return Json(new { isError = true, msg = "Invalid payment details." });
			}

			var paymentData = _userHelper.GetTrainingCourseById(paymentViewModel.CourseId);
			if (paymentData == null)
			{
				return Json(new { isError = true, msg = "Course not found." });
			}

			var user = Session.GetCurrentUserInfor();
			if (user == null)
			{
				return Json(new { isError = true, msg = "User session not found." });
			}

			var paymentLinkResult = _paystackHelper.GeneratePaymentParameters(paymentData, user).Result;
			if (paymentLinkResult?.data?.authorization_url == null)
			{
				return Json(new { isError = true, msg = "Failed to generate payment link." });
			}

			return Json(new { isError = false, paystackUrl = paymentLinkResult.data.authorization_url });
		}

        [HttpPost]
        public async Task<JsonResult> ManualPaymenUpload([FromBody] PaymentViewModel request)
        {
            if (request != null && !string.IsNullOrEmpty(request.ProveOfPayment))
            {
                try
                {
                    if (request.ProveOfPayment != null && request.Id > 0)
                    {
                        var userId = Session.GetCurrentUser().Id;

                        var paymentViewModel = new PaymentViewModel
                        {
                            Id = request.Id,
                            ProveOfPayment = request.ProveOfPayment,
                            UserId = userId
                        };

                        var proveSaved = await _studentHelper.UploadMaualPaymentProve(paymentViewModel, userId);

                        if (proveSaved != null)
                        {
                            return Json(new { isError = false, msg = "Uploaded Successfully. Your upload will be reviewed before you can access this course." });
                        }
                        else
                        {
                            return Json(new { isError = true, msg = "Failed to save the payment proof. Please try again." });
                        }
                    }

                    return Json(new { isError = true, msg = "Invalid payment proof received." });
                }
                catch (FormatException)
                {
                    return Json(new { isError = true, msg = "Invalid base64 string format. Please try again." });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); // Consider using a logging framework
                    return Json(new { isError = true, msg = "An unexpected error occurred. Please try again." });
                }
            }

            return Json(new { isError = true, msg = "Upload failed. No proof of payment received." });
        }


        [HttpPost]
		public JsonResult ManualPaymenUploads(int id,string ProveOfPayment)
		{
			if (ProveOfPayment != null)
			{
				var myPaymentProve = JsonConvert.DeserializeObject<PaymentViewModel>(ProveOfPayment);
				if (myPaymentProve != null)
				{
					var userId = Session.GetCurrentUser().Id;
					var proveSaved = _studentHelper.UploadMaualPaymentProve(myPaymentProve, userId);
					if (proveSaved != null)
					{
						return Json(new { isError = false, msg = "Uploaded Successfully. Your Upload will be reviewed before you can access this Course" });
					}
				}
			}
			return Json(new { isError = true, msg = "Upload Failed" });
		}
        public bool GetPaymentResponse(Paystack paystack)
        {
            if (paystack != null)
            {
                if (paystack != null || paystack.PaymentHistoryId > 0)
                {
                    var user = Session.GetCurrentUser();
                    var course = _userHelper.GetTrainingCourseById(paystack.PaymentsHistory.CourseId);
                    var completedpayment = _paystackHelper.VerifyPayment(paystack);

                    string toEmail = _generalConfiguration.AdminEmail;
                    string subject = "PAYMENT NOTIFICATION";
                    string message = "&#8358;" + paystack.amount + " has been credited to your Account by " + user.FullName +
                         " been payment for " + course.Title + " with Ref No: " + paystack.reference;

                    _emailService.SendAsync(toEmail, subject, message);
                    return true;
                }
            }
            return false;
        }
		[HttpGet]
		public IActionResult StudentCourses()
		{
			var userId = Session.GetCurrentUser().Id;
			var listOfallCourses = new List<TrainingCourse>();
			if (userId != null)
			{
				ViewBag.PaidCourseIdList = _userHelper.GetListOfCourseIdStudentPaid4(userId);
			}
			var allCourses = _studentHelper.GetAllTrainingCourseDB();
			if (allCourses != null)
			{
				listOfallCourses = allCourses;
				return View(listOfallCourses);
			}
			return View(listOfallCourses);
		}

		[HttpGet]
		public IActionResult TrainingVideos()
		{
			var newListVideos = new List<TrainingVideo>();
			var userId = Session.GetCurrentUser().Id;
			if (userId != null)
			{
				var listOfVideos = _userHelper.GetStudentPaidTrainingVideos(userId);
				if (listOfVideos != null)
				{
					return View(listOfVideos);
				}
			}
			return View(newListVideos);
		}
	}
}
