using AutoMapper;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Request;
using TaskTrackerApp.Application.CQRS.TaskItems.Queries.Response;
using TaskTrackerApp.Domain.Entities;

namespace TaskTrackerApp.Application.Common.MappingProfiles;

public class TaskItemMappingProfile : Profile
{
    public TaskItemMappingProfile()
    {
        CreateMap<CreateTaskItemCommandRequest, TaskItem>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src =>
                string.IsNullOrWhiteSpace(src.Title)
                    ? string.Empty
                    : src.Title.Trim().ToLowerInvariant()
            ));

        CreateMap<UpdateTaskItemCommandRequest, TaskItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DueDate, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore());

        CreateMap<TaskItem, GetTaskItemByIdQueryResponse>();

        CreateMap<TaskItem, GetAllTasksQueryResponse>();

        CreateMap<TaskItem, GetTasksByUserIdQueryResponse>();

        CreateMap<TaskItem, GetRecentlyCompletedTasksQueryResponse>();
    }
}
