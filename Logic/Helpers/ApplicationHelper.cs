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
   
    public class ApplicationHelper : IApplicationHelper
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserHelper _userHelper;
        private readonly AppDbContext _context;
        private readonly IEmailHelper _emailHelper;
        private bool isPersistent;
        private bool lockoutOnFailure;
        public ApplicationHelper(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserHelper userHelper,
            AppDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userHelper = userHelper;
            _context = context;
        }

        //public async Task<ApplicationUser> RegisterApplicantService(ApplicationUserViewModel applicationUserViewModel)
        //{
        //    if (applicationUserViewModel != null)
        //    {
        //        var newInstanceOfApplicantModelAboutToBCreated = new ApplicationUser
        //        {
        //            FirstName = applicationUserViewModel.FirstName,
        //            LastName = applicationUserViewModel.LastName,
        //            Email = applicationUserViewModel.Email,
        //            PhoneNumber = applicationUserViewModel.PhoneNumber,
        //            UserName = applicationUserViewModel.Email,
        //            Address = applicationUserViewModel.Address,
        //            HasLaptop = applicationUserViewModel.HasLaptop,
        //            HasAnyProgrammingExp = applicationUserViewModel.HasAnyProgrammingExp,
        //            ProgrammingLanguagesExps = applicationUserViewModel.ProgrammingLanguagesExps,
        //            ApplicantResideInEnugu = applicationUserViewModel.ApplicantResideInEnugu,
        //            ReasonForProgramming = applicationUserViewModel.ReasonForProgramming,
        //            HowDoYouIntendToCopeStatement = applicationUserViewModel.HowDoYouIntendToCopeStatement,
        //            Deactivated = false,
        //            IsAdmin = false,
        //            Status = ApplicationStatus.Pending,
        //            DateRegistered = DateTime.Now,
        //        };
        //        var result = await _userManager.CreateAsync(newInstanceOfApplicantModelAboutToBCreated, applicationUserViewModel.Password).ConfigureAwait(false);
        //        if (result.Succeeded)
        //        {
        //            return newInstanceOfApplicantModelAboutToBCreated;
        //        }
        //    }
        //    return null;
        //}
        public async Task<ApplicationUser> RegisterApplicantService(ApplicationUserViewModel applicationUserViewModel)
        {
            if (applicationUserViewModel == null)
            {
                // Log the error
                Console.WriteLine("Application is null.");
                return null;
            }
            var newInstanceOfApplicantModelAboutToBCreated = new ApplicationUser
            {
                FirstName = applicationUserViewModel.FirstName,
                LastName = applicationUserViewModel.LastName,
                Email = applicationUserViewModel.Email,
                PhoneNumber = applicationUserViewModel.PhoneNumber,
                UserName = applicationUserViewModel.Email,
                Address = applicationUserViewModel.Address,
                HasLaptop = applicationUserViewModel.HasLaptop,
                HasAnyProgrammingExp = applicationUserViewModel.HasAnyProgrammingExp,
                ProgrammingLanguagesExps = applicationUserViewModel.ProgrammingLanguagesExps,
                ApplicantResideInEnugu = applicationUserViewModel.ApplicantResideInEnugu,
                ReasonForProgramming = applicationUserViewModel.ReasonForProgramming,
                HowDoYouIntendToCopeStatement = applicationUserViewModel.HowDoYouIntendToCopeStatement,
                IsDeactivated = false,
                IsActivated = true,
                IsAdmin = false,
                Status = ApplicationStatus.Pending,
                DateRegistered = DateTime.Now,
            };
            try
            {
                var result = await _userManager.CreateAsync(newInstanceOfApplicantModelAboutToBCreated, applicationUserViewModel.Password).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    return newInstanceOfApplicantModelAboutToBCreated;
                }
                else
                {
                    // Log detailed errors
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error Code: {error.Code}, Description: {error.Description}");
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                // Log detailed DbUpdateException errors
                Console.WriteLine($"DbUpdateException: {dbEx.InnerException?.Message ?? dbEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log other exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.InnerException?.Message ?? ex.Message}");
                throw;
            }
            return null;
        }

        public async Task<ApplicationUser> RegisterAdminService(ApplicationUserViewModel applicationUserViewModel)
        {
            if (applicationUserViewModel != null)
            {
                var newInstanceOfApplicantModelAboutToBCreated = new ApplicationUser
                {
                    FirstName = applicationUserViewModel.FirstName,
                    LastName = applicationUserViewModel.LastName,
                    Email = applicationUserViewModel.Email,
                    UserName = applicationUserViewModel.Email,
                    PhoneNumber = applicationUserViewModel.PhoneNumber,
                    Address = applicationUserViewModel.Address,
                    HowDoYouIntendToCopeStatement = "Default coping statement",
                    Role = "Admin",
                    IsDeactivated = false,
                    IsAdmin = true,
                    DateRegistered = DateTime.Now
                };

                try
                {
                    var result = await _userManager.CreateAsync(newInstanceOfApplicantModelAboutToBCreated, applicationUserViewModel.Password);
                    if (result.Succeeded)
                    {
                        return newInstanceOfApplicantModelAboutToBCreated;
                    }
                    else
                    {
                        // Log the identity errors
                        foreach (var error in result.Errors)
                        {
                            // You can use a logger here or throw a detailed exception
                            Console.WriteLine($"Error: {error.Description}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log or rethrow the inner exception for more details
                    Console.WriteLine($"Exception: {ex.InnerException?.Message ?? ex.Message}");
                    throw; // Re-throw the exception if necessary
                }
            }
            return null;
        }


        public string GetUserDashboardPage(ApplicationUser userr)
        {
            var userRole = _userManager.GetRolesAsync(userr).Result.FirstOrDefault();
            if (userRole != null)
            {
                if (userRole == "SuperAdmin" || userRole == "Admin")
                {
                    return "/Admin/Index";

                }
                else
                {
                    return "/Student/Index";
                }
            }
            return null;
        }

        public async Task<ApplicationUser> AuthenticateUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && user.IsDeactivated != true)
            {
                var logger = _signInManager.PasswordSignInAsync(user.UserName, password, isPersistent = false, lockoutOnFailure = false).Result;
                if (logger.Succeeded)
                {
                    return user;
                }
            }
            return null;
        }
        public async Task<bool> LogOut()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        public string GetUserLayout(string username)
        {
            var accountType = _userHelper.FindByUserNameAsync(username).Result;
            var userRole = _userManager.GetRolesAsync(accountType).Result.FirstOrDefault();
            if (userRole != null)
            {
                if (userRole == "SuperAdmin" || userRole == "Admin")
                {
                    return "~/Views/Shared/_AdminLayout.cshtml";
                }
                else
                {
                    return "~/Views/Shared/_StudentLayout.cshtml";
                }
            }
            return null;
        }

        //APPLICATION ACCEPT POST SERVICE
        public ApplicationUser AcceptSelectedApplication(ApplicationUser applicantIdToAccept)
        {
            if (applicantIdToAccept != null)
            {
                var applicantsDetails = _userManager.FindByIdAsync(applicantIdToAccept.Id).Result;

                applicantsDetails.Status = ApplicationStatus.Accepted;

                _context.ApplicationUsers.Update(applicantsDetails);
                _context.SaveChanges();

                _emailHelper.AcceptSuccessfulApplicantMessage(applicantsDetails.Email);
                return applicantsDetails;
            }
            else
            {
                return null;
            }
        }

        //APPLICATION REJECT POST SERVICE
        public ApplicationUser RejectSelectedApplication(ApplicationUser applicantIdToReject)
        {
            if (applicantIdToReject != null)
            {
                var applicantsDetails = _userManager.FindByIdAsync(applicantIdToReject.Id).Result;

                applicantsDetails.Status = ApplicationStatus.Rejected;

                _context.ApplicationUsers.Update(applicantsDetails);
                _context.SaveChanges();

                _emailHelper.DeclineApplicant(applicantsDetails.Email);
                return applicantsDetails;
            }
            else
            {
                return null;
            }
        }

        //APPLICANT INTERVIEW QUALIFICATION POST SERVICE
        public ApplicationUser QualifiedToTakeWrritingInterview(ApplicationUser applicantsDetails)
        {
            if (applicantsDetails != null)
            {
                applicantsDetails.Status = ApplicationStatus.TakeInterview;

                _context.ApplicationUsers.Update(applicantsDetails);
                _context.SaveChanges();

                _emailHelper.InformApplicantAboutWrittenInterview(applicantsDetails.Email);
                return applicantsDetails;
            }
            else
            {
                return null;
            }
        }
    }
}
