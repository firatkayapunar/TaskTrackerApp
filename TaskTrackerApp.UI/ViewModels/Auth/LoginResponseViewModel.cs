using TaskTrackerApp.UI.ViewModels.Enums;

namespace TaskTrackerApp.UI.ViewModels.Auth;

public class LoginResponseViewModel
{
    public string Token { get; set; } = default!;
    public Guid UserId { get; set; }
    public string Email { get; set; } = default!;
    public UserRole Role { get; set; }
}
