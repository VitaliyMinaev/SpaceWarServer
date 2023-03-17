using AutoMapper;
using SpaceWar.Api.Common;
using SpaceWar.Api.Contracts.v1.Requests;
using SpaceWar.Api.Contracts.v1.Responses;
using SpaceWar.Api.Domain;
using SpaceWar.Api.Entities;

namespace SpaceWar.Api.Helpers;

public class ApplicationMapper : Profile
{
    public ApplicationMapper()
    {
        /* CreateTaskRequest -> TaskDomain */
        CreateMap<CreateTaskRequest, TaskDomain>();
        /* UpdateTaskRequest -> TaskDomaim */
        CreateMap<UpdateTaskRequest, TaskDomain>();

        /* TaskDomain -> CreateTaskResponse */
        CreateMap<TaskDomain, CreateTaskResponse>();
        /* TaskDomain -> UpdateTaskResponse */
        CreateMap<TaskDomain, UpdateTaskResponse>();
        /* TaskDomain -> GetTaskResponse */
        CreateMap<TaskDomain, GetTaskResponse>();

        /* TaskDomain -> TaskEntity AND TaskEntity -> TaskDomain */
        CreateMap<TaskDomain, TaskEntity>().ReverseMap();

        /* AccountDomain -> AccountEntity */
        CreateMap<AccountDomain, AccountEntity>()
            .ForMember(dest =>
            dest.Roles,
            opt => opt.MapFrom(src => src.Roles.Select(x => new RoleEntity { Role=x })))
            .ReverseMap();

        /* AccountOperationsResult -> ChangeAccountDataResult */
        CreateMap<AccountOperationsResult, ChangeAccountDataResult>();
    }
}