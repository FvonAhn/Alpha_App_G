using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class UserEditViewModel
    {
        public int Id { get; set; }

        public string? CurrentAvatarUrl { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [Display(Name = "New Password")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string? ConfirmNewPassword { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile? NewAvatar { get; set; }
    }
}

