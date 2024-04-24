using Porcupine.Application.Contracts.Common;

namespace Porcupine.Application.Contracts.Models.Permissions.Dtos
{
    public class CreateUpdatePermissionDto
    {
        public string ShortDescription { get; set; }
    }

    public class CreateUpdatePermissionResponseDto : BaseResponseDto
    {
        public string ShortDescription { get; set; }
    }
}