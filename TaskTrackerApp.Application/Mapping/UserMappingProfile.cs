using AutoMapper;
using TaskTrackerApp.CQRS.Users.Commands.Request;
using TaskTrackerApp.CQRS.Users.Commands.Response;
using TaskTrackerApp.CQRS.Users.Queries.Response;
using TaskTrackerApp.Domain.Entities;

namespace TaskTrackerApp.Application.Common.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserCommandRequest, User>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

        CreateMap<UpdateUserCommandRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Trim().ToLowerInvariant()))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName.Trim()))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.Trim()))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Trim()));

        CreateMap<User, CreateUserCommandResponse>()
            .ForCtorParam("FullName", opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

        CreateMap<User, UpdateUserCommandResponse>()
                   .ForCtorParam("FullName", opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

        CreateMap<User, GetUserByIdQueryResponse>();

        CreateMap<User, GetAllUsersQueryResponse>();
    }
}
