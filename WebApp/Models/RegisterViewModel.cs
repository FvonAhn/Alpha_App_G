using System.ComponentModel.DataAnnotations;
using WebApp.Attributes;

namespace WebApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "You must enter your full name.")]
        [DataType(DataType.Text)]
        [Display(Name = "Full Name", Prompt = "Enter your full name")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "You must enter your email.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Enter your email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "You must enter a password.")]
        [PasswordStrength]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Enter your password")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "You must confirm your password.")]
        [Compare(nameof(Password), ErrorMessage = "Password must be confirmed.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
        public string ConfirmPassword { get; set; } = null!;

        public string? AvatarUrl { get; set; }

        [Display(Name = "Terms And Conditions", Prompt = "I accept the terms and conditions.")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions")]
        public bool TermsAndConditions { get; set; }            
    }
}
