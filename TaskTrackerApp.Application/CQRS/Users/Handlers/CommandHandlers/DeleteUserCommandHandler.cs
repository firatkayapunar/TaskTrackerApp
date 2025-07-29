using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.CQRS.Users.Commands.Request;
using TaskTrackerApp.CQRS.Users.Commands.Response;

namespace TaskTrackerApp.CQRS.Users.Handlers.CommandHandlers;

public sealed class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommandRequest, ServiceResult<DeleteUserCommandResponse>>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ServiceResult<DeleteUserCommandResponse>> Handle(
        DeleteUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
            return ServiceResult<DeleteUserCommandResponse>.Fail(ResultCode.NotFound, $"User with ID '{request.Id}' not found.");

        _userRepository.Delete(user);

        var response = new DeleteUserCommandResponse(true);

        return ServiceResult<DeleteUserCommandResponse>.Success(response, ResultCode.NoContent);
    }
}
