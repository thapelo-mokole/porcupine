using Porcupine.Application.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Porcupine.Application.Contracts.Models.Users.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; }
    }

    public class UserResponseDto : BaseResponseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}