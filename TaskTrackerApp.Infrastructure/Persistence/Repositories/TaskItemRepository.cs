using Microsoft.EntityFrameworkCore;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.Domain.Entities;
using TaskTrackerApp.Domain.Enums;

namespace TaskTrackerApp.Infrastructure.Persistence.Repositories;

public class TaskItemRepository : GenericRepository<TaskItem>, ITaskItemRepository
{
    private readonly ApplicationDbContext _context;

    public TaskItemRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetAllTasksAsync()
    {
        return await _context.TaskItems
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<TaskItem>> GetRecentlyCompletedTasksAsync()
    {
        return await _context.TaskItems
        .AsNoTracking()
            .Where(t => t.State == TaskItemState.Completed)
            .OrderByDescending(t => t.UpdatedAt ?? t.CreatedAt)
            .Take(10)
        .ToListAsync();
    }

    public async Task<List<TaskItem>> GetTasksByStateAsync(TaskItemState state)
    {
        return await _context.TaskItems
            .AsNoTracking()
            .Where(t => t.State == state)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<TaskItem>> GetTasksByUserIdAsync(Guid userId)
    {
        return await _context.TaskItems
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }
}
