using Microsoft.AspNetCore.Http;

namespace WebApp.Models
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        public string? CurrentAvatarUrl { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public IFormFile? NewAvatar { get; set; }
    }
}
