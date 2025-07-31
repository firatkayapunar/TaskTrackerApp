using MediatR;
using TaskTrackerApp.Application.Common.Interfaces;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.CQRS.Users.Commands.Request;
using TaskTrackerApp.CQRS.Users.Commands.Response;

namespace TaskTrackerApp.CQRS.Users.Handlers.CommandHandlers;

public class LoginUserCommandHandler(
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService,
        IPasswordHasher passwordHasher) : IRequestHandler<LoginUserCommandRequest, ServiceResult<LoginUserCommandResponse>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<ServiceResult<LoginUserCommandResponse>> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
            return ServiceResult<LoginUserCommandResponse>.Fail(ResultCode.NotFound, "User not found");

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            return ServiceResult<LoginUserCommandResponse>.Fail(ResultCode.Unauthorized, "Invalid credentials");

        var token = _jwtTokenService.GenerateToken(user);

        var response = new LoginUserCommandResponse(token, user.Email, user.Username, user.Role,user.Id);

        return ServiceResult<LoginUserCommandResponse>.Success(response);
    }
}
