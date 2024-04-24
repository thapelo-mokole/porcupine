using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Porcupine.Application.Contracts.Models.Users;
using Porcupine.Application.Contracts.Models.Users.Dtos;
using Porcupine.Application.Exceptions;
using Porcupine.Core.Entities;
using Porcupine.EntityFrameworkCore.Repositories.Users;

namespace Porcupine.Application.Models.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserAppService(IMapper mapper,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<CreateUpdateUserResponseDto> CreateAsync(CreateUpdateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);

            var result = await _userRepository.AddAsync(user);

            return new CreateUpdateUserResponseDto
            {
                Id = result.Id
            };
        }
    }
}