using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Permissions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Porcupine.Application.Contracts.Models.Permissions
{
    public interface IPermissionAppService
    {
        Task<CreateUpdatePermissionResponseDto> CreateAsync(CreateUpdatePermissionDto createPermissionDto);

        Task<BaseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PermissionResponseDto>
            GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<PermissionResponseDto>>
            GetAllListAsync(CancellationToken cancellationToken = default);

        Task<CreateUpdatePermissionResponseDto> UpdateAsync(Guid id, CreateUpdatePermissionDto updateCreatePermissionDto,
            CancellationToken cancellationToken = default);
    }
}