using AutoMapper;
using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Groups;
using Porcupine.Application.Contracts.Models.Groups.Dtos;

using Porcupine.Core.Entities;
using Porcupine.EntityFrameworkCore.Repositories.Groups;

namespace Porcupine.Application.Services
{
    public class GroupAppService : IGroupAppService
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public GroupAppService(IMapper mapper,
            IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<CreateUpdateGroupResponseDto> CreateAsync(CreateUpdateGroupDto createGroupDto)
        {
            var group = _mapper.Map<Group>(createGroupDto);

            var result = await _groupRepository.AddAsync(group);

            return new CreateUpdateGroupResponseDto
            {
                Id = result.Id
            };
        }

        public async Task<BaseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var group = await _groupRepository.GetFirstAsync(x => x.Id == id);

            return new BaseResponseDto
            {
                Id = (await _groupRepository.DeleteAsync(group)).Id
            };
        }

        public async Task<IEnumerable<GroupResponseDto>> GetAllListAsync(CancellationToken cancellationToken = default)
        {
            var groups = await _groupRepository.GetAllAsync();
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
            var group = await _groupRepository.GetFirstAsync(x => x.Id == id);

            _mapper.Map(createUpdateGroupDto, group);

            return new CreateUpdateGroupResponseDto
            {
                Id = (await _groupRepository.UpdateAsync(group)).Id
            };
        }
    }
}