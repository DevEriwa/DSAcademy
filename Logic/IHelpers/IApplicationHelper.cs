using Core.Models;
using Core.ViewModels;

namespace Logic.IHelpers
{
    public interface IApplicationHelper
    {
        Task<ApplicationUser> RegisterApplicantService(ApplicationUserViewModel applicationUserViewModel, Guid? companyId);
        Task<ApplicationUser> RegisterAdminService(ApplicationUserViewModel applicationUserViewModel);
        Task<ApplicationUser> AuthenticateUser(string email, string password);
        string GetUserDashboardPage(ApplicationUser userr);
        Task<bool> LogOut();
        string GetUserLayout(string username);
        ApplicationUser AcceptSelectedApplication(ApplicationUser applicantIdToAccept);
        ApplicationUser RejectSelectedApplication(ApplicationUser applicantIdToReject);
        ApplicationUser QualifiedToTakeWrritingInterview(ApplicationUser applicantIdToAccept);
		Payments SaveProveOfPayment(PaymentViewModel collectedData, string base64, ApplicationUser user);
        Task<bool> CreateCompany(ApplicationUserViewModel companyViewModel, string base64);
	}
}
