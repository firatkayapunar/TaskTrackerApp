using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TaskTrackerApp.UI.Constants;
using TaskTrackerApp.UI.ViewModels.Auth;
using TaskTrackerApp.UI.ViewModels.Enums;

namespace TaskTrackerApp.UI.Pages.Auth;

public class RegisterModel : PageModel
{
    [BindProperty]
    [Required(ErrorMessage = "First name is required.")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = default!;

    [BindProperty]
    [Required(ErrorMessage = "Last name is required.")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = default!;

    [BindProperty]
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = default!;

    [BindProperty]
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(32, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [BindProperty]
    [Required(ErrorMessage = "Please retype the password.")]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    [DataType(DataType.Password)]
    public string RePassword { get; set; } = default!;

    private readonly IHttpClientFactory _httpClientFactory;

    public RegisterModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult OnGet()
    {
        var token = HttpContext.Session.GetString(SessionKeys.JwtToken);
        var role = HttpContext.Session.GetString(SessionKeys.UserRole);

        if (string.IsNullOrWhiteSpace(token) || role != UserRole.Admin.ToString())
            return RedirectToPage("/TaskItems/List");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Please fill in all fields correctly.";
            return Page();
        }

        var token = HttpContext.Session.GetString(SessionKeys.JwtToken);

        if (string.IsNullOrWhiteSpace(token))
        {
            TempData["ErrorMessage"] = "Your session has expired. Please login again.";
            return RedirectToPage("/Auth/Login");
        }

        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var registerRequest = new RegisterRequestViewModel
        {
            FirstName = FirstName.Trim(),
            LastName = LastName.Trim(),
            Email = Email.Trim().ToLowerInvariant(),
            Password = Password
        };

        var json = JsonSerializer.Serialize(registerRequest);
        var content = new StringContent(json, Encoding.UTF8, ContentTypes.Json);

        var response = await client.PostAsync(ApiRoutes.Register, content);

        if (response.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "Registration successful! You can now log in.";
            return RedirectToPage("Login");
        }

        var responseBody = await response.Content.ReadAsStringAsync();

        try
        {
            var errors = JsonSerializer.Deserialize<List<string>>(responseBody);
            TempData["ErrorMessage"] = errors?.FirstOrDefault() ?? "An unexpected error occurred.";
        }
        catch
        {
            TempData["ErrorMessage"] = response.StatusCode switch
            {
                HttpStatusCode.Conflict => "A user with this email already exists.",
                HttpStatusCode.Unauthorized => "You must be logged in to register a user.",
                HttpStatusCode.Forbidden => "Only admin users can register new users.",
                HttpStatusCode.BadRequest => "Invalid input. Please check the form values.",
                _ => "Registration failed. Please try again later."
            };
        }

        return Page();
    }

}
