using Core.Models;
using Core.ViewModels;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;

namespace Logic
{
    public static class AppHttpContext
    {
        static IServiceProvider services = null;

        /// <summary>
        /// Provides static access to the framework's services provider
        /// </summary>
        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }
                services = value;
            }
        }

        /// <summary>
        /// Provides static access to the current HttpContext
        /// </summary>
        public static HttpContext Current
        {
            get
            {
                IHttpContextAccessor httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
                return httpContextAccessor?.HttpContext;
            }
        }

        public class SessionTimeoutAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpContext ctx = AppHttpContext.Current;
                if (AppHttpContext.Current.Session.GetString("user") == null)
                {
                    filterContext.Result = new RedirectResult("~/Accounts/Login");
                    return;
                }
                base.OnActionExecuting(filterContext);
            }
        }
    }
}
public class Session
{
    public static ApplicationUser GetCurrentUser()
    {
        var user = AppHttpContext.Current.Session.GetString("user");
        if (user != null)
        {
            var loggedInUser = JsonConvert.DeserializeObject<ApplicationUser>(user);
            return loggedInUser;
        }
        return null;
    }
	public static Notification GetSystemSetting()
	{
		var settings = new Notification();
		var settingsString = AppHttpContext.Current?.Session?.GetString("DSASystemSettings");
		if (settingsString != null)
		{
			settings = JsonConvert.DeserializeObject<Notification>(settingsString);
			return settings;
		}
		return settings;
	}

	public static CompanySetting GetCompanySettings()
	{
		var companySettings = new CompanySetting();

		var result = AppHttpContext.Current.Session.GetString("companySettings");
		if (result != null)
		{
			companySettings = JsonConvert.DeserializeObject<CompanySetting>(result);
			if (companySettings != null)
			{
				return companySettings;
			}
		}
		return companySettings;
	}

	public static string GetRoleLayout()
    {
        var loggedInUser = GetCurrentUser();
        if (loggedInUser != null)
        {
            var superAdmin = loggedInUser.Roles.Contains(Constants.SuperAdminRole);
            if (superAdmin)
            {
                return Constants.AdminLayout;
            }
            else if (loggedInUser.Roles.Contains(Constants.AdminRole))
            {
                return Constants.AdminLayout;
            }
            else
            {
                var isStudent = loggedInUser.Roles.Contains(Constants.StudentRole);
                if (isStudent)
                {
                    return Constants.StudentsLayout;
                }
                else
                {
                    var isUser = loggedInUser.Roles.Contains(Constants.ApplicantRole);
                    if (isUser)
                    {
                        return Constants.StudentsLayout;
                    }
                }
            }
        }
        return Constants.DefaultLayout;
    }
    public static string GetUserDashboardPage(bool isProgram)
    {
        var user = GetCurrentUser();
        if (isProgram == false)
        {
			var userRole = user.Role;
			if (userRole != null)
			{
				if (userRole == "Student" || userRole == "Applicant")
				{
					return "/Accounts/Program";
				}
			}
		}else if (isProgram == true)
        {
			var userRole = user.Role;
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
		}
        return null;
    }

    public static class Constants
    {
        public static string SuperAdminRole = "SuperAdmin";
        public static string AdminRole = "Admin";
        public static string StudentRole = "Student";
        public static string ApplicantRole = "Applicant";
        public static string DefaultLayout = "~/Views/Shared/_Layout.cshtml";
        public static string AdminLayout = "~/Views/Shared/_CompanyAdminLayout.cshtml";
        public static string StudentsLayout = "~/Views/Shared/_StudentLayout.cshtml";
        public static string ProgramLayout = "~/Views/Shared/_ProgramLayout.cshtml";
		public static string SuperAdminLayout = "~/Views/Shared/_SuperAdminLayout.cshtml";
	}

	public bool CheckAdminIsLogin()
	{
		var result = AppHttpContext.Current.Session.GetString("isImpersonating");
		if (result == null)
		{
			return false;
		}
		return true;
	}

	public static class MessageConstants
	{
		public static string SendAppointmentEmailToClient = "Hi ${customerName}, <br/>" +
			"Your appointment has been booked for ${appointmentDate} at ${messageSenderName}.<br/>" +
			"Thanks,<br/> ${companyName}.<br/>";

		public static string SendReminderForUpcomingMOT = "Hello ${customerName}, your MOT expires in ${dueDate}." + "\n" +
			" To book your MOT test please call us on ${companyPhoneNumber}. " + "\n" +
			"Thank you!  ${companyName}";

		public static string VHC = "Hi, please see your vehicle health check online using the link below ${link}" + "\n" +
									"Thanks!  ${companyName}";
	}
}