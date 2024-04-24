using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Groups.Dtos;
using Porcupine.Application.Contracts.Models.Groups;

namespace Porcupine.Web.Host.Controllers
{
    public class GroupController : ApiBaseController
    {
        private readonly IGroupAppService _groupAppService;

        public GroupController(IGroupAppService groupAppService)
        {
            _groupAppService = groupAppService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync(CreateUpdateGroupDto createGroupDto)
        {
            return Ok(ApiResult<CreateUpdateGroupResponseDto>.Success(await _groupAppService.CreateAsync(createGroupDto)));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, CreateUpdateGroupDto createUpdateGroupDto)
        {
            return Ok(ApiResult<CreateUpdateGroupResponseDto>.Success(await _groupAppService.UpdateAsync(id, createUpdateGroupDto)));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok(ApiResult<BaseResponseDto>.Success(await _groupAppService.DeleteAsync(id)));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(ApiResult<GroupResponseDto>.Success(await _groupAppService.GetByIdAsync(id)));
        }

        [HttpGet("GetAllList")]
        public async Task<IActionResult> GetAllListAsync()
        {
            return Ok(ApiResult<IEnumerable<GroupResponseDto>>.Success(await _groupAppService.GetAllListAsync()));
        }
    }
}