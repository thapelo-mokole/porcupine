using AutoMapper;
using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Permissions;
using Porcupine.Application.Contracts.Models.Permissions.Dtos;
using Porcupine.Core.Entities;
using Porcupine.EntityFrameworkCore.Repositories.Permissions;

namespace Porcupine.Application.Services
{
    public class PermissionAppService : IPermissionAppService
    {
        private readonly IMapper _mapper;
        private readonly IPermissionRepository _permissionRepository;

        public PermissionAppService(IMapper mapper,
            IPermissionRepository permissionRepository)
        {
            _mapper = mapper;
            _permissionRepository = permissionRepository;
        }

        public async Task<CreateUpdatePermissionResponseDto> CreateAsync(CreateUpdatePermissionDto createPermissionDto)
        {
            await _permissionRepository.IsDescriptionUnique(createPermissionDto.ShortDescription);

            var permission = _mapper.Map<Permission>(createPermissionDto);

            var result = await _permissionRepository.AddAsync(permission);

            return new CreateUpdatePermissionResponseDto
            {
                Id = result.Id
            };
        }

        public async Task<BaseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var permission = await _permissionRepository.GetFirstAsync(x => x.Id == id, includes: x => x.Groups);

            if (permission.Groups != null)
            {
                throw new InvalidOperationException("Cannot delete the child (permission) while linked to a parent (group).");
            }

            return new BaseResponseDto
            {
                Id = (await _permissionRepository.DeleteAsync(permission)).Id
            };
        }

        public async Task<IEnumerable<PermissionResponseDto>> GetAllListAsync(CancellationToken cancellationToken = default)
        {
            var permissions = await _permissionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PermissionResponseDto>>(permissions);
        }

        public async Task<PermissionResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var permission = await _permissionRepository.GetFirstAsync(x => x.Id == id);

            if (permission == null)
                return new PermissionResponseDto();

            return _mapper.Map<PermissionResponseDto>(permission);
        }

        public async Task<CreateUpdatePermissionResponseDto> UpdateAsync(Guid id, CreateUpdatePermissionDto createUpdatePermissionDto, CancellationToken cancellationToken = default)
        {
            var permission = await _permissionRepository.GetFirstAsync(x => x.Id == id);

            _mapper.Map(createUpdatePermissionDto, permission);

            return new CreateUpdatePermissionResponseDto
            {
                Id = (await _permissionRepository.UpdateAsync(permission)).Id
            };
        }
    }
}