using Porcupine.Application.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Porcupine.Application.Contracts.Models.Permissions.Dtos
{
    public class PermissionDto
    {
        public string ShortDescription { get; set; }
    }

    public class PermissionResponseDto : BaseResponseDto
    {
        public string ShortDescription { get; set; }
    }
}