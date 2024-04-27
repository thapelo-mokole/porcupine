using Microsoft.EntityFrameworkCore;
using Porcupine.Core.Entities;
using Porcupine.EntityFrameworkCore.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porcupine.EntityFrameworkCore.Repositories.GroupPermissions
{
    public class GroupPermissionRepository : IGroupPermissionRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<GroupPermission> _dbSet;

        public GroupPermissionRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<GroupPermission>();
        }

        public async Task AddRangeAsync(List<GroupPermission> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(List<GroupPermission> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GroupPermission>> GetByGroupAsync(Guid groupId)
        {
            return await _dbSet.Where(x => x.GroupId == groupId).ToListAsync();
        }

        public async Task UpdateRangeAsync(List<GroupPermission> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }
    }
}