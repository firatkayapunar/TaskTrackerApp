using MediatR;
using TaskTrackerApp.Application.Common.Interfaces;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.CQRS.Users.Commands.Request;
using TaskTrackerApp.CQRS.Users.Commands.Response;

namespace TaskTrackerApp.CQRS.Users.Handlers.CommandHandlers;

public sealed class DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteUserCommandRequest, ServiceResult<DeleteUserCommandResponse>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<DeleteUserCommandResponse>> Handle(
        DeleteUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
            return ServiceResult<DeleteUserCommandResponse>.Fail(ResultCode.NotFound, $"User with ID '{request.Id}' not found.");

        _userRepository.Delete(user);
        await _unitOfWork.SaveChangeAsync();

        var response = new DeleteUserCommandResponse(true);

        return ServiceResult<DeleteUserCommandResponse>.Success(response, ResultCode.NoContent);
    }
}
