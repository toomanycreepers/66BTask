using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebFootballers.Models.Utility
{
    public class ProjectUniqueValidations
    {
        public static ValidationResult ValidateDOBFormat(string dob, ValidationContext context)
        {
            string datePattern = @"^\d{4}-\d{2}-\d{2}$";
            if (!Regex.IsMatch(dob,datePattern)) 
            {
                return new ValidationResult("Дата рождения не совпадает с требуемым форматом.");
            } 
            return ValidationResult.Success;
        }
    }
}
