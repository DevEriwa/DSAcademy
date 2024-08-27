using Core.Models;
using Hangfire.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using RedisCache;
using Serilog;
using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase;

namespace DSAcademy.Controllers
{
	public abstract class BaseController : Controller
	{
		public BaseController() { }

		private ApplicationUser _user;
		private Notification _notification;
		private CompanySetting _companySetting;

		public ApplicationUser? CurrentUser
		{
			get
			{
				_user = Session.GetCurrentUser();
				if (_user.Id == null && User != null && User.Identity.Name != null)
				{
					_user = GetAsync<ApplicationUser>(RedisKeyHelper.CurrentUserKey(User.Identity.Name)).Result;
				}

				return _user;
			}
		}

		public Notification? GarageSetting
		{
			get
			{
				_notification = Session.GetSystemSetting();
				return _notification;
			}
		}


		public CompanySetting? CompanySetting
		{
			get
			{
				_companySetting = Session.GetCompanySettings();
				return _companySetting;
			}
		}


		public string? CurrentUserId => CurrentUser.Id;
		public string? CurrentUserName => CurrentUser.FullName;
		private async Task<T> GetAsync<T>(string key)
		{
			try
			{
				IDatabase cache = Connection.GetDatabase();
				var obj = await cache.GetAsync<T>(key);
				return obj;
			}
			catch (Exception ex)
			{
				Log.Logger.Error(ex.Message ?? ex.InnerException?.Message);
				return default;
			}
		}
		public void LogCritical(string message)
		{
			Log.Logger.Fatal(message);
		}

		public void LogError(string error)
		{
			Log.Logger.Error(error);
		}

		public void LogInformation(string error)
		{
			Log.Logger.Information(error);
		}

		public void LogVerbose(string error)
		{
			Log.Logger.Verbose(error);
		}

		public void LogWarning(string error)
		{
			Log.Logger.Warning(error);
		}

		public static ConnectionMultiplexer Connection
		{
			get
			{
				return RedisCacheService.Connection;
			}
		}
	}
}