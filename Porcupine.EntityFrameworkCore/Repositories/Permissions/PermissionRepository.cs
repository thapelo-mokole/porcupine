using Microsoft.EntityFrameworkCore;
using Porcupine.Core.Entities;
using Porcupine.EntityFrameworkCore.EntityFrameworkCore;
using Porcupine.EntityFrameworkCore.Repositories.Base;
using System;

namespace Porcupine.EntityFrameworkCore.Repositories.Permissions
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(DatabaseContext context) : base(context)
        {
        }
    }
}