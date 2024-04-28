using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Groups;
using Porcupine.Application.Contracts.Models.Groups.Dtos;

using Porcupine.Core.Entities;
using Porcupine.EntityFrameworkCore.EntityFrameworkCore;
using Porcupine.EntityFrameworkCore.Repositories.GroupPermissions;
using Porcupine.EntityFrameworkCore.Repositories.Groups;
using Porcupine.EntityFrameworkCore.Repositories.Permissions;

namespace Porcupine.Application.Services
{
    public class GroupAppService : IGroupAppService
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupPermissionRepository _groupPermissionRepository;

        public GroupAppService(IMapper mapper,
            IGroupRepository groupRepository,
            IGroupPermissionRepository groupPermissionRepository
            )
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
            _groupPermissionRepository = groupPermissionRepository;
        }

        public async Task<CreateUpdateGroupResponseDto> CreateAsync(CreateUpdateGroupDto createGroupDto)
        {
            var results = new CreateUpdateGroupResponseDto();

            try
            {
                await _groupRepository.IsDescriptionUnique(createGroupDto.ShortDescription);

                Group group = new()
                {
                    ShortDescription = createGroupDto.ShortDescription,
                    LongDescription = createGroupDto.LongDescription
                };

                var result = await _groupRepository.AddAsync(group);

                List<GroupPermission> groupPermissions = createGroupDto.Permissions.Select(group => new GroupPermission
                {
                    PermissionId = new Guid(group),
                    GroupId = result.Id
                }).ToList();

                await _groupPermissionRepository.AddRangeAsync(groupPermissions);

                results.Id = result.Id;
            }
            catch (Exception)
            {
                // Handle the exception by logging it, but for the prupose of assessment, just throw
                throw;
            }

            return results;
        }

        public async Task<BaseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var group = await _groupRepository.GetFirstAsync(x => x.Id == id, includes: x => x.Users);

            if (group.Users != null)
            {
                throw new InvalidOperationException("Cannot delete the child (group) while linked to a parent(user).");
            }

            return new BaseResponseDto
            {
                Id = (await _groupRepository.DeleteAsync(group)).Id
            };
        }

        public async Task<IEnumerable<GroupResponseDto>> GetAllListAsync(CancellationToken cancellationToken = default)
        {
            var groups = await _groupRepository.GetAllWithDetailsAsync(x => x.Permissions, x => x.Users);
            return _mapper.Map<IEnumerable<GroupResponseDto>>(groups);
        }

        public async Task<GroupResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var group = await _groupRepository.GetFirstAsync(x => x.Id == id);

            if (group == null)
                return new GroupResponseDto();

            return _mapper.Map<GroupResponseDto>(group);
        }

        public async Task<CreateUpdateGroupResponseDto> UpdateAsync(Guid id, CreateUpdateGroupDto createUpdateGroupDto, CancellationToken cancellationToken = default)
        {
            var results = new CreateUpdateGroupResponseDto();

            try
            {
                var group = await _groupRepository.GetFirstAsync(x => x.Id == id);
                group.ShortDescription = createUpdateGroupDto.ShortDescription;
                group.LongDescription = createUpdateGroupDto.LongDescription;

                var result = await _groupRepository.UpdateAsync(group);

                // Remove existing mappings
                var currentLinks = await _groupPermissionRepository.GetByGroupAsync(id);
                await _groupPermissionRepository.DeleteRangeAsync(currentLinks);

                // Add new mappings
                List<GroupPermission> groupPermissions = createUpdateGroupDto.Permissions.Select(group => new GroupPermission
                {
                    PermissionId = new Guid(group),
                    GroupId = result.Id
                }).ToList();

                await _groupPermissionRepository.AddRangeAsync(groupPermissions);

                results.Id = result.Id;
            }
            catch (Exception)
            {
                // Handle the exception by logging it, but for the prupose of assessment, just throw
                throw;
            }

            return results;
        }
    }
}