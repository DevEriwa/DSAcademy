using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class PageStatisticsView
	{
		public DateTime PageDate { get; set; }
		public string? PageUrl { get; set; }
		public int CountOfPage { get; set; }
		public string? BranchName { get; set; }
		public string? PageName { get; set; }
		public string? CompanyName { get; set; }
		public Guid BranchId { get; set; }
		public Guid CompanyId { get; set; }
		public string? RoleName { get; set; }
	}
}
