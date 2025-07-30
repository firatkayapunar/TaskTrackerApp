using TaskTrackerApp.Domain.Auditing.BaseClasses;
using TaskTrackerApp.Domain.Entities;
using TaskTrackerApp.Domain.Enums;

namespace TaskTrackerApp.Application.Repositories;

public interface ITaskItemRepository : IGenericRepository<TaskItem>
{
    Task<List<TaskItem>> GetAllTasksAsync();
    Task<List<TaskItem>> GetTasksByUserIdAsync(Guid userId);
    Task<List<TaskItem>> GetTasksByStateAsync(TaskItemState state);
    Task<List<TaskItem>> GetRecentlyCompletedTasksAsync();
}
