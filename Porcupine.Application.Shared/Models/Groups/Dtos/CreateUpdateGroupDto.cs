using System;
using System.Collections.Generic;
using System.Text;

namespace Porcupine.Application.Contracts.Models.Groups.Dtos
{
    public class CreateUpdateGroupDto
    {
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
    }
}