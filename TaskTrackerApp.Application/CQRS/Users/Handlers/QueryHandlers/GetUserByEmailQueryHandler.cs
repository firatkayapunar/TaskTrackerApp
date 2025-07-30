using AutoMapper;
using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.CQRS.Users.Queries.Request;
using TaskTrackerApp.CQRS.Users.Queries.Response;

namespace TaskTrackerApp.CQRS.Users.Handlers.QueryHandlers;

public sealed class GetUserByEmailQueryHandler
    : IRequestHandler<GetUserByEmailQueryRequest, ServiceResult<GetUserByEmailQueryResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByEmailQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResult<GetUserByEmailQueryResponse>> Handle(
        GetUserByEmailQueryRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
            return ServiceResult<GetUserByEmailQueryResponse>.Fail(ResultCode.NotFound, "User not found.");

        var response = _mapper.Map<GetUserByEmailQueryResponse>(user);

        return ServiceResult<GetUserByEmailQueryResponse>.Success(response);
    }
}
