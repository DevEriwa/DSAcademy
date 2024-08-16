using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
	public class AnswerOptions
	{
		public int? Id { get; set; }
		public int? QuestionId { get; set; }
		[ForeignKey("QuestionId")]
		public virtual TestQuestions? Question { get; set; }
		public string? Option { get; set; }
        public Guid? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }
    }
}
