using Core.Db;
using Core.Enum;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helpers
{
	public class NotificationHelper : INotificationHelper
	{

		private readonly AppDbContext _context;
		public int checker = 0;

		public NotificationHelper(
			AppDbContext context)
		{
			_context = context;

		}
		public async Task<CustomNotificationViewModel> GetNotificationSettings(Guid? branchId)
		{
			var settings = new CustomNotificationViewModel();

			var existingSettings = _context.Notifications
				.FirstOrDefault(c => c.CompanyId == branchId);
			if (existingSettings == null)
			{
				var notificationSetting = new Notification()
				{
					CompanyId = branchId,
					PageSizeData = 25,
					CompanyWorkHourPerDay = 16,
					CompanyInvoiceTerms = "",
					
					IsReceiptWaterMark = false,
					PriceMarkUp = 40,
					IsPriceMarkUp = false,
					
					ReceiptColour = "#366092",
					ReceiptSubColour = "#DCE6F1",
					
				};
				_context.Add(notificationSetting);
				_context.SaveChanges();

				//instantiate the viewmodel for the view
				settings.CompanyId = (Guid)branchId;
				settings.SetPageSize = notificationSetting.PageSizeData;
				settings.CompanyWorkHourPerDay = notificationSetting.CompanyWorkHourPerDay;
				settings.CompanyInvoiceTerms = notificationSetting.CompanyInvoiceTerms;
				settings.IsReceiptWaterMark = notificationSetting.IsReceiptWaterMark;
				settings.PriceMarkUp = notificationSetting.PriceMarkUp;
				settings.IsPriceMarkUp = notificationSetting.IsPriceMarkUp;
				settings.QRDescription = notificationSetting.QRScanDetails;
				settings.QRLink = notificationSetting.QRScanLink;
				settings.FacebookAccessToken = notificationSetting.FacebookAccessToken;
				settings.FacebookAppId = notificationSetting.FacebookAppId;
				settings.FacebookSecretKey = notificationSetting.FacebookSecretKey;
				settings.GoogleMapId = notificationSetting.GoogleMapId;
				settings.FacebookAppId = notificationSetting.FacebookAppId;
				settings.FacebookSecretKey = notificationSetting.FacebookSecretKey;
				settings.HourlyRate = notificationSetting.HourlyRate;
				settings.ReceiptColour = notificationSetting.ReceiptColour;
				settings.ReceiptSubColour = notificationSetting.ReceiptSubColour;
				settings.LunchStartTime = notificationSetting.LunchStartTime;
				settings.LunchEndTime = notificationSetting.LunchEndTime;
				settings.BusinessStartTime = notificationSetting.BusinessStartTime;
				settings.BusinessEndTime = notificationSetting.BusinessEndTime;
				
				return settings;
			}
			else
			{
				settings.CompanyId = (Guid)branchId;
				//settings.CompanyBranchName = branch.Name;
				settings.SetPageSize = existingSettings.PageSizeData;
				settings.CompanyWorkHourPerDay = existingSettings.CompanyWorkHourPerDay;
				settings.CompanyInvoiceTerms = existingSettings.CompanyInvoiceTerms;
				settings.IsReceiptWaterMark = existingSettings.IsReceiptWaterMark;
				settings.PriceMarkUp = existingSettings.PriceMarkUp;
				settings.IsPriceMarkUp = existingSettings.IsPriceMarkUp;
				settings.QRDescription = existingSettings.QRScanDetails;
				settings.QRLink = existingSettings.QRScanLink;
				settings.FacebookAccessToken = existingSettings.FacebookAccessToken;
				settings.FacebookSecretKey = existingSettings.FacebookSecretKey;
				settings.FacebookAppId = existingSettings.FacebookAppId;
				settings.GoogleMapId = existingSettings.GoogleMapId;
				settings.FacebookAppId = existingSettings.FacebookAppId;
				settings.FacebookSecretKey = existingSettings.FacebookSecretKey;
				settings.HourlyRate = existingSettings.HourlyRate;
				settings.ReceiptColour = string.IsNullOrEmpty(existingSettings.ReceiptColour) ? "#366092" : existingSettings.ReceiptColour;
				settings.ReceiptSubColour = string.IsNullOrEmpty(existingSettings.ReceiptSubColour) ? "#DCE6F1" : existingSettings.ReceiptSubColour;
				settings.LunchStartTime = existingSettings.LunchStartTime == TimeSpan.Parse("00:00:00") ? TimeSpan.Parse("12:00:00") : existingSettings.LunchStartTime;
				settings.LunchEndTime = existingSettings.LunchEndTime == TimeSpan.Parse("00:00:00") ? TimeSpan.Parse("13:00:00") : existingSettings.LunchEndTime;
				settings.BusinessStartTime = existingSettings.BusinessStartTime == TimeSpan.Parse("00:00:00") ? TimeSpan.Parse("09:00:00") : existingSettings.BusinessStartTime;
				settings.BusinessEndTime = existingSettings.BusinessEndTime == TimeSpan.Parse("00:00:00") ? TimeSpan.Parse("17:00:00") : existingSettings.BusinessEndTime;
			}
			return settings;
		}
		public async Task<Company> GetCompanyBranchById(Guid? companyBranchId)
		{
			var company = await _context.Companies.Where(x => x.Id == companyBranchId && x.Active).FirstOrDefaultAsync();
			if (company != null)
			{
				return company;
			}
			return null;
		}
		public CustomNotificationViewModel GetCompanyCustomSettings(Guid companyId, CustomNotificationViewModel customNotificationViewModel, List<string> checkedAdminSettings, List<string> uncheckedAdminSettings)
		{
			var setting = new CustomNotificationViewModel();
			if (companyId != Guid.Empty)
			{
				var notificationSettings = _context.Notifications.Include(v => v.Company)
					.FirstOrDefault(c => c.CompanyId == companyId);
				foreach (var item in checkedAdminSettings)
				{
					
					if (CompanySettings.IsReceiptWaterMark.ToString().ToLower() == item.ToLower())
						notificationSettings.IsReceiptWaterMark = true;
					if (CompanySettings.IsPriceMarkUp.ToString().ToLower() == item.ToLower())
						notificationSettings.IsPriceMarkUp = true;
				}
				foreach (var comp in uncheckedAdminSettings)
				{
					
					if (CompanySettings.IsReceiptWaterMark.ToString().ToLower() == comp.ToLower())
						notificationSettings.IsReceiptWaterMark = false;
					if (CompanySettings.IsPriceMarkUp.ToString().ToLower() == comp.ToLower())
						notificationSettings.IsPriceMarkUp = false;
					
				}
				if (notificationSettings != null)
				{
					notificationSettings.PageSizeData = customNotificationViewModel.SetPageSize;
					notificationSettings.CompanyWorkHourPerDay = customNotificationViewModel.CompanyWorkHourPerDay;
					notificationSettings.CompanyInvoiceTerms = customNotificationViewModel.CompanyInvoiceTerms;
					
					notificationSettings.PriceMarkUp = customNotificationViewModel.PriceMarkUp;
					notificationSettings.QRScanLink = customNotificationViewModel.QRLink;
					notificationSettings.QRScanDetails = customNotificationViewModel.QRDescription;
					notificationSettings.FacebookAppId = customNotificationViewModel.FacebookAppId;
					notificationSettings.FacebookSecretKey = customNotificationViewModel.FacebookSecretKey;
					notificationSettings.FacebookAccessToken = customNotificationViewModel.FacebookAccessToken;
					notificationSettings.HourlyRate = customNotificationViewModel.HourlyRate;
					notificationSettings.ReceiptColour = customNotificationViewModel.ReceiptColour;
					notificationSettings.ReceiptSubColour = customNotificationViewModel.ReceiptSubColour;
					notificationSettings.LunchStartTime = customNotificationViewModel.LunchStartTime;
					notificationSettings.LunchEndTime = customNotificationViewModel.LunchEndTime;
					notificationSettings.BusinessStartTime = customNotificationViewModel.BusinessStartTime;
					notificationSettings.BusinessEndTime = customNotificationViewModel.BusinessEndTime;
					_context.Update(notificationSettings);
					_context.SaveChanges();

					var settings = new CustomNotificationViewModel()
					{
						CompanyId = notificationSettings.CompanyId.Value,
						CompanyName = notificationSettings.Company.Name,
						SetPageSize = notificationSettings.PageSizeData,
						CompanyWorkHourPerDay = notificationSettings.CompanyWorkHourPerDay,
						CompanyInvoiceTerms = notificationSettings.CompanyInvoiceTerms,
						
						IsReceiptWaterMark = notificationSettings.IsReceiptWaterMark,
						PriceMarkUp = notificationSettings.PriceMarkUp,
						IsPriceMarkUp = notificationSettings.IsPriceMarkUp,
						FacebookAccessToken = notificationSettings.FacebookAccessToken,
						FacebookSecretKey = notificationSettings.FacebookSecretKey,
						
						HourlyRate = notificationSettings.HourlyRate,
						ReceiptColour = notificationSettings.ReceiptColour,
						ReceiptSubColour = notificationSettings.ReceiptSubColour,
						
						LunchStartTime = notificationSettings.LunchStartTime,
						LunchEndTime = notificationSettings.LunchEndTime,
						BusinessStartTime = notificationSettings.BusinessStartTime,
						BusinessEndTime = notificationSettings.BusinessEndTime,
					};
					return settings;
				}
			}
			return setting;
		}

		// ─── In-App Notification Methods ─────────────────────────────────────

		public async Task SendAsync(string userId, Guid? companyId, string title, string message,
			string icon = "bi bi-bell", string? actionUrl = null)
		{
			var n = new AppNotification
			{
				UserId = userId,
				CompanyId = companyId,
				Title = title,
				Message = message,
				Icon = icon,
				ActionUrl = actionUrl,
				DateCreated = DateTime.Now
			};
			_context.AppNotifications.Add(n);
			await _context.SaveChangesAsync();
		}

		public async Task SendToCompanyAsync(Guid companyId, string title, string message,
			string icon = "bi bi-bell", string? actionUrl = null)
		{
			var users = _context.ApplicationUsers
				.Where(u => u.CompanyId == companyId && !u.IsDeactivated)
				.Select(u => u.Id)
				.ToList();

			var notifications = users.Select(uid => new AppNotification
			{
				UserId = uid,
				CompanyId = companyId,
				Title = title,
				Message = message,
				Icon = icon,
				ActionUrl = actionUrl,
				DateCreated = DateTime.Now
			}).ToList();

			_context.AppNotifications.AddRange(notifications);
			await _context.SaveChangesAsync();
		}

		public List<AppNotification> GetUnread(string userId)
		{
			return _context.AppNotifications
				.Where(n => n.UserId == userId && !n.IsRead)
				.OrderByDescending(n => n.DateCreated)
				.Take(15)
				.ToList();
		}

		public int GetUnreadCount(string userId)
			=> _context.AppNotifications.Count(n => n.UserId == userId && !n.IsRead);

		public async Task MarkAllReadAsync(string userId)
		{
			var unread = _context.AppNotifications
				.Where(n => n.UserId == userId && !n.IsRead).ToList();
			unread.ForEach(n => n.IsRead = true);
			await _context.SaveChangesAsync();
		}

		public async Task MarkReadAsync(int notificationId)
		{
			var n = _context.AppNotifications.FirstOrDefault(x => x.Id == notificationId);
			if (n != null) { n.IsRead = true; await _context.SaveChangesAsync(); }
		}
	}
}
