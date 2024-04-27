using Porcupine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porcupine.EntityFrameworkCore.Repositories.UserGroups
{
    public interface IUserGroupRepository
    {
        Task AddRangeAsync(List<UserGroup> entities);

        Task UpdateRangeAsync(List<UserGroup> entities);

        Task DeleteRangeAsync(List<UserGroup> entities);

        Task<List<UserGroup>> GetByUserAsync(Guid userId);
    }
}