using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class TestQuestions
	{
		[Key]
		public int Id { get; set; }
		public int? CourseId { get; set; }
		[ForeignKey("CourseId")]
		public virtual TrainingCourse Course { get; set; }
		public string Question { get; set; }
		public string Answer { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
