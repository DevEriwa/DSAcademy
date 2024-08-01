using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class AnswerOptionViewModel
	{
		public int QuestionId { get; set; }
		public string OptionOne { get; set; }
		public string OptionTwo { get; set; }
		public string OptionThree { get; set; }
		public string OptionFour { get; set; }
	}

}
