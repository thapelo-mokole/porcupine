using Porcupine.Application.Contracts.Common;
using System.Collections.Generic;

namespace Porcupine.Application.Contracts.Models.Groups.Dtos
{
    public class CreateUpdateGroupDto
    {
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public List<string> Permissions { get; set; }
    }

    public class CreateUpdateGroupResponseDto : BaseResponseDto
    {
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
    }
}