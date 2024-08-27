using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCache
{
	public class RedisKeyHelper
	{
		public static string CustomerKey(Guid? companyBranchId) => $"cachedCustomers:Customers_{companyBranchId}";
		public static string CurrentUserKey(string userName) => $"cachedUserName:User_{userName}";
		public static string AdminDashboardKey(Guid? companyBranchId) => $"cachedAdminDashboard:AdminDashboard_{companyBranchId}";
		public static string ManagersDashboardKey(Guid? companyBranchId) => $"cachedManagersDashboardKey:ManagersDashboard_{companyBranchId}";
		public static string QuikSaleKey(Guid? companyBranchId) => $"cachedQuickSale:QuickSales_{companyBranchId}";
		public static string SuperAdminKey => $"cachedSuperAdmin:SuperAdminList";
		public static string PageSize(Guid? companyBranchId) => $"cachedPageSize:PageSize_{companyBranchId}";
		public static string BookProductKey(Guid? companyBranchId) => $"cachedBookingProduct:BookingProduct_{companyBranchId}";
		public static string BookingCustomerKey(Guid? companyBranchId) => $"cachedCustomerBookingProduct:CustomerBookingProduct_{companyBranchId}";
		public static string CompanySettingsKey(Guid? companyBranchId) => $"cachedCompanySettings:CompanySettings_{companyBranchId}";
		public static string ProductsKey(Guid? companyBranchId) => $"cachedProducts:Products_{companyBranchId}";
		public static string ProductCategoriesKey(Guid? companyBranchId) => $"cachedProductCategories:ProductCategories_{companyBranchId}";
		public static string UnitsKey(Guid? companyBranchId) => $"cachedUnits:Units_{companyBranchId}";
		public static string SupplierKey(Guid? companyBranchId) => $"cachedSupplier:Supplier_{companyBranchId}";
		public static string PatientsKey(Guid? companyBranchId) => $"cachedPatients:Patients_{companyBranchId}";
		public static string VisitTreatmentKey(Guid? companyBranchId) => $"cachedVisitTreatments:VisitTreatments_{companyBranchId}";
		public static string SpeciesKey(Guid? companyBranchId) => $"cachedSpecies:Species_{companyBranchId}";
		public static string GenderKey(Guid? companyBranchId) => $"cachedGender:Gender_{companyBranchId}";
		public static string CompanyWorkHourPerDay(Guid? companyBranchId) => $"cachedWorkHourPerDay:WorkHourPerDay{companyBranchId}";
		public static string CompanyInvoiceTerms(Guid? companyBranchId) => $"cachedCompanyInvoiceTerms:CompanyInvoiceTerms_{companyBranchId}";
		public static string QuickVisitMOTCost(Guid? companyBranchId) => $"cachedQuickVisitMOTCost:QuickVisitMOTCost_{companyBranchId}";
		public static string MotReminderDays(Guid? companyBranchId) => $"cachedMotReminderDays:MotReminderDays_{companyBranchId}";
		public static string VHCDashboardKey(Guid? companyBranchId) => $"cachedVHCDashboard:VHCDashboard_{companyBranchId}";
		public static string CustomHomePageDashboardKey(string? companyName) => $"cachedCustomHomePage:CustomHomePage_{companyName}";
		public static string FacebookAccessTokenKey(Guid? companyBranchId) => $"cachedFacebookAccessToken:FacebookAccessToken_{companyBranchId}";
		public static string GoogleMapIdKey(Guid? companyBranchId) => $"cachedGoogleMapId:GoogleMapId_{companyBranchId}";
		public static string CompanyBranchKey(Guid? companyBranchId) => $"cachedCompanyBranch:CompanyBranch_{companyBranchId}";
		public static string GarageSetting(Guid? companyBranchId) => $"cachedGarageSetting:GarageSetting{companyBranchId}";
		public static string UserNotes(string currentUserId) => $"cachedUserNotes:UserNotes{currentUserId}";
	}
}
