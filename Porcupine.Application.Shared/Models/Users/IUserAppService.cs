using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Porcupine.Application.Contracts.Models.Users
{
    public interface IUserAppService
    {
        //Task<LoginResponseDto> LoginAsync(LoginUserDto loginUserModel);

        Task<CreateUpdateUserResponseDto> CreateAsync(CreateUpdateUserDto createUserModel);
    }
}