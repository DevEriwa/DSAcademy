using Core.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
	public class Payments: BaseModel
    {
		public TransactionType? Source { get; set; }
		public string? InvoiceNumber { get; set; }
		public string? ProveOfPayment { get; set; }
		public int? CourseId { get; set; }
		[ForeignKey("CourseId")]
		public virtual TrainingCourse? Courses { get; set; }
		public PaymentStatus? Status { get; set; }
		public Guid? CompanyId { get; set; }
		[ForeignKey("CompanyId")]
		public virtual Company? Company { get; set; }
		public ProgramEnum? ProgramStatus { get; set; }
	}
}
