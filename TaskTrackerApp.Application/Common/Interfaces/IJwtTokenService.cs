using TaskTrackerApp.Domain.Entities;

namespace TaskTrackerApp.Application.Common.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
