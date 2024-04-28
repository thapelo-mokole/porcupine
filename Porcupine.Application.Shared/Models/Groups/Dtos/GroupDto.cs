using Porcupine.Application.Contracts.Common;
using Porcupine.Application.Contracts.Models.Permissions.Dtos;
using Porcupine.Application.Contracts.Models.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Porcupine.Application.Contracts.Models.Groups.Dtos
{
    public class GroupDto
    {
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
    }

    public class GroupResponseDto : BaseResponseDto
    {
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public List<PermissionResponseDto> Permissions { get; set; }
        public List<UserResponseDto> Users { get; set; }
    }
}