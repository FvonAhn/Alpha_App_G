﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ProjectEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string? ProjectName { get; set; }

        [Required]
        public string? ClientName { get; set; }
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal Budget { get; set; }
    }
}
