using Microsoft.EntityFrameworkCore;
using TaskTrackerApp.Domain.Entities;
using TaskTrackerApp.Infrastructure.Common.Marker;

namespace TaskTrackerApp.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<User> Users => Set<User>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>().HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InfrastructureAssemblyMarker).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
