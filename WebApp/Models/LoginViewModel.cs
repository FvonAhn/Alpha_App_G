using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "You must enter your email to sign in.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Enter your email.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "You must enter your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Enter your password.")]
        public string Password { get; set; } = null!;

        [Display(Name = "Remember me", Prompt = "Remember me.")]
        public bool RememberMe { get; set; }
    }
}
