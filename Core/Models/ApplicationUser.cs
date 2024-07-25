﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace Core.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		public string Address { get; set; }
		public DateTime DateRegistered { get; set; }
		public bool Deactivated { get; set; }
		public YesNoEnum HasLaptop { get; set; }
		public YesNoEnum HasAnyProgrammingExp { get; set; }
		public string? ProgrammingLanguagesExps { get; set; }
		public YesNoEnum ApplicantResideInEnugu { get; set; }

		[Display(Name = "Reason for Programming")]
		public string? ReasonForProgramming { get; set; }
		public string HowDoYouIntendToCopeStatement { get; set; }
		public bool IsAdmin { get; set; }
		public ApplicationStatus Status { get; set; }
		[NotMapped]
		public string Message { get; set; }
		[NotMapped]
		public bool ErrorHappened { get; set; }
		[Display(Name = "Full Name")]
		[NotMapped]
		public string FullName => FirstName + " " + LastName;
		[NotMapped]
		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password and Confirm Password must be the same. ")]
		public string ConfirmPassword { get; set; }
		[NotMapped]
		public string ActionType { get; set; }
		public string Role { get; set; } 
		public bool IsEnrolled { get; set; } = true; 
		public bool IsApproved { get; set; } = false; 
	}
}
