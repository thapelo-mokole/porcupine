using AutoMapper;
using Porcupine.Application.Contracts.Models.Groups.Dtos;
using Porcupine.Application.Contracts.Models.Permissions.Dtos;
using Porcupine.Application.Contracts.Models.Utils;
using Porcupine.EntityFrameworkCore.Repositories.Groups;
using Porcupine.EntityFrameworkCore.Repositories.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porcupine.Application.Services
{
    public class LookupAppService : ILookupAppService
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly IPermissionRepository _permissionRepository;

        public LookupAppService(IMapper mapper,
            IGroupRepository groupRepository,
            IPermissionRepository permissionRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<GroupResponseDto>> GetAllGroupsAsync(CancellationToken cancellationToken = default)
        {
            var groups = await _groupRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GroupResponseDto>>(groups);
        }

        public async Task<IEnumerable<PermissionResponseDto>> GetAllPermissionsAsync(CancellationToken cancellationToken = default)
        {
            var permissions = await _permissionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PermissionResponseDto>>(permissions);
        }
    }
}