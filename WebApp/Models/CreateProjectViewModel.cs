using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class CreateProjectViewModel
    {
        [Required]
        public string ProjectName { get; set; } = null!;

        public string? Image { get; set; }

        [Required]
        public string ClientName { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required]
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(15);

        [Required]
        public decimal Budget { get; set; }
    }
}
