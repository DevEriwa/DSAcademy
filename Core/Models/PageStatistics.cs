using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class PageStatistics
	{
		public Guid Id { get; set; }
		public Guid? CompanyId { get; set; }
		[Display(Name = "Company")]
		[ForeignKey("CompanyId")]
		public virtual Company? Company { get; set; }
		public string? UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual ApplicationUser? User { get; set; }
		public string? FullPageUrl { get; set; }
		public string? PageUrl { get; set; }
		public DateTime PageDate { get; set; }
		public string? PageName { get; set; }
		public string? RoleName { get; set; }
	}
}
