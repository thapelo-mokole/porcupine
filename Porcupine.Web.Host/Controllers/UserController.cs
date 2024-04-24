using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Users;
using Porcupine.Application.Contracts.Models.Users.Dtos;

namespace Porcupine.Web.Host.Controllers
{
    public class UserController : ApiBaseController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync(CreateUpdateUserDto createUserDto)
        {
            return Ok(ApiResult<CreateUpdateUserResponseDto>.Success(await _userAppService.CreateAsync(createUserDto)));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, CreateUpdateUserDto createUpdateUserDto)
        {
            return Ok(ApiResult<CreateUpdateUserResponseDto>.Success(
                await _userAppService.UpdateAsync(id, createUpdateUserDto)));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok(ApiResult<BaseResponseDto>.Success(await _userAppService.DeleteAsync(id)));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(ApiResult<UserResponseDto>.Success(
                await _userAppService.GetByIdAsync(id)));
        }

        [HttpGet("GetAllList")]
        public async Task<IActionResult> GetAllListAsync()
        {
            return Ok(ApiResult<IEnumerable<UserResponseDto>>.Success(
                await _userAppService.GetAllListAsync()));
        }
    }
}