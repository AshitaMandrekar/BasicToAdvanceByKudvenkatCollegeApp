using System.ComponentModel.DataAnnotations;
using BasicToAdvanceByKudvenkatCollegeApp.Models.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BasicToAdvanceByKudvenkatCollegeApp.Models
{
    public class StudentDTO
    {
        [ValidateNever] // this field is never validated
        public int Id { get; set; }
        [Required(ErrorMessage ="student name required")]
        [StringLength(40)]
        public string StudentName { get; set; }
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; }
        [Range(10,20)]
        public int age { get; set; }

        [Required(ErrorMessage ="address is required")]
        public string Address { get; set; }

        public string Password { get; set; }
        // [Compare("Password")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [DateCheck]
        public DateTime AdmissionDate { get; set; }
    }
}
