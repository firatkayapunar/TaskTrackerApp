using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerApp.CQRS.Users.Commands.Request;
using TaskTrackerApp.CQRS.Users.Queries.Request;

namespace TaskTrackerApp.WebAPI.Controllers;

public class UsersController(IMediator mediator) : CustomBaseController
{
    [Authorize(Roles = "Admin")]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] CreateUserCommandRequest request)
    {
        var username = HttpContext.User.Identity?.Name;

        var result = await mediator.Send(request);

        var location = result.IsSuccess && result.Data is not null
            ? Url.Action(nameof(GetByIdAsync), new { id = result.Data.Id })
            : null;

        return CreateActionResult(result, location!);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommandRequest request)
        => CreateActionResult(await mediator.Send(request));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
        => CreateActionResult(await mediator.Send(new GetAllUsersQueryRequest()));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        => CreateActionResult(await mediator.Send(new GetUserByIdQueryRequest(id)));

    [HttpGet("by-email")]
    public async Task<IActionResult> GetByEmailAsync([FromQuery] GetUserByEmailQueryRequest request)
    => CreateActionResult(await mediator.Send(request));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateUserCommandRequest request)
        => CreateActionResult(await mediator.Send(request with { Id = id }));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        => CreateActionResult(await mediator.Send(new DeleteUserCommandRequest(id)));
}
