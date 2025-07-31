using MediatR;
using TaskTrackerApp.Application.Common.Results;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Commands.Request;

public sealed record CompleteTaskItemCommandRequest(Guid Id) : IRequest<ServiceResult>;
