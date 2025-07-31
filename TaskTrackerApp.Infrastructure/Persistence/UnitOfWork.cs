using TaskTrackerApp.Application.Common.Interfaces;

namespace TaskTrackerApp.Infrastructure.Persistence;

public class UnitOfWork(ApplicationDbContext appDbContext) : IUnitOfWork
{
    public Task<int> SaveChangeAsync() => appDbContext.SaveChangesAsync();
}
