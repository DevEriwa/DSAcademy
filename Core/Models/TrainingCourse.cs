using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class TrainingCourse: BaseModel
    {
		public string? Title { get; set; }
		public string? Description { get; set; }
		public decimal? Amount { get; set; }
		public string? Duration { get; set; }
		public int? TestDuration { get; set; }
		public string? Logo { get; set; }
		public ICollection<TrainingVideo> TrainingVideos { get; set; }
        public Guid? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }
        public ProgramEnum? ProgramStatus { get; set; }
    }

}
