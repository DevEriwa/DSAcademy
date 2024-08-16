using Core.Models;

namespace Core.ViewModels
{
	public class CompanyViewModel
	{
		public Guid? Id { get; set; }
		public string? CompanyAddress { get; set; }
		public string? Email { get; set; }
		public string? CompanyLogo { get; set; }
		public string? Phone { get; set; }
		public string? Mobile { get; set; }
		public string? CreatedById { get; set; }
		public  ApplicationUser? CreatedBy { get; set; }
		public DateTime DateCreated { get; set; }
		public string? Name { get; set; }
		public bool Deleted { get; set; }
		public bool Active { get; set; }
		public string? CompanyMotto { get; set; }
	}
}
