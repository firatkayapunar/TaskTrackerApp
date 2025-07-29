using AutoMapper;
using MediatR;
using TaskTrackerApp.Application.Common.Interfaces;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.CQRS.Users.Commands.Request;
using TaskTrackerApp.CQRS.Users.Commands.Response;
using TaskTrackerApp.Domain.Entities;

namespace TaskTrackerApp.CQRS.Users.Handlers.CommandHandlers;

public sealed class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommandRequest, ServiceResult<CreateUserCommandResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IMapper mapper,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<ServiceResult<CreateUserCommandResponse>> Handle(
        CreateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var emailTaken = await _userRepository.AnyAsync(u => u.Email == request.Email);
        if (emailTaken)
            return ServiceResult<CreateUserCommandResponse>.Fail(ResultCode.Conflict, "Email is already in use.");

        var usernameTaken = await _userRepository.AnyAsync(u => u.Username == request.Username);
        if (usernameTaken)
            return ServiceResult<CreateUserCommandResponse>.Fail(ResultCode.Conflict, "Username is already taken.");

        var hashedPassword = _passwordHasher.Hash(request.Password);

        var user = _mapper.Map<User>(request);
        user.PasswordHash = hashedPassword;

        await _userRepository.AddAsync(user);

        var response = _mapper.Map<CreateUserCommandResponse>(user);

        return ServiceResult<CreateUserCommandResponse>.Success(response, ResultCode.Created);
    }
}
