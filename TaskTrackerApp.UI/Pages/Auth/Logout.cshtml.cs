using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskTrackerApp.UI.Constants;

namespace TaskTrackerApp.UI.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Remove(SessionKeys.JwtToken);
            HttpContext.Session.Remove(SessionKeys.UserId);
            HttpContext.Session.Remove(SessionKeys.Email);
            HttpContext.Session.Remove(SessionKeys.UserRole);

            return RedirectToPage("/Index");
        }
    }
}
