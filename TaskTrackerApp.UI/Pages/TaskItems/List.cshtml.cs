using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using TaskTrackerApp.UI.Constants;
using TaskTrackerApp.UI.Filters;
using TaskTrackerApp.UI.ViewModels;

namespace TaskTrackerApp.UI.Pages.TaskItems;

public class ListModel : LoginCheckPageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public List<TaskItemResponseViewModel> Tasks { get; set; } = new();

    public ListModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var (token, userId) = GetSessionCredentials();

        if (token is null || userId is null)
            return RedirectToPage("/Auth/Login");

        await LoadTasksAsync(token, userId);

        return Page();
    }

    public async Task<IActionResult> OnPostCompleteAsync(Guid id)
    {
        var (token, _) = GetSessionCredentials();

        if (token is null)
            return RedirectToPage("/Auth/Login");

        var client = CreateAuthorizedClient(token);

        var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Patch, $"{ApiRoutes.CompleteTask}/{id}"));

        TempData[response.IsSuccessStatusCode ? "SuccessMessage" : "ErrorMessage"] =
            response.IsSuccessStatusCode ? "Task marked as completed." : "Could not mark the task as completed.";

        return RedirectToPage("List");
    }

    public async Task<IActionResult> OnGetDeleteAsync(Guid id)
    {
        var (token, userId) = GetSessionCredentials();

        if (token is null || userId is null)
            return RedirectToPage("/Auth/Login");

        var client = CreateAuthorizedClient(token);

        var response = await client.DeleteAsync($"{ApiRoutes.DeleteTask}/{id}");

        if (response.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "Task deleted successfully.";
            return RedirectToPage("List");
        }

        TempData["ErrorMessage"] = "Failed to delete the task. Please try again.";

        await LoadTasksAsync(token, userId);

        return Page();
    }

    private async Task LoadTasksAsync(string token, string userId)
    {
        var client = CreateAuthorizedClient(token);

        var response = await client.GetAsync($"{ApiRoutes.GetTasksByUserId}/{userId}");

        if (!response.IsSuccessStatusCode)
        {
            Tasks = new();
            return;
        }

        var json = await response.Content.ReadAsStringAsync();

        Tasks = JsonSerializer.Deserialize<List<TaskItemResponseViewModel>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new();
    }

    private (string? Token, string? UserId) GetSessionCredentials()
    {
        var token = HttpContext.Session.GetString(SessionKeys.JwtToken);
        var userId = HttpContext.Session.GetString(SessionKeys.UserId);
        return (token, userId);
    }

    private HttpClient CreateAuthorizedClient(string token)
    {
        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }
}
