using Data.Entities;

namespace WebApp.Models
{
    public class HomeViewModel
    {
        public UserEditViewModel Profile { get; set; } = null!;
        public List<ProjectEntity> Projects { get; set; } = new();
        public List<UserEntity> TeamMembers { get; set;} = new();
    }
}
