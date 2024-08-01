using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class AnswerOptions
	{
		public int Id { get; set; }
		public int? QuestionId { get; set; }
		[ForeignKey("QuestionId")]
		public virtual TestQuestions Question { get; set; }
		public string Option { get; set; }
	}
}
