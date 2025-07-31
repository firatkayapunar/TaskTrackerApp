using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TaskTrackerApp.UI.Constants;
using TaskTrackerApp.UI.Filters;
using TaskTrackerApp.UI.ViewModels;

namespace TaskTrackerApp.UI.Pages.TaskItems;

public class CreateModel : LoginCheckPageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CreateModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; } = default!;

    [BindProperty]
    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = default!;

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Please fill in all fields correctly.";
            return Page();
        }

        var token = HttpContext.Session.GetString(SessionKeys.JwtToken);
        var userId = HttpContext.Session.GetString(SessionKeys.UserId);

        if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(userId))
        {
            TempData["ErrorMessage"] = "Your session has expired. Please login again.";
            return RedirectToPage("/Auth/Login");
        }

        var request = new CreateTaskItemRequestViewModel
        {
            Title = Title.Trim(),
            Description = Description.Trim(),
            UserId = Guid.Parse(userId)
        };

        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, ContentTypes.Json);

        var response = await client.PostAsync(ApiRoutes.TaskItemCreate, content);

        if (response.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "Task created successfully.";
            return RedirectToPage("List");
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
                HttpStatusCode.Conflict => "A task with the same title already exists.",
                HttpStatusCode.Unauthorized => "You must be logged in to create a task.",
                HttpStatusCode.Forbidden => "You do not have permission to create tasks.",
                HttpStatusCode.BadRequest => "Invalid input. Please review your entries.",
                _ => "Task could not be created. Please try again later."
            };
        }

        return Page();
    }

}
