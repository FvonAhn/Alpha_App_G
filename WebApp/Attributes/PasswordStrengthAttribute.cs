using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApp.Attributes
{
    public class PasswordStrengthAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = value as string;

            if (string.IsNullOrWhiteSpace(password))
                return new ValidationResult("You must enter a password.");

            var errors = new List<string>();

            if (!Regex.IsMatch(password, @"[A-Z]"))
                errors.Add("an uppercase letter");

            if (!Regex.IsMatch(password, @"[a-z]"))
                errors.Add("a lowercase letter");

            if (!Regex.IsMatch(password, @"\d"))
                errors.Add("a number");

            if (!Regex.IsMatch(password, @"[\W_]"))
                errors.Add("a special character");

            if (password.Length < 5)
                errors.Add("at least 5 characters long");

            if (errors.Count > 0)
                return ValidationResult.Success;

            return new ValidationResult($"Password must contain {string.Join(", ", errors)}");
        }

    }
}
