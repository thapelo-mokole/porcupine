using AutoMapper;
using Porcupine.Application.Contracts.Models.Groups.Dtos;
using Porcupine.Application.Contracts.Models.Permissions.Dtos;
using Porcupine.Application.Contracts.Models.Users.Dtos;
using Porcupine.Core.Entities;

namespace Porcupine.Application
{
    public class ApplicationAutoMapperProfile : Profile
    {
        public ApplicationAutoMapperProfile()
        {
            CreateMap<CreateUpdateUserDto, User>();
            CreateMap<User, UserResponseDto>();

            CreateMap<CreateUpdateGroupDto, Group>();
            CreateMap<Group, GroupResponseDto>();

            CreateMap<CreateUpdatePermissionDto, Permission>();
            CreateMap<Permission, PermissionResponseDto>();
        }
    }
}