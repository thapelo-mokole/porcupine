using Microsoft.EntityFrameworkCore;
using Porcupine.Core.Entities;
using Porcupine.EntityFrameworkCore.EntityFrameworkCore;

namespace Porcupine.EntityFrameworkCore.Repositories.UserGroups
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<UserGroup> _dbSet;

        public UserGroupRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<UserGroup>();
        }

        public async Task AddRangeAsync(List<UserGroup> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(List<UserGroup> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserGroup>> GetByUserAsync(Guid userId)
        {
            return await _dbSet.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task UpdateRangeAsync(List<UserGroup> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }
    }
}