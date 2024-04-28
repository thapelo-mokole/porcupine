using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Groups.Dtos;
using Porcupine.Application.Contracts.Models.Users;
using Porcupine.Application.Contracts.Models.Users.Dtos;
using Porcupine.Core.Entities;
using Porcupine.EntityFrameworkCore.EntityFrameworkCore;
using Porcupine.EntityFrameworkCore.Repositories.GroupPermissions;
using Porcupine.EntityFrameworkCore.Repositories.UserGroups;
using Porcupine.EntityFrameworkCore.Repositories.Users;

namespace Porcupine.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;

        private readonly DatabaseContext _context;

        public UserAppService(
            IMapper mapper,
            DatabaseContext context,
            IUserRepository userRepository,
            IUserGroupRepository userGroupRepository)
        {
            _mapper = mapper;
            _context = context;
            _userRepository = userRepository;
            _userGroupRepository = userGroupRepository;
        }

        public async Task<CreateUpdateUserResponseDto> CreateAsync(CreateUpdateUserDto createUserDto)
        {
            var results = new CreateUpdateUserResponseDto();

            try
            {
                await _userRepository.IsEmailUnique(createUserDto.Email);
                await _userRepository.IsUsernameUnique(createUserDto.UserName);

                User user = new()
                {
                    UserName = createUserDto.UserName.Trim(),
                    Name = createUserDto.Name,
                    Surname = createUserDto.Surname,
                    Email = createUserDto.Email,
                    NormalizedEmail = createUserDto.Email.ToUpper().Trim(),
                    EmailConfirmed = createUserDto.EmailConfirmed
                };

                var result = await _userRepository.AddAsync(user);

                List<UserGroup> userGroups = createUserDto.Groups.Select(group => new UserGroup
                {
                    GroupId = new Guid(group),
                    UserId = result.Id
                }).ToList();

                await _userGroupRepository.AddRangeAsync(userGroups);

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
            var user = await _userRepository.GetFirstAsync(x => x.Id == id);

            return new BaseResponseDto
            {
                Id = (await _userRepository.DeleteAsync(user)).Id
            };
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllListAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetAllWithDetailsAsync(includes: x => x.Groups);
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
            var results = new CreateUpdateUserResponseDto();

            try
            {
                var user = await _userRepository.GetFirstAsync(x => x.Id == id);
                user.Name = createUpdateUserDto.Name;
                user.Surname = createUpdateUserDto.Surname;
                user.Email = createUpdateUserDto.Email;
                user.NormalizedEmail = createUpdateUserDto.Email.ToUpper().Trim();
                user.EmailConfirmed = createUpdateUserDto.EmailConfirmed;

                var result = await _userRepository.UpdateAsync(user);

                // Remove existing mappings
                var currentLinks = await _userGroupRepository.GetByUserAsync(id);
                await _userGroupRepository.DeleteRangeAsync(currentLinks);

                // Add new mapppings
                List<UserGroup> userGroups = createUpdateUserDto.Groups.Select(group => new UserGroup
                {
                    GroupId = new Guid(group),
                    UserId = result.Id //
                }).ToList();

                await _userGroupRepository.AddRangeAsync(userGroups);

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