using Microsoft.AspNetCore.Mvc;
using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Groups.Dtos;
using Porcupine.Application.Contracts.Models.Permissions.Dtos;
using Porcupine.Application.Contracts.Models.Utils;

namespace Porcupine.Web.Host.Controllers
{
    public class LookupController : ApiBaseController
    {
        private readonly ILookupAppService _lookupAppService;

        public LookupController(ILookupAppService lookupAppService)
        {
            _lookupAppService = lookupAppService;
        }

        [HttpGet("GetAllPermissions")]
        public async Task<IActionResult> GetAllPermissionsAsync()
        {
            return Ok(ApiResult<IEnumerable<PermissionResponseDto>>.Success(await _lookupAppService.GetAllPermissionsAsync()));
        }

        [HttpGet("GetAllGroups")]
        public async Task<IActionResult> GetAllGroupsAsync()
        {
            return Ok(ApiResult<IEnumerable<GroupResponseDto>>.Success(await _lookupAppService.GetAllGroupsAsync()));
        }
    }
}