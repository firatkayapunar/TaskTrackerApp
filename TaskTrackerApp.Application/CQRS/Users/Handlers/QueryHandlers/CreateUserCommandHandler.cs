using AutoMapper;
using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.CQRS.Users.Queries.Request;
using TaskTrackerApp.CQRS.Users.Queries.Response;

namespace TaskTrackerApp.CQRS.Users.Handlers.QueryHandlers;

public sealed class GetUserByIdQueryHandler
    : IRequestHandler<GetUserByIdQueryRequest, ServiceResult<GetUserByIdQueryResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResult<GetUserByIdQueryResponse>> Handle(
        GetUserByIdQueryRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
            return ServiceResult<GetUserByIdQueryResponse>.Fail(ResultCode.NotFound, $"User with ID '{request.Id}' not found.");

        var response = _mapper.Map<GetUserByIdQueryResponse>(user);

        return ServiceResult<GetUserByIdQueryResponse>.Success(response);
    }
}
