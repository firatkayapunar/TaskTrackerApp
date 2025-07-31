using AutoMapper;
using MediatR;
using TaskTrackerApp.Application.Common.Interfaces;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.CQRS.Users.Commands.Request;
using TaskTrackerApp.CQRS.Users.Commands.Response;
using TaskTrackerApp.Domain.Entities;
using TaskTrackerApp.Domain.Enums;

namespace TaskTrackerApp.CQRS.Users.Handlers.CommandHandlers;

public sealed class CreateUserCommandHandler(
    IUserRepository userRepository,
    IMapper mapper,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork)
        : IRequestHandler<CreateUserCommandRequest, ServiceResult<CreateUserCommandResponse>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<CreateUserCommandResponse>> Handle(
        CreateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var emailTaken = await _userRepository.AnyAsync(u => u.Email == request.Email);
        if (emailTaken)
            return ServiceResult<CreateUserCommandResponse>.Fail(ResultCode.Conflict, "Email is already in use.");

        var generatedUsername = $"{request.FirstName}.{request.LastName}"
            .ToLowerInvariant()
            .Replace(" ", "");

        var usernameTaken = await _userRepository.AnyAsync(u => u.Username == request.Username);
        if (usernameTaken)
            return ServiceResult<CreateUserCommandResponse>.Fail(ResultCode.Conflict, "Username is already taken.");

        var hashedPassword = _passwordHasher.Hash(request.Password);

        var user = _mapper.Map<User>(request);
        user.PasswordHash = hashedPassword;
        user.Username = generatedUsername;
        user.Role =  UserRole.User; 

        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangeAsync();

        var response = _mapper.Map<CreateUserCommandResponse>(user);

        return ServiceResult<CreateUserCommandResponse>.Success(response, ResultCode.Created);
    }
}
