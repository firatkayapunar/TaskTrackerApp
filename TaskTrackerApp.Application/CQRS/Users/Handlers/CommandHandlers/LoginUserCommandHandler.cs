using MediatR;
using TaskTrackerApp.Application.Common.Interfaces;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.CQRS.Users.Commands.Request;
using TaskTrackerApp.CQRS.Users.Commands.Response;

namespace TaskTrackerApp.CQRS.Users.Handlers.CommandHandlers;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, ServiceResult<LoginUserCommandResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher _passwordHasher;

    public LoginUserCommandHandler(
            IUserRepository userRepository,
            IJwtTokenService jwtTokenService,
            IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<ServiceResult<LoginUserCommandResponse>> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
            return ServiceResult<LoginUserCommandResponse>.Fail(ResultCode.NotFound, "User not found");

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            return ServiceResult<LoginUserCommandResponse>.Fail(ResultCode.Unauthorized, "Invalid credentials");

        var token = _jwtTokenService.GenerateToken(user.Id, user.Username, user.Email);

        var response = new LoginUserCommandResponse(token, user.Email, user.Username);
        return ServiceResult<LoginUserCommandResponse>.Success(response);
    }
}
 