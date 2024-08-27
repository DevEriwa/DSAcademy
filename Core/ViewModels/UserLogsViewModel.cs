using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class UserLogsViewModel
	{
		public int Id { get; set; }
		public string? UserId { get; set; }
		public DateTime LoggedDate { get; set; }
		public string? IpAddress { get; set; }
		public string? DeviceName { get; set; }
		public string? UserName { get; set; }
		public bool IsActive { get; set; }
		public string? StartDate { get; set; }
		public string? EndDate { get; set; }
		public string? CompanyName { get; set; }
	}
}
