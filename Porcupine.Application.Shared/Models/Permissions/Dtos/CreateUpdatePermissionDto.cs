﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Porcupine.Application.Contracts.Models.Permissions.Dtos
{
    public class CreateUpdatePermissionDto
    {
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
    }
}