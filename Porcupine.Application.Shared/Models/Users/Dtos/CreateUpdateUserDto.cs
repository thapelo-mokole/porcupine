using Porcupine.Application.Contracts.Common;
using System.Collections.Generic;

namespace Porcupine.Application.Contracts.Models.Users.Dtos
{
    public class CreateUpdateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<string> Groups { get; set; }
    }

    public class CreateUpdateUserResponseDto : BaseResponseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}