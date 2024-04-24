using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Users;
using Porcupine.Application.Contracts.Models.Users.Dtos;

namespace Porcupine.Web.Host.Controllers
{
    public class UsersController : ApiBaseController
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(CreateUpdateUserDto createUserDto)
        {
            return Ok(ApiResult<CreateUpdateUserResponseDto>.Success(await _userAppService.CreateAsync(createUserDto)));
        }
    }
}