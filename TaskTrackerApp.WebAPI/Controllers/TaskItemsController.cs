using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Request;
using TaskTrackerApp.Application.CQRS.TaskItems.Queries.Request;

namespace TaskTrackerApp.WebAPI.Controllers;

public class TaskItemsController(IMediator mediator) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
        => CreateActionResult(await mediator.Send(new GetAllTasksQueryRequest()));

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        => CreateActionResult(await mediator.Send(new GetTaskItemByIdQueryRequest(id)));

    [HttpGet("by-user/{userId:Guid}")]
    public async Task<IActionResult> GetByUserIdAsync([FromRoute] Guid userId)
        => CreateActionResult(await mediator.Send(new GetTasksByUserIdQueryRequest(userId)));

    [HttpGet("recent-completed")]
    public async Task<IActionResult> GetRecentlyCompletedAsync()
        => CreateActionResult(await mediator.Send(new GetRecentlyCompletedTasksQueryRequest()));

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTaskItemCommandRequest request)
    {
        var result = await mediator.Send(request);

        var locationUrl = result.IsSuccess && result.Data is not null
            ? Url.Action(nameof(GetByIdAsync), new { id = result.Data.Id })
            : null;

        return CreateActionResult(result, locationUrl!);
    }

    [HttpPatch("complete/{id}")]
    public async Task<IActionResult> CompleteAsync(Guid id) => CreateActionResult(await mediator.Send(new CompleteTaskItemCommandRequest(id)));

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateTaskItemCommandRequest request)
        => CreateActionResult(await mediator.Send(request with { Id = id }));

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        => CreateActionResult(await mediator.Send(new DeleteTaskItemCommandRequest(id)));
}
