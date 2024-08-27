//using Core.Db;
//using Core.Models;
//using Logic.Helpers;
//using Logic.IHelpers;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json;
//using RedisCache;
//using static Logic.AppHttpContext;

//namespace DSAcademy.Controllers
//{
//	[SessionTimeout]
//	[Authorize(Roles = "SuperAdmin")]
//	public class SuperAdminController : BaseController
//	{
//		private readonly IAdminHelper _adminHelper;
//		private readonly ISuperAdminHelper _superAdminHelper;
//		private readonly IEmailHelper _emailHelper;
//		private readonly IUserHelper _userHelper;
//		private readonly ICompanyHelper _companyHelper;
//		private readonly SignInManager<ApplicationUser> _signInManager;
//		private readonly AppDbContext _context;
//		private readonly UserManager<ApplicationUser> _userManager;
//		private readonly ICacheService _cacheService;
//		private readonly IBaseHelper _baseHelper;
//		private readonly IDropdownHelper _dropdownHelper;

//		private readonly INotificationHelper _notificationHelper;
//		public SuperAdminController(
//			AppDbContext context,
//			IEmailHelper emailHelper,
//			IUserHelper userHelper,
//			IAdminHelper adminHelper,
//			SignInManager<ApplicationUser> signInManager,
//			UserManager<ApplicationUser> userManager,
//			ICompanyHelper companyHelper,

//			ISuperAdminHelper superAdminHelper,

//			ICacheService cacheService,
			
//			IBaseHelper baseHelper,
//			IDropdownHelper dropdownHelper)
//		{
//			_context = context;
//			_userHelper = userHelper;
//			_adminHelper = adminHelper;
//			_signInManager = signInManager;
//			_userManager = userManager;
//			_companyHelper = companyHelper;
//			_superAdminHelper = superAdminHelper;
//			_emailHelper = emailHelper;
//			_cacheService = cacheService;
//			//_notificationHelper = notificationHelper;
//			_baseHelper = baseHelper;
//			_dropdownHelper = dropdownHelper;
//		}

//		[HttpGet]
//		public IActionResult Index()
//		{
//			ViewBag.Layout = _userHelper.GetRoleLayout();
//			var allCompanies = _companyHelper.GetAllCompanies();
//			return View(allCompanies);
//		}

//		[HttpGet]
//		public async Task<IActionResult> CompanySetting(Guid companyId)
//		{
//			var companySettings = await _companyHelper.GetCompanySettings(companyId).ConfigureAwait(false);
//			if (companySettings != null)
//			{
//				return View(companySettings);
//			}
//			return View(companySettings);
//		}

//		[HttpPost]
//		public JsonResult CompanyCustomSetting(Guid companyId, string companySettingDetails, List<string> checkedCompanySettings, List<string> uncheckedCompanySettings)
//		{
//			var companySettingsViewModel = JsonConvert.DeserializeObject<CompanySettingViewModel>(companySettingDetails);
//			if (companySettingsViewModel == null)
//			{
//				return ResponseHelper.JsonError("Error occurred");
//			}
//			var companySetting = _companyHelper.GetCompanyCustomSettings(companyId, companySettingsViewModel, checkedCompanySettings, uncheckedCompanySettings);
//			if (companySetting != null)
//			{
//				return ResponseHelper.JsonSuccess("Company settings updated successfully");
//			}
//			return Json(new { isError = true, msg = "Could not update company settings", data = companySetting });
//		}
//		public async Task<JsonResult> ImpersonateCompanyAdmin(string email)
//		{
//			if (email != null)
//			{
//				var session = HttpContext.Session;
//				var superAdmin = Utility.GetCurrentUser();
//				if (superAdmin.CompanyBranchId == null && superAdmin.UserName != null)
//				{
//					superAdmin = _userHelper.UpdateSessionAsync(superAdmin.UserName).Result;

//				}
//				var getCompanyAdmin = _userManager.Users
//					.Where(x => x.Email == email)
//					.Include(c => c.CompanyBranch)
//					.Include(f => f.CompanyBranch.Company)
//					.FirstOrDefault();
//				if (getCompanyAdmin == null)
//				{
//					return Json(new { isError = true, msg = "This company does not exist" });
//				}

//				if (getCompanyAdmin.CompanyBranch.Deleted)
//				{
//					return Json(new { isError = true, msg = "This company has been deactivated, try another active user name" });
//				}

//				if (superAdmin.UserRole != Utility.Constants.SuperAdminRole)
//				{
//					return Json(new { isError = true, msg = "You can't impersonate yourself, you're logged In already." });
//				}
//				var impersonationRecord = new Impersonation
//				{
//					CompanyAdminId = getCompanyAdmin?.Id,
//					SuperAdminUserId = superAdmin.Id,
//					AmTheRealUser = false,
//					DateImpersonated = DateTime.Now
//				};
//				if (impersonationRecord != null)
//				{
//					_context.Impersonations.Add(impersonationRecord);
//					_context.SaveChanges();
//					session.Clear();
//					var companyAdmin = getCompanyAdmin;
//					await _signInManager.SignInAsync(companyAdmin, isPersistent: false).ConfigureAwait(false);
//					var companySetting = await _companyHelper.GetCompanySettings((Guid)companyAdmin.CompanyBranch.CompanyId).ConfigureAwait(false);
//					companyAdmin.QuickVisit = companySetting.QuickVisit;
//					companyAdmin.EnableSMS = companySetting.EnableSMS;
//					companyAdmin.EnableVisitPayment = companySetting.EnableVisitPayment;
//					var garageSystemSettings = await _notificationHelper.GetNotificationSettings(companyAdmin.CompanyBranchId).ConfigureAwait(false);
//					HttpContext.Session.SetString("garageSystemSettings", JsonConvert.SerializeObject(garageSystemSettings));
//					companyAdmin.CompanyBranch.Company.CreatedBy = null;
//					companyAdmin.Roles = (List<string>)await _userManager.GetRolesAsync(companyAdmin).ConfigureAwait(false);
//					companyAdmin.UserRole = Utility.Constants.CompanyAdminRole;
//					if (companyAdmin.CompanyBranch.Company.MessageSenderName == null)
//						companyAdmin.CompanyBranch.Company.MessageSenderName = companyAdmin.CompanyBranch.Company.Name.Length > 10 ? companyAdmin.CompanyBranch.Company.Name.Substring(0, 10) : companyAdmin.CompanyBranch.Company.Name;
//					var currentUser = JsonConvert.SerializeObject(companyAdmin);
//					HttpContext.Session.SetString("myuser", currentUser);
//					HttpContext.Session.SetString("isImpersonating", "true");
//					var companySub = JsonConvert.SerializeObject(companySetting);
//					HttpContext.Session.SetString("companySettings", companySub);
//					var SessionKeyName = companyAdmin.Id;
//					if (string.IsNullOrEmpty(session.GetString(SessionKeyName)))
//					{
//						session.SetString(SessionKeyName, "true");
//					}
//					else { session.Clear(); }

//					return Json(new { isError = false, msg = "Company admin Impersonated  successfully.", data = superAdmin.Id });

//				}
//			}
//			return Json(new { isError = true, msg = "An error has occurred, try again. Please contact support if the error persists. while impersonating." });
//		}

//		[HttpPost]
//		public JsonResult DeleteCompany(Guid companyId)
//		{
//			if (companyId != Guid.Empty)
//			{
//				var deleteCompany = _companyHelper.DeleteCompany(companyId);
//				if (deleteCompany)
//				{
//					return Json(new { isError = false, msg = "Company deleted successfully" });
//				}
//				return Json(new { isError = true, msg = "Unable to deleted Company" });
//			}
//			return Json(new { isError = true, msg = "An error has occurred, try again. Please contact support if the error persists." });
//		}
//		public IActionResult LoginUserLogs(IPageListModel<UserLogsViewModel>? model, DateTime date, int page = 1)
//		{
//			ViewBag.Layout = _userHelper.GetRoleLayout();
//			var loggedUser = _userHelper.GetLoggedInUsers(model, date, page);
//			model.Model = loggedUser;
//			model.SearchAction = "LoginUserLogs";
//			model.SearchController = "SuperAdmin";
//			return View(model);
//		}
//		public ActionResult UserSMSLog(IPageListModel<UserSmsViewModel>? model, DateTime date, int page = 1)
//		{
//			ViewBag.Layout = _userHelper.GetRoleLayout();
//			var sentSmsLists = _userHelper.GetSentSmsList(model, date, page);
//			model.Model = sentSmsLists;
//			model.SearchAction = "UserSMSLog";
//			model.SearchController = "SuperAdmin";
//			return View(model);
//		}

//		[HttpGet]
//		public IActionResult Packages()
//		{
//			ViewBag.Layout = _userHelper.GetRoleLayout();
//			ViewBag.Packages = _dropdownHelper.GetPackages();
//			var subscriptionPackages = _superAdminHelper.GetSubscriptionPackages();
//			return View(subscriptionPackages);
//		}

//		[HttpPost]
//		public JsonResult AddPackage(string packageDetails)
//		{
//			try
//			{
//				ViewBag.Layout = _userHelper.GetRoleLayout();
//				var subscriptionPackageViewModel = JsonConvert.DeserializeObject<SubscriptionPackageViewModel>(packageDetails);
//				if (subscriptionPackageViewModel == null)
//				{
//					return ResponseHelper.JsonError("Error occurred");
//				}
//				var isExistingPackage = _baseHelper.GetByPredicate<SubscriptionPackage>
//				(p => p.PackageEnum == subscriptionPackageViewModel.PackageEnum && p.Active)
//				.FirstOrDefault();
//				if (isExistingPackage != null)
//					return ResponseHelper.JsonError("Package already exist!");

//				var createModuleCost = _superAdminHelper.CreatePackage(subscriptionPackageViewModel);
//				if (createModuleCost)
//				{
//					return ResponseHelper.JsonSuccess("Package created successfully");
//				}
//				return ResponseHelper.JsonError("Unable to create package");
//			}
//			catch (Exception ex)
//			{
//				LogCritical($"{ex.Message} This exception message occurred while trying to add package");
//				throw;
//			}
//		}

//		[HttpPost]
//		public JsonResult EditSubscriptionPackage(string packageDetails)
//		{
//			if (packageDetails == null)
//			{
//				return Json(new { isError = true, msg = "Error occurred" });
//			}
//			var subscriptionPackageViewModel = JsonConvert.DeserializeObject<SubscriptionPackageViewModel>(packageDetails);
//			if (subscriptionPackageViewModel != null)
//			{
//				var createsubscriptionPackage = _superAdminHelper.EditSubscriptionPackages(subscriptionPackageViewModel);
//				if (createsubscriptionPackage)
//				{
//					return Json(new { isError = false, msg = "Subscription package updated successfully" });
//				}
//			}
//			return Json(new { isError = true, msg = "Unable to subscription package" });
//		}

//		[HttpGet]
//		public JsonResult GetPackageById(int packageId)
//		{
//			if (packageId > 0)
//			{
//				var package = _context.SubscriptionPackages.FirstOrDefault(c => c.Id == packageId && c.Active);
//				if (package != null)
//				{
//					return Json(package);
//				}
//			}
//			return null;

//		}


//		[HttpPost]
//		public JsonResult DeleteSubscriptionPackage(int packageId)
//		{
//			if (packageId == 0)
//			{
//				return Json(new { isError = true, msg = "Error occurred" });
//			}
//			var isSubscriptionPackageDeleted = _superAdminHelper.DeleteSubscriptionPackages(packageId);
//			if (isSubscriptionPackageDeleted)
//			{
//				return Json(new { isError = false, msg = "Subscription package deleted successfully" });
//			}
//			return Json(new { isError = true, msg = "Unable to delete subscription package" });
//		}

//		[HttpGet]
//		public IActionResult ViewListOfContact()
//		{
//			ViewBag.Layout = _userHelper.GetRoleLayout();
//			IEnumerable<Support> GetListOfContact = _context.Supports.Where(x => !x.Deleted).OrderBy(c => c.DateCreated);
//			return View(GetListOfContact);
//		}
//		[HttpGet]
//		public JsonResult GetSupportById(int? id)
//		{
//			try
//			{
//				if (id > 0)
//				{
//					var support = _context.Supports.Where(b => b.Id == id).FirstOrDefault();
//					if (support != null)
//					{
//						return Json(new { isError = false, data = support });
//					}
//				}
//				return Json(new { isError = true, msg = "No Result Found" });
//			}
//			catch (Exception)
//			{

//				throw;
//			}
//		}
//		[HttpPost]
//		public JsonResult FilterByDateRange(string dateFrom, string dateTo)
//		{
//			if (!String.IsNullOrEmpty(dateFrom) && dateTo != null)
//			{
//				var getFilterDate = _bookingHelper.GetDateFilteringByDateRange(dateFrom, dateTo);

//				if (getFilterDate.Count > 0 && getFilterDate != null)
//				{
//					return Json(new { isError = false, data = getFilterDate });
//				}
//				return Json(new { isError = true, msg = "No data available for this date range" });
//			}
//			return Json(new { isError = true, msg = "Please Enter details" });
//		}
//		[HttpGet]
//		public JsonResult GetMessageContentById(Guid? id)
//		{
//			try
//			{
//				if (id != Guid.Empty)
//				{
//					var messageCont = _superAdminHelper.GetSMSLogById(id);
//					if (messageCont != null)
//					{
//						messageCont.DateInString = messageCont.DateSent.ToString("D");
//						return Json(new { isError = false, data = messageCont });
//					}
//				}
//				return Json(new { isError = true, msg = "No Result Found" });
//			}
//			catch (Exception exp)
//			{
//				throw exp;
//			}
//		}

//		public JsonResult filterByDate(string dateFrom, string dateTo)
//		{
//			if (!String.IsNullOrEmpty(dateFrom) && dateTo != null)
//			{
//				var getDate = _userHelper.GetDateFilteringByDate(dateFrom, dateTo);

//				if (getDate.Count > 0 && getDate != null)
//				{
//					return Json(new { isError = false, data = getDate });
//				}
//				return Json(new { isError = true, msg = "No data available for this date range" });
//			}
//			return Json(new { isError = true, msg = "Please Enter details" });
//		}

//		[HttpGet]
//		public IActionResult CopyFiles()
//		{
//			ViewBag.Layout = _userHelper.GetRoleLayout();
//			//ViewBag.CompanyBranches = _dropDownHelper.GetAllCompanyBranch();
//			ViewBag.Company = _dropdownHelper.GetAllCompany();
//			ViewBag.Files = _dropdownHelper.GetCopyFileEnums();
//			var copyFile = _superAdminHelper.GetListOfCopiedFile();
//			return View(copyFile);
//		}

//		public JsonResult GetBranches(Guid companyId)
//		{
//			var companies = _dropdownHelper.GetAllCompanyBranches(companyId);
//			if (companies != null)
//				return Json(new { isError = false, data = companies });

//			return Json(new { isError = true });
//		}


//		public IActionResult SuperAdminSms()
//		{
//			ViewBag.Layout = _userHelper.GetRoleLayout();
//			ViewBag.Admins = _adminHelper.SuperAdminSms();
//			return View();
//		}
//	}
//}
