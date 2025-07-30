using Microsoft.EntityFrameworkCore;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.Domain.Entities;

namespace TaskTrackerApp.Infrastructure.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public Task<List<User>> GetAllUsersAsync()
    {
        return _context.Users
            .AsNoTracking()
            .ToListAsync();
    }
}
