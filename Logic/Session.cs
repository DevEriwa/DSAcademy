
using Core.ViewModels;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

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
    public static ApplicationUserViewModel GetCurrentUser()
    {
        var user = AppHttpContext.Current.Session.GetString("user");
        if (user != null)
        {
            var loggedInUser = JsonConvert.DeserializeObject<ApplicationUserViewModel>(user);
            return loggedInUser;
        }
        return null;
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
    public static string GetUserDashboardPage()
    {
        var user = GetCurrentUser();
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
        return null;
    }

    public static class Constants
    {
        public static string SuperAdminRole = "SuperAdmin";
        public static string AdminRole = "Admin";
        public static string StudentRole = "Student";
        public static string ApplicantRole = "Applicant";
        public static string DefaultLayout = "~/Views/Shared/_Layout.cshtml";
        public static string AdminLayout = "~/Views/Shared/_AdminLayout.cshtml";
        public static string StudentsLayout = "~/Views/Shared/_StudentLayout.cshtml";
    }
}