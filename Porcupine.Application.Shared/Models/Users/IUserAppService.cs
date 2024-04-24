using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Porcupine.Application.Contracts.Models.Users
{
    public interface IUserAppService
    {
        Task<CreateUpdateUserResponseDto> CreateAsync(CreateUpdateUserDto createUserDto);

        Task<BaseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<UserResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<UserResponseDto>> GetAllListAsync(CancellationToken cancellationToken = default);

        Task<CreateUpdateUserResponseDto> UpdateAsync(Guid id, CreateUpdateUserDto createUpdateUserDto, CancellationToken cancellationToken = default);
    }
}