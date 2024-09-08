using Core.Enum;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class TrainingVideosViewModel
	{
		public Guid Id { get; set; }
		public int CourseId { get; set; }
		public string Title { get; set; }
		public string VideoLink { get; set; }
		public string Outline { get; set; }
		public bool IsActive { get; set; }
		public GeneralAction ActionType { get; set; }
		public DateTime DateCreated { get; set; }
		public List<TrainingVideo> Videos { get; set; }
	}
}
