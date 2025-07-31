using AutoMapper;
using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.CQRS.Users.Queries.Request;
using TaskTrackerApp.CQRS.Users.Queries.Response;

namespace TaskTrackerApp.CQRS.Users.Handlers.QueryHandlers;

public sealed class GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        : IRequestHandler<GetAllUsersQueryRequest, ServiceResult<List<GetAllUsersQueryResponse>>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ServiceResult<List<GetAllUsersQueryResponse>>> Handle(
        GetAllUsersQueryRequest request,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsersAsync();

        var response = _mapper.Map<List<GetAllUsersQueryResponse>>(users);

        return ServiceResult<List<GetAllUsersQueryResponse>>.Success(response);
    }
}
