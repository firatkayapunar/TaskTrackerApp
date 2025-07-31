using TaskTrackerApp.Domain.Entities;

namespace TaskTrackerApp.Application.Repositories;

public interface ITaskItemRepository : IGenericRepository<TaskItem>
{
    Task<List<TaskItem>> GetAllTasksAsync();
    Task<List<TaskItem>> GetTasksByUserIdAsync(Guid userId);
    Task<List<TaskItem>> GetRecentlyCompletedTasksAsync();
}
