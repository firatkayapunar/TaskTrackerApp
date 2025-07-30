using TaskTrackerApp.Domain.Auditing.BaseClasses;
using TaskTrackerApp.Domain.Entities;

namespace TaskTrackerApp.Application.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<List<User>> GetAllUsersAsync();
}
