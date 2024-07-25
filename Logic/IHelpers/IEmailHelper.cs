﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
    public interface IEmailHelper
    {
        bool AcceptSuccessfulApplicantMessage(string userEmail);
        bool ChangePasswordAlert(ApplicationUser userDetail, string loginLink);
        Task<UserVerification> CreateUserToken(string userEmail);
        bool DeclineApplicant(string userEmail);
        void ForgotPasswordTemplateEmailer(ApplicationUser userEmail, string linkToClick);
        bool InformApplicantAboutWrittenInterview(string userEmail);
        bool SendMailToApplicant(ApplicationUser userDetail, string linkToClick);
        bool VerificationEmail(string applicantEmail, string linkToClick);
    }
}
