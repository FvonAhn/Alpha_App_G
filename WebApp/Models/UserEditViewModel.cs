using Microsoft.AspNetCore.Http;

namespace WebApp.Models
{
    public class UserEditViewModel
    {
        public string CurrentAvatarUrl { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;

        public IFormFile? NewAvatar { get; set; }
    }
}
