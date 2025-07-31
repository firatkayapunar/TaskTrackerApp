namespace TaskTrackerApp.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangeAsync();
}
