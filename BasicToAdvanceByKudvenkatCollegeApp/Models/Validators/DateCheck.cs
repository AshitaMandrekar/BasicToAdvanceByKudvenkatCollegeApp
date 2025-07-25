using System.ComponentModel.DataAnnotations;

namespace BasicToAdvanceByKudvenkatCollegeApp.Models.Validators
{
    public class DateCheck : ValidationAttribute
    {
        // just type protected override isvalid and enter
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;
            if(date< DateTime.Now)
            {
                return new ValidationResult("Date must be greater than today's date");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
