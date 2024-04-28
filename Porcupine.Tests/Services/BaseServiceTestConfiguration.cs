using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Porcupine.Application;
using Porcupine.Core.Shared.Utils.Interface;
using Porcupine.EntityFrameworkCore.Repositories.GroupPermissions;
using Porcupine.EntityFrameworkCore.Repositories.Groups;
using Porcupine.EntityFrameworkCore.Repositories.Permissions;
using Porcupine.EntityFrameworkCore.Repositories.UserGroups;
using Porcupine.EntityFrameworkCore.Repositories.Users;

namespace Porcupine.UnitTests.Services
{
    public class BaseServiceTestConfiguration
    {
        protected readonly IUtilityService UtilityService;
        protected readonly IConfiguration Configuration;
        protected readonly IMapper Mapper;

        protected readonly IPermissionRepository PermissionRepository;
        protected readonly IGroupRepository GroupRepository;
        protected readonly IUserRepository UserRepository;

        protected readonly IGroupPermissionRepository GroupPermissionRepository;
        protected readonly IUserGroupRepository UserGroupRepository;

        protected BaseServiceTestConfiguration()
        {
            Mapper = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(ApplicationAutoMapperProfile)); }).CreateMapper();

            var configurationBody = new Dictionary<string, string>
        {
            { "JwtConfiguration:SecretKey", "Super secret token key" }
        };

            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configurationBody)
                .Build();

            PermissionRepository = Substitute.For<IPermissionRepository>();
            GroupRepository = Substitute.For<IGroupRepository>();
            UserRepository = Substitute.For<IUserRepository>();
            GroupPermissionRepository = Substitute.For<IGroupPermissionRepository>();
            UserGroupRepository = Substitute.For<IUserGroupRepository>();

            UtilityService = Substitute.For<IUtilityService>();
            UtilityService.GetUserId().Returns(new Guid().ToString());
        }
    }
}