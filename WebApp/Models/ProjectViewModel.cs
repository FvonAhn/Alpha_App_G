using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ProjectViewModel
    {
        public string Image { get; set; } = null!;

        [Required]
        public string ProjectName { get; set; } = null!;

        [Required]
        public string ClientName { get; set; } = null!;
        public string Description { get; set; } = null!;

        [Required]  
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal Budget { get; set; }

        public bool IsCompleted { get; set; }
    }
}
