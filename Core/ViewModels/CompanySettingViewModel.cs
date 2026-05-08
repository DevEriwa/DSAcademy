using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class CompanySettingViewModel
	{
		public Guid? CompanyId { get; set; }
		public string CompanyName { get; set; }

		[Display(Name = "Enable Quick Visit")]
		public bool QuickVisit { get; set; }

		[Display(Name = "Enable VHC")]
		public bool EnableVHC { get; set; }

		[Display(Name = "Turn Off Base Package")]
		public bool EnableBasePackage { get; set; }
		public string? DashboardUrl { get; set; }

		[Display(Name = "Enable SMS")]
		public bool EnableSMS { get; set; }
		[Display(Name = "Enable Web Module")]
		public bool EnableWebModule { get; set; }
		[Display(Name = "Enable Visit Payment")]
		public bool EnableVisitPayment { get; set; }
		[Display(Name = "Enable Custom Invoice")]
		public bool EnableCustomInvoice { get; set; }
		[Display(Name = "Enable Quotation")]
		public bool EnableQuotation { get; set; }
		[Display(Name = "Enable Diary")]
		public bool EnableDiary { get; set; }
		[Display(Name = "Enable Note")]
		public bool EnableNote { get; set; }

		[Display(Name = "Enable Supplier")]
		public bool EnableSupplier { get; set; }

		// ── Theme / Branding ──────────────────────────────────────────────
		[Display(Name = "Primary Color")]
		public string? PrimaryColor { get; set; } = "#0A192F";

		[Display(Name = "Secondary Color")]
		public string? SecondaryColor { get; set; } = "#FFB300";

		[Display(Name = "Sidebar Color")]
		public string? SidebarColor { get; set; } = "#112B50";

		[Display(Name = "Font Family")]
		public string? FontFamily { get; set; } = "Outfit";

		[Display(Name = "Dark Mode")]
		public bool DarkMode { get; set; } = false;
	}
}
