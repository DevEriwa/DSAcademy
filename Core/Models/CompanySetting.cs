using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class CompanySetting
	{
		[Key]
		public Guid? CompanyId { get; set; }
		[Display(Name = "Company")]
		[ForeignKey("CompanyId")]
		public virtual Company? Company { get; set; }
		[Display(Name = "Enable Custom Invoice")]
		public bool EnableCustomInvoice { get; set; }
		[Display(Name = "Enable Web Module")]
		public double CharLengthPerPage { get; set; }
		[Display(Name = "Dashboard Url")]
		public string? DashboardUrl { get; set; }
		[Display(Name = "Enable Base Package")]
		public bool EnableBasePackage { get; set; }
		[Display(Name = "Enable SMS")]
		public bool EnableSMS { get; set; }
		
	}
}
