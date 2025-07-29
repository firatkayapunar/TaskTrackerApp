namespace TaskTrackerApp.Application.Common.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(Guid userId, string username, string email);
}
