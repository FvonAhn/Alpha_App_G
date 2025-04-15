using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<ProjectEntity> Projects { get; set; } = null!;
    }
}
