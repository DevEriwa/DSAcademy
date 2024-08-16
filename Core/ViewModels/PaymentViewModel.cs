using Core.Enum;
using Core.Models;

namespace Core.ViewModels
{
	public class PaymentViewModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public TransactionType? Source { get; set; }
		public string? InvoiceNumber { get; set; }
		public string? ProveOfPayment { get; set; }
		public int? CourseId { get; set; }
		public  TrainingCourse? Courses { get; set; }
		public PaymentStatus? Status { get; set; }
		public string? UserId { get; set; }
		public Guid? CompanyId { get; set; }
		public  ApplicationUser? User { get; set; }
	}
}
