using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
	public interface INotificationHelper
	{
		Task<CustomNotificationViewModel> GetNotificationSettings(Guid? branchId);
		CustomNotificationViewModel GetCompanyCustomSettings(Guid companyBranchId, CustomNotificationViewModel customNotificationViewModel, List<string> checkedAdminSettings, List<string> uncheckedAdminSettings);

		// ─── In-App Notifications ─────────────────────────────────────────────
		Task SendAsync(string userId, Guid? companyId, string title, string message, string icon = "bi bi-bell", string? actionUrl = null);
		Task SendToCompanyAsync(Guid companyId, string title, string message, string icon = "bi bi-bell", string? actionUrl = null);
		List<AppNotification> GetUnread(string userId);
		int GetUnreadCount(string userId);
		Task MarkAllReadAsync(string userId);
		Task MarkReadAsync(int notificationId);
	}
}
