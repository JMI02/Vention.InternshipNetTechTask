using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence;

public class TaskDbContext : DbContext
{
    public DbSet<User>? Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder) 
        => builder.UseSqlite("Data Source=TechTask.db");
}