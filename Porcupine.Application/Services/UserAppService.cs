using AutoMapper;
using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Users;
using Porcupine.Application.Contracts.Models.Users.Dtos;
using Porcupine.Core.Entities;
using Porcupine.EntityFrameworkCore.Repositories.Users;

namespace Porcupine.Application.Services
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

        public async Task<BaseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetFirstAsync(x => x.Id == id);

            return new BaseResponseDto
            {
                Id = (await _userRepository.DeleteAsync(user)).Id
            };
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllListAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetFirstAsync(x => x.Id == id);

            if (user == null)
                return new UserResponseDto();

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<CreateUpdateUserResponseDto> UpdateAsync(Guid id, CreateUpdateUserDto createUpdateUserDto, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetFirstAsync(x => x.Id == id);

            _mapper.Map(createUpdateUserDto, user);

            return new CreateUpdateUserResponseDto
            {
                Id = (await _userRepository.UpdateAsync(user)).Id
            };
        }
    }
}