using Porcupine.Application.Contracts.Models.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Porcupine.Application.Contracts.Models.Permissions.Dtos;
using Porcupine.Application.Contracts.Models.Groups.Dtos;

namespace Porcupine.Application.Contracts.Models.Utils
{
    public interface ILookupAppService
    {
        Task<IEnumerable<PermissionResponseDto>> GetAllPermissionsAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<GroupResponseDto>> GetAllGroupsAsync(CancellationToken cancellationToken = default);
    }
}