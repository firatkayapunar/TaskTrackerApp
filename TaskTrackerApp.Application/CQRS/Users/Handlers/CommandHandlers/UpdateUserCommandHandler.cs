using AutoMapper;
using MediatR;
using TaskTrackerApp.Application.Common.Interfaces;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.CQRS.Users.Commands.Request;
using TaskTrackerApp.CQRS.Users.Commands.Response;

namespace TaskTrackerApp.CQRS.Users.Handlers.CommandHandlers;

public sealed class UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateUserCommandRequest, ServiceResult<UpdateUserCommandResponse>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<UpdateUserCommandResponse>> Handle(
        UpdateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
            return ServiceResult<UpdateUserCommandResponse>.Fail(ResultCode.NotFound, $"User with ID '{request.Id}' not found.");

        var emailTaken = await _userRepository.AnyAsync(u => u.Email == request.Email && u.Id != request.Id);

        if (emailTaken)
            return ServiceResult<UpdateUserCommandResponse>.Fail(ResultCode.Conflict, "Email is already in use.");

        var usernameTaken = await _userRepository.AnyAsync(u => u.Username == request.Username && u.Id != request.Id);

        if (usernameTaken)
            return ServiceResult<UpdateUserCommandResponse>.Fail(ResultCode.Conflict, "Username is already taken.");

        _mapper.Map(request, user);

        _userRepository.Update(user);
        await _unitOfWork.SaveChangeAsync();
        
        var response = _mapper.Map<UpdateUserCommandResponse>(user);

        return ServiceResult<UpdateUserCommandResponse>.Success(response);
    }
}
