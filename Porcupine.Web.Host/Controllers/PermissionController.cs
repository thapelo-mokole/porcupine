using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Permissions.Dtos;
using Porcupine.Application.Contracts.Models.Permissions;

namespace Porcupine.Web.Host.Controllers
{
    public class PermissionController : ApiBaseController
    {
        private readonly IPermissionAppService _permissionAppService;

        public PermissionController(IPermissionAppService permissionAppService)
        {
            _permissionAppService = permissionAppService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync(CreateUpdatePermissionDto createPermissionDto)
        {
            return Ok(ApiResult<CreateUpdatePermissionResponseDto>.Success(await _permissionAppService.CreateAsync(createPermissionDto)));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, CreateUpdatePermissionDto createUpdatePermissionDto)
        {
            return Ok(ApiResult<CreateUpdatePermissionResponseDto>.Success(
                await _permissionAppService.UpdateAsync(id, createUpdatePermissionDto)));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok(ApiResult<BaseResponseDto>.Success(await _permissionAppService.DeleteAsync(id)));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(ApiResult<PermissionResponseDto>.Success(await _permissionAppService.GetByIdAsync(id)));
        }

        [HttpGet("GetAllList")]
        public async Task<IActionResult> GetAllListAsync()
        {
            return Ok(ApiResult<IEnumerable<PermissionResponseDto>>.Success(await _permissionAppService.GetAllListAsync()));
        }
    }
}