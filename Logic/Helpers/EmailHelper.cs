using Core.Config;
using Core.Db;
using Core.Models;
using Logic.IHelpers;
using Logic.SmtpMailServices;

namespace Logic.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        private readonly IUserHelper _userHelper;
        private readonly IEmailService _emailService;
        private readonly AppDbContext _context;
        private readonly IGeneralConfiguration _generalConfiguration;
        public EmailHelper(
            IUserHelper userHelper,
            IEmailService emailService,
            AppDbContext context,
            IGeneralConfiguration generalConfiguration)
        {
            _userHelper = userHelper;
            _emailService = emailService;
            _context = context;
            _generalConfiguration = generalConfiguration;
        }

        public bool SendMailToApplicant(ApplicationUser userDetail, string linkToClick)
        {
            string toEmail = userDetail.Email;
            string subject = "Registration Completed Successfully";
            string message = "Dear " + userDetail.FirstName + ", You have successfully completed the registration in our platform" + " Please click on the link below to choose your interview date" + "<br>" +
                   "<a  href='" + linkToClick + "?userId=" + userDetail.Id + "' target='_blank'>" + "<button style='color:white; background-color:#0C2B4B; padding:10px; border:10px;'>Create Now</button>" + "</a>";
            _emailService.SendEmail(toEmail, subject, message);
            return true;
        }

        public bool VerificationEmail(string applicantEmail, string linkToClick)
        {
            try
            {
                if (applicantEmail != null)
                {
                    string toEmail = applicantEmail;
                    string subject = "Verify your account - Digital Skills Academy ";
                    string message = "Thank you for registering! You’re only one click away from accessing your Digital skills academy account." + "<br/>" + "<br/>" +
                        "<a  href='" + linkToClick + "' target='_blank'>" + "<button style='color:white; background-color:#0C2B4B; padding:10px; border:10px;'>Verify Me Now</button>" + "</a>" + "<br/>" +
                        "If the link does not work copy the link here to your browser: " + linkToClick + "<br/>" +
                        "Need help? We’re here for you.Simply reply to this email to contact us. <br/>" +

                        "Kind regards,<br/>" +
                        "Digital Skills Academy Support.";
                    _emailService.SendEmail(toEmail, subject, message);
                    return true;
                };
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Gratitude(string applicantEmail)
        {
            try
            {
                if (applicantEmail != null)
                {
                    string toEmail = applicantEmail;
                    string subject = "Email Verified Successfully";
                    string message = "Your Email has been verified.Thank you for applying with Digital Skills Academy.<br/> " +
                        "<br/>" + "<br/>" + "Need help? We’re always here for you.Simply reply to this email to reach us. <br/>" + "<br/>" +
                    "Kind regards,<br/>" +
                    "<b>Digital Skills Academy.</b>";
                    _emailService.SendEmail(toEmail, subject, message);
                    return true;
                };
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async void ForgotPasswordTemplateEmailer(ApplicationUser userEmail, string linkToClick)
        {
            var getUserId = await _userHelper.FindByUserNameAsync(userEmail.FirstName);
            if (userEmail != null)
            {
                string toEmail = userEmail.Email;
                string subject = "Reset your Email - Digital Skills Academy ";
                string message = "Hi!" + " " + userEmail.FirstName + "," + "<br/>" + "<br/>" +
                    "You requested for a password reset. please click on the link below to create a new password," + "<br/>" +
                    "<a  href='" + linkToClick + "' target='_blank'>" + "<button style='color:white; background-color:#0C2B4B; padding:10px; border:10px;'>Password Reset</button>" + "</a>" + "<br/>" +
                    "If the link does not work copy the link here to your browser" + "<br/>" +
                    "<a  href='" + linkToClick + "' target='_blank'>" + linkToClick + "</a>" + "<br/>" +
                    "Need help? We’re here for you.Simply reply to this email to contact us. <br/>" +

                    "Kind regards,<br/>" +
                    "Digital Skills Academy.";
                _emailService.SendEmail(toEmail, subject, message);

            }

        }

        public async Task PasswordResetedTemplateEmailerAsync(ApplicationUser userEmail, string linkToClick)
        {
            var getUserId = await _userHelper.FindByUserNameAsync(userEmail.FirstName);
            if (userEmail != null)
            {
                string toEmail = userEmail.Email;
                string subject = "Reset your Email - Digital Skills Academy ";
                string message = "Hi!" + userEmail.FirstName + "," + "<br/>" + "<br/>" +
                "You requested for a password reset." + "Your password has been changed.If you did not make this change please email us at dskillacademy1@gmail.com<br> Regards" + " < br/>" +
                    "<a  href='" + linkToClick + "' target='_blank'>" + "<button style='color:white; background-color:#0C2B4B; padding:10px; border:10px;'>Verify Me Now</button>" + "</a>" + "<br/>" +
                   "If you have any trouble with your account, you can always email us at hello@bivisoft.com," + "<br/>" +
                    "Need help? We’re here for you.Simply reply to this email to contact us. <br/>" +

                    "Kind regards,<br/>" +
                    "Digital Skills Academy.";
                _emailService.SendEmail(toEmail, subject, message);

            }
        }

        public bool ChangePasswordAlert(ApplicationUser userDetail, string loginLink)
        {
            string toEmail = userDetail.Email;
            string subject = "PASSWORD CHANGE ALERT";
            string message = "Dear " + userDetail.FirstName + "," + "<br>" + "<br>" + "Your password has been changed successfully. If you did not make this change please email us at dskillacademy1@gmail.com" + "<br>" + "<br>" + "Regards";
            _emailService.SendEmail(toEmail, subject, message);

            ChangePasswordMailTemlate(userDetail, loginLink);
            return true;
        }

        public bool ChangePasswordMailTemlate(ApplicationUser userDetail, string loginLink)
        {
            string toEmail = userDetail.Email;
            string subject = "PASSWORD CHANGED SUCCESSFULLY ";
            string message = "Hi " + userDetail.FirstName + ", " + "<br>" + "<br>" + "You have successfully changed your password. please click on the link below to login with your new password" + "<br>" + "<br>" + "Regards" + "<br>" + "<br>" +
                   "<a  href='" + loginLink + "' target='_blank'>" + "<button style='color:white; background-color:#0C2B4B; padding:10px; border:10px;'>Login</button>" + "</a>";
            _emailService.SendEmail(toEmail, subject, message);
            return true;
        }
        //public bool ApprovedProjectTopicMailTemlate(ApplicationUser userDetail, ProjectTopic topicOwer)
        //{ 
        //    string toEmail = userDetail.Email;
        //    string subject = "PROJECT TOPIC APPROVAL";
        //    string message = "Dear " + userDetail.FirstName + ", " + "<br>" + "<br>" + "<b>" + topicOwer.Title + "</b>" + " has been approved for your project. Make Sure you Update your project to GitHub." + "<br>" + "<br>" + "Good luck!!!" + "<br>" + "<br>";
        //    _emailService.SendEmail(toEmail, subject, message);
        //    return true;
        //}
        public bool SendPaymentAprovalMsg(ApplicationUser userDetail, string course)
        {
            try
            {
                if (userDetail != null)
                {
                    string toEmail = userDetail.Email;
                    string subject = "PAYMENT APPROVED";
                    string message = "Dear " + userDetail.FirstName + "<br/>" + "<br/>" + "The transaction you made for " + course + " course was successful, You can access the course.<br/> " +
                        "<br/>" + "Good luck!!! <br/>";
                    _emailService.SendEmail(toEmail, subject, message);
                    return true;
                };

                return false;
            }

            catch (Exception)
            {
                throw;
            }

        }

        public bool SendPaymentDeclineMsg(ApplicationUser userDetail, string course)
        {
            try
            {
                if (userDetail != null)
                {
                    string toEmail = userDetail.Email;
                    string subject = "PAYMENT DECLINED";
                    string message = "Dear " + userDetail.FirstName + "<br/>" + "<br/>" + "The transaction you made for " + course + " course wasn't successful, Check your transaction history to confirm. for any clarification reach Out to us at academy@bivisoft.com<br/> " +
                        "<br/>" + " Regards <br/>";
                    _emailService.SendEmail(toEmail, subject, message);
                    return true;
                };

                return false;
            }

            catch (Exception)
            {
                throw;
            }

        }

        public async Task<UserVerification> CreateUserToken(string userEmail)
        {
            try
            {
                var user = await _userHelper.FindByEmailAsync(userEmail);
                if (user != null)
                {
                    UserVerification userVerification = new UserVerification()
                    {
                        UserId = user.Id,
						DateUsed = DateTime.Now,
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
            catch (Exception)
            {
                throw;
            }

        }

        public bool SendMailToStudentsOnProjectCompletion(string emailAddressTo)
        {
            try
            {
                if (emailAddressTo != null)
                {
                    string toEmail = emailAddressTo;
                    string subject = "CONGRATULATION";
                    string message = "Congratulations, your project has been accepted you can now apply for a job." + "<br/> " + "<br/> " +
                    "Kind regards,<br/>" +
                    "<b>Bivisoft Limited.</b>";
                    _emailService.SendEmail(toEmail, subject, message);
                    return true;
                };

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public bool SendMailToNewEmployee(string reference)
        //{
        //    try
        //    {
        //        var myPlanDetails = _context.Payments.Where(x => x.InvoiceNumber == reference).Include(c => c.User).FirstOrDefault();
        //        if (myPlanDetails != null)
        //        {
        //            string toEmail = myPlanDetails.User.Email;
        //            string subject = "25% MONTHLY DEDUCTION FORM SALARY";
        //            string message = "Congratulations on your new job." + "<br/> " +
        //                "As agreed, 25% of your salary will be funded to company's accounts for the next 24 months. click on the button below to activate." + "<br/> " +
        //                "<a  href='" + myPlanDetails.authorization_url + "' target='_blank'>" + "<button style='color:white; background-color:#0C2B4B; padding:10px; border:10px;'>Activate Now</button>" + "</a>" + "<br/> " + "<br/>" +
        //                "If the link does not work, copy the link here to your browser: " + myPlanDetails.authorization_url + "<br/>" +
        //                "Need help? We’re here for you.Simply reply to this email to contact us. <br/>" +
        //            "Kind regards,<br/>" +
        //            "<b>Bivisoft Limited.</b>";
        //            _emailService.SendEmail(toEmail, subject, message);
        //            return true;
        //        }

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public bool InformApplicantAboutWrittenInterview(string userEmail)
        {
            try
            {
                if (userEmail != null)
                {
                    if (userEmail != null)
                    {
                        string toEmail = userEmail;
                        string subject = "INVITATION FOR TECHNICAL TEXT";
                        string message = "Greeting!" + "<br/> " +
                            "With respect to your application with Bivisoft Academy, you have been selected to participate in a written interview. Kindly login into your account to take this exam" + "<br/> " +
                            "Need help? We’re here for you.Simply reply to this email to contact us. <br/>" +
                        "Kind regards,<br/>" +
                        "<b>Bivisoft Limited.</b>";
                        _emailService.SendEmail(toEmail, subject, message);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeclineApplicant(string userEmail)
        {
            try
            {
                if (userEmail != null)
                {
                    if (userEmail != null)
                    {
                        string toEmail = userEmail;
                        string subject = "For Your Information";
                        string message = "Greeting!" + "<br/> " +
                            "With respect to your application with Bivisoft Academy, we are sorry to inform you that after proper review on your submission that you are not qualify for this program." + "<br/> " +
                        "Kind regards,<br/>" +
                        "<b>Bivisoft Limited.</b>";
                        _emailService.SendEmail(toEmail, subject, message);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool AcceptSuccessfulApplicantMessage(string userEmail)
        {
            try
            {
                if (userEmail != null)
                {
                    if (userEmail != null)
                    {
                        string toEmail = userEmail;
                        string subject = "CONGRATULATION!!!";
                        string message = "Dear Applicant," + "<br/> " +
                            "With respect to your application with Bivisoft Academy and your performance in the written interview, we are glad to inform you that you made it and you are qualify to start this program. kindly login into you dashboard for other documentations" + "<br/> " +
                        "Kind regards,<br/>" +
                        "<b>Bivisoft Limited.</b>";
                        _emailService.SendEmail(toEmail, subject, message);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		public bool SendEmailToStudentAndCompanyForProofOfPayment(string studentEmail, string companyEmail, string companyName)
		{
			try
			{
				if (!string.IsNullOrEmpty(studentEmail) && !string.IsNullOrEmpty(companyEmail))
				{
					// Email content for the student
					string studentSubject = "Payment Proof Received";
					string studentMessage = $"Dear Applicant,<br/><br/>" +
						$"We have successfully received your proof of payment for the Digital Skills Academy program. Our team is currently reviewing your submission, and you will receive an email from {companyName} shortly with further instructions.<br/><br/>" +
						"Thank you for your prompt action.<br/><br/>" +
						$"Kind regards,<br/>" +
						$"<b>{companyName}</b>";


					// Email content for the company
					string companySubject = "New Payment Notification";
					string companyMessage = $"Dear {companyName},<br/><br/>" +
						$"A new payment has been successfully processed by {studentEmail}. Please review the transaction and proceed with the necessary steps.<br/><br/>" +
						"Best regards,<br/>" +
						"<b>Your Automated System</b>";

					// Send email to the student
					_emailService.SendEmail(studentEmail, studentSubject, studentMessage);

					// Send email to the company
					_emailService.SendEmail(companyEmail, companySubject, companyMessage);

					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				// Consider logging the exception here instead of rethrowing it
				throw;
			}
		}
		public bool TeacherVerificationEmail(string applicantEmail, string linkToClick)
		{
			try
			{
				if (applicantEmail != null)
				{
					string toEmail = applicantEmail;
					string subject = "Verify your account - Digital Skills Academy ";
					string message = "Thank you for registering! You’re only one click away from accessing your Digital skills academy account." + "<br/>" + "<br/>" +
						"<a  href='" + linkToClick + "' target='_blank'>" + "<button style='color:white; background-color:#0C2B4B; padding:10px; border:10px;'>Verify Me Now</button>" + "</a>" + "<br/>" +
						"If the link does not work copy the link here to your browser: " + linkToClick + "<br/>" + 
                        "User this passwold to login to your account and do not share this with anyone passwold = (1111111)" + "</br>" +
						"Need help? We’re here for you.Simply reply to this email to contact us. <br/>" +

						"Kind regards,<br/>" +
						"Digital Skills Academy Support.";
					_emailService.SendEmail(toEmail, subject, message);
					return true;
				};
				return false;
			}
			catch (Exception)
			{
				throw;
			}
		}

	}
}
