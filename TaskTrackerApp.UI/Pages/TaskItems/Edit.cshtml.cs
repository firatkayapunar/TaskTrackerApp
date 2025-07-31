using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.Json;
using TaskTrackerApp.UI.Constants;
using TaskTrackerApp.UI.Filters;
using TaskTrackerApp.UI.ViewModels;

namespace TaskTrackerApp.UI.Pages.TaskItems;

public class EditModel : LoginCheckPageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EditModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public Guid Id { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; } = default!;

    [BindProperty]
    public string? Description { get; set; }

    public DateTime? DueDate { get; private set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var token = HttpContext.Session.GetString(SessionKeys.JwtToken);
        if (string.IsNullOrWhiteSpace(token))
            return RedirectToPage("/Auth/Login");

        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync($"{ApiRoutes.TaskItemById}/{id}");

        if (!response.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = "Task not found.";
            return RedirectToPage("List");
        }

        var json = await response.Content.ReadAsStringAsync();

        var task = JsonSerializer.Deserialize<TaskItemResponseViewModel>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (task is null)
        {
            TempData["ErrorMessage"] = "Task could not be loaded.";
            return RedirectToPage("List");
        }

        Id = task.Id;
        Title = task.Title;
        Description = task.Description;
        DueDate = task.DueDate; 

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

        var updateRequest = new UpdateTaskItemRequestViewModel
        {
            Id = Id,
            Title = Title.Trim(),
            Description = string.IsNullOrWhiteSpace(Description) ? null : Description.Trim()
        };

        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var json = JsonSerializer.Serialize(updateRequest);
        var content = new StringContent(json, Encoding.UTF8, ContentTypes.Json);

        var response = await client.PutAsync($"{ApiRoutes.TaskItemUpdate}/{Id}", content);

        if (response.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "Task updated successfully.";
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
                HttpStatusCode.BadRequest => "Invalid input. Please check the form fields.",
                HttpStatusCode.Unauthorized => "You must be logged in to update a task.",
                HttpStatusCode.Forbidden => "You do not have permission to update this task.",
                HttpStatusCode.NotFound => "Task not found. It may have been deleted.",
                _ => "Failed to update task. Please try again later."
            };
        }

        return Page();
    }
}
