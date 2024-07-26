﻿using Core.Db;
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
        private readonly IDropdownHelper _dropdownHelper;

        public AccountsController(AppDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserHelper userHelper,
            IApplicationHelper applicationHelper,
            IEmailHelper emailHelper,
            IDropdownHelper dropdownHelper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _userHelper = userHelper;
            _applicationHelper = applicationHelper;
            _emailHelper = emailHelper;
            _dropdownHelper = dropdownHelper;
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
            ViewBag.YesOrNo = _dropdownHelper.GetDropDownEnumsList();
            return View();
        }
        //REGISTRAION POST 
        [HttpPost]
        public async Task<JsonResult> StudentRegisteration(string userDetails)
        {
            try
            {
                var newApplicant = JsonConvert.DeserializeObject<ApplicationUserViewModel>(userDetails);
                ViewBag.YesOrNo = _dropdownHelper.GetDropDownEnumsList();
                var emailCheck = await _userHelper.FindByEmailAsync(newApplicant.Email);
                var usernameCheck = await _userHelper.FindByUserNameAsync(newApplicant.UserName);
                var phoneCheck = await _userHelper.FindByPhoneNumberAsync(newApplicant.PhoneNumber);
                if (emailCheck != null)
                {
                    return Json(new { isError = true, msg = "Email already exist" });
                }
                if (usernameCheck != null)
                {
                    return Json(new { isError = true, msg = "UserName already exist" });
                }
                if (phoneCheck != null)
                {
                    return Json(new { isError = true, msg = "Phone number already exist" });
                }
                if (newApplicant.Password != newApplicant.ConfirmPassword)
                {
                    return Json(new { isError = true, msg = "Password and Confirm password must match" });
                }
                var returndResultFrmRegisterService = _applicationHelper.RegisterApplicantService(newApplicant).Result;
                if (returndResultFrmRegisterService != null)
                {
                    var userToken = await _emailHelper.CreateUserToken(newApplicant.Email);
                    var addToRole = _userManager.AddToRoleAsync(returndResultFrmRegisterService, "Applicant").Result;
                    if (addToRole.Succeeded & userToken != null)
                    {
                        string linkToClick = HttpContext.Request.Scheme.ToString() + "://"
                        + HttpContext.Request.Host.ToString() + "/Accounts/EmailVerified?token=" + userToken.Token;
                        _emailHelper.VerificationEmail(newApplicant.Email, linkToClick);
                        return Json(new { isError = false, msg = "Registeration successful, Check your mail to complete application" });
                    }
                }
                return Json(new { isError = true, msg = "Application Failed" });
            }
            catch (Exception ex)
            {
                return Json(new { isError = true, msg = "An unexpected error occured " + ex.Message });
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login POST ACTION
        [HttpPost]
        public async Task<JsonResult> Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    return Json(new { isError = true, msg = "Please fill the form correctly" });
                }
                var applicationUserViewModel = new ApplicationUserViewModel();
                var user = await _userHelper.FindByEmailAsync(email);
                if (user != null)
                {
                    if (user.EmailConfirmed && !user.IsDeactivated)
                    {
                        var currentUser = _applicationHelper.AuthenticateUser(email, password).Result;
                        if (currentUser != null)
                        {
                            applicationUserViewModel.Roles = (List<string>)await _userManager.GetRolesAsync(currentUser).ConfigureAwait(false);
                            foreach (var role in applicationUserViewModel.Roles)
                            {
                                switch (role)
                                {
                                    case "Admin":
                                        applicationUserViewModel.Role = Session.Constants.AdminRole;
                                        break;
                                    case "Student":
                                        applicationUserViewModel.Role = Session.Constants.StudentRole;
                                        break;
                                    case "SuperAdmin":
                                        applicationUserViewModel.Role = Session.Constants.AdminRole;
                                        break;
                                    case "Applicant":
                                        applicationUserViewModel.Role = Session.Constants.ApplicantRole;
                                        break;
                                }
                            }
                            applicationUserViewModel.Id = currentUser.Id;
                            applicationUserViewModel.Email = currentUser.Email;
                            applicationUserViewModel.FullName = currentUser.FullName;
                            applicationUserViewModel.UserName = currentUser.UserName;
                            applicationUserViewModel.Status = currentUser.Status;
                            HttpContext.Session.SetString("user", JsonConvert.SerializeObject(applicationUserViewModel));
                            var dashboard = Session.GetUserDashboardPage();
                            return Json(new { isError = false, msg = "Welcome!", dashboard = dashboard });
                        }
                    }
                    else
                    {
                        if (!user.EmailConfirmed)
                        {
                            var url = "/Accounts/UnverifiedAccount";
                            return Json(new { isNotVerified = true, msg = "Email Unverifed!!!", url = url });
                        }
                        if (user.IsDeactivated)
                        {
                            var url = "/Accounts/Login";
                            return Json(new { isNotVerified = true, msg = "Account Deactivated!!! Please contact Admin", url = url });
                        }
                    }
                }
                return Json(new { isError = true, msg = "Login Failed" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult LogOut()
        {
            _applicationHelper.LogOut();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult UnverifiedAccount()
        {
            return View();
        }
        public IActionResult EmailVerified(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                Guid userToken = Guid.Parse(token);
                var userVerification = _userHelper.GetUserToken(userToken).Result;
                if (userVerification == null || userVerification.Token == Guid.Empty)
                {
                    return RedirectToAction("Login");
                }
                if (userVerification.User.EmailConfirmed)
                {
                    return View();
                }
                if (userVerification.Used)
                {
                    return View();
                }
                else
                {
                    userVerification.Used = true;
                    userVerification.DateUsed = DateTime.Now;
                    userVerification.User.EmailConfirmed = true;
                    _context.Update(userVerification);
                    _context.Update(userVerification.User);
                    var sendemail = _emailHelper.Gratitude(userVerification.User.Email);
                    _context.SaveChanges();
                    return View();
                }
            }
            return RedirectToAction("Login");
        }
    }
}
