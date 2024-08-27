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

	}
}
