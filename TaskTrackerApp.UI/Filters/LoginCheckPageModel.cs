using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TaskTrackerApp.UI.Filters
{
    public abstract class LoginCheckPageModel : PageModel
    {
        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (!context.HttpContext.Session.Keys.Contains("jwtToken"))
            {
                context.Result = new RedirectToPageResult("/Auth/Login");
                return;
            }
        }
    }
}
