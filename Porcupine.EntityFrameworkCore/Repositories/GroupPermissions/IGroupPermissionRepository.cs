using Microsoft.EntityFrameworkCore;
using Porcupine.Core.Entities;
using Porcupine.EntityFrameworkCore.EntityFrameworkCore;
using Porcupine.EntityFrameworkCore.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Porcupine.EntityFrameworkCore.Repositories.GroupPermissions
{
    public interface IGroupPermissionRepository
    {
        Task AddRangeAsync(List<GroupPermission> entities);

        Task UpdateRangeAsync(List<GroupPermission> entities);

        Task DeleteRangeAsync(List<GroupPermission> entities);

        Task<List<GroupPermission>> GetByGroupAsync(Guid groupId);
    }
}