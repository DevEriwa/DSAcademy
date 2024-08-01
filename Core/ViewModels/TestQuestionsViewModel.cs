using Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class TestQuestionsViewModel
	{
		public int Id { get; set; }
		public int CourseId { get; set; }
		public string Question { get; set; }
		public List<string> OptionList { get; set; }
		public AnsweredQuestionss[] AnsweredQuestions { get; set; }
		public string Answer { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime DateCreated { get; set; }
		public GeneralAction ActionType { get; set; }
	}

	public class AnsweredQuestionss
	{
		public int questionId { get; set; }
		public string selectedAnswer { get; set; }
	}
}
