using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DbView
{
	[Keyless]
	public class View_PageStatisticsView
	{
		public string? PageUrl { get; set; }
		public int CountOfPage { get; set; }
		public string? BranchName { get; set; }
		public string? CompanyName { get; set; }
		public Guid CompanyId { get; set; }
		public string? PageName { get; set; }
		public string? RoleName { get; set; }
	}
}
