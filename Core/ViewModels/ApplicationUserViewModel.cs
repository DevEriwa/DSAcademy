using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        public bool Deactivated { get; set; }

        [Display(Name = "Nysc")]
        public YesNoEnum HasCompletedNysc { get; set; }
        public YesNoEnum HasLaptop { get; set; }
        public YesNoEnum HasAnyProgrammingExp { get; set; }
        public string ProgrammingLanguagesExps { get; set; }
        public YesNoEnum ApplicantResideInEnugu { get; set; }
        [Display(Name = "Reason for Programming")]
        public string ReasonForProgramming { get; set; }
        public string AboutYourSkills { get; set; }
        public string HowDoYouIntendToCopeStatement { get; set; }
        public ApplicationStatus Status { get; set; }
        public string CV { get; set; }
        public string Message { get; set; }
        public bool ErrorHappened { get; set; }
        public bool IsAdmin { get; set; }
        public string Role { get; set; }
        public List<string> Roles { get; set; }
        public string Layout { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}
