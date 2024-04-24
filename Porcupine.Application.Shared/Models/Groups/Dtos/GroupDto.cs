﻿using Porcupine.Application.Contracts.Common;
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
    }
}