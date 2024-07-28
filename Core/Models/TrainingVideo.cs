using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class TrainingVideo
	{
		[Key]
		public Guid? Id { get; set; }

		[Required]
		public int? CourseId { get; set; }
		[ForeignKey("CourseId")]
		public virtual TrainingCourse? Course { get; set; } 
		public string? VideoLink { get; set; }
		public string? VideoPath { get; set; }
		public string? Outline { get; set; }
		public bool IsActive { get; set; }
		public DateTime DateCreated { get; set; }
	}

}
