using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Groups.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Porcupine.Application.Contracts.Models.Groups
{
    public interface IGroupAppService
    {
        Task<CreateUpdateGroupResponseDto> CreateAsync(CreateUpdateGroupDto createGroupDto);

        Task<BaseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<GroupResponseDto>
            GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<GroupResponseDto>>
            GetAllListAsync(CancellationToken cancellationToken = default);

        Task<CreateUpdateGroupResponseDto> UpdateAsync(Guid id, CreateUpdateGroupDto updateCreateGroupDto,
            CancellationToken cancellationToken = default);
    }
}