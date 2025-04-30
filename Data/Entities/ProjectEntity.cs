namespace Data.Entities
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string ProjectName { get; set; } = null!;
        public string ClientName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;
    }
}
