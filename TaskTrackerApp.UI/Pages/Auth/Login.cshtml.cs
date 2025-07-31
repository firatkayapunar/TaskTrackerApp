using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.Json;
using TaskTrackerApp.UI.Constants;
using TaskTrackerApp.UI.ViewModels.Auth;

namespace TaskTrackerApp.UI.Pages.Auth;

public class LoginModel : PageModel
{
    [BindProperty]
    [Required(ErrorMessage = "Email field cannot be empty.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email.")]
    public string Email { get; set; } = default!;

    [BindProperty]
    [Required(ErrorMessage = "Password cannot be empty.")]
    [StringLength(32, MinimumLength = 5, ErrorMessage = "Password must be at least 5 characters.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    private readonly IHttpClientFactory _httpClientFactory;

    public LoginModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult OnGet()
    {
        var token = HttpContext.Session.GetString(SessionKeys.JwtToken);
        if (!string.IsNullOrWhiteSpace(token))
            return RedirectToPage("/TaskItems/List");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var client = _httpClientFactory.CreateClient("ApiClient");

        var loginRequest = new
        {
            Email = Email.Trim().ToLowerInvariant(),
            Password
        };

        var json = JsonSerializer.Serialize(loginRequest);
        var content = new StringContent(json, Encoding.UTF8, ContentTypes.Json);

        var response = await client.PostAsync(ApiRoutes.Login, content);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            var loginResult = JsonSerializer.Deserialize<LoginResponseViewModel>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (!string.IsNullOrWhiteSpace(loginResult?.Token))
            {
                HttpContext.Session.SetString(SessionKeys.JwtToken, loginResult.Token);
                HttpContext.Session.SetString(SessionKeys.UserId, loginResult.UserId.ToString());
                HttpContext.Session.SetString(SessionKeys.Email, loginResult.Email);
                HttpContext.Session.SetString(SessionKeys.UserRole, loginResult.Role.ToString());

                TempData["SuccessMessage"] = "Login successful. Welcome!";

                return RedirectToPage("/TaskItems/List");
            }

            TempData["ErrorMessage"] = "Login failed. Token not received.";

            return Page();
        }

        var responseBodyText = await response.Content.ReadAsStringAsync();

        try
        {
            var errors = JsonSerializer.Deserialize<List<string>>(responseBodyText);
            TempData["ErrorMessage"] = errors?.FirstOrDefault() ?? "An unexpected error occurred.";
        }
        catch
        {
            TempData["ErrorMessage"] = response.StatusCode switch
            {
                HttpStatusCode.Unauthorized => "Incorrect email or password.",
                _ => "An unexpected error occurred. Please try again later."
            };
        }

        return Page();
    }
}
