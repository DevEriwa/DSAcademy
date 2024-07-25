using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DSAcademy.Controllers
{
	public class AccountsController : Controller
	{
		private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserHelper _userHelper;
        private readonly IApplicationHelper _applicationHelper;
        private readonly IEmailHelper _emailHelper;

        public AccountsController(AppDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserHelper userHelper,
            IApplicationHelper applicationHelper,
            IEmailHelper emailHelper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _userHelper = userHelper;
            _applicationHelper = applicationHelper;
            _emailHelper = emailHelper;
        }



        // ADMIN REGISTRAION GET ACTION
        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        // ADMIN REGISTRAION POST 
        [HttpPost]
        public async Task<JsonResult> AdminRegisteration(string applicationUserViewModel)
        {
            try
            {
                var newApplicant = JsonConvert.DeserializeObject<ApplicationUserViewModel>(applicationUserViewModel);

                // Query the user details if it exists in the Db B4 Authentication
                var emailCheck = await _userHelper.FindByEmailAsync(newApplicant.Email);
                if (emailCheck != null)
                {
                    return Json(new { isError = true, msg = "Email already exists" });
                }
                // End of Query for the user details if it exists in the Db B4 Authentication

                var returndResultFrmRegisterService = await _applicationHelper.RegisterAdminService(newApplicant);
                if (returndResultFrmRegisterService != null)
                {
                    var userToken = await _emailHelper.CreateUserToken(newApplicant.Email);
                    var addToRole = await _userManager.AddToRoleAsync(returndResultFrmRegisterService, "Admin");
                    if (addToRole.Succeeded && userToken != null)
                    {
                        string linkToClick = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Accounts/EmailVerified?token={userToken.Token}";
                        _emailHelper.VerificationEmail(newApplicant.Email, linkToClick);
                        return Json(new { isError = false, msg = "Registration successful, check your mail to complete the application" });
                    }
                }
                return Json(new { isError = true, msg = "Application failed" });
            }
            catch (Exception ex)
            {
                // Log or return the inner exception message
                return Json(new { isError = true, msg = $"An unexpected error occurred: {ex.InnerException?.Message ?? ex.Message}" });
            }
        }


        [HttpGet]
		public IActionResult RegisterStudent()
		{
			return View();
		}

		[HttpPost]
		public IActionResult StudentRegisteration()
		{
			return View();
		}
		
		
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
	}
}
