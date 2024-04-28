using Microsoft.EntityFrameworkCore;
using Porcupine.Core.Common;
using Porcupine.Core.Exceptions;
using Porcupine.EntityFrameworkCore.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Porcupine.EntityFrameworkCore.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DatabaseContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(DatabaseContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = (await DbSet.AddAsync(entity)).Entity;
            await Context.SaveChangesAsync();

            return addedEntity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            var removedEntity = DbSet.Remove(entity).Entity;
            await Context.SaveChangesAsync();

            return removedEntity;
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllWithDetailsAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;

            // Apply includes
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await DbSet.Where(predicate).FirstOrDefaultAsync();

            if (entity == null) throw new ResourceNotFoundException(typeof(TEntity));

            return await DbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;

            // Apply includes
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var entity = await query.Where(predicate).FirstOrDefaultAsync();

            if (entity == null) throw new ResourceNotFoundException(typeof(TEntity));

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> IsDescriptionUnique(string shortDescription)
        {
            if (await DbSet.AnyAsync(e => EF.Property<string>(e, "ShortDescription") == shortDescription))
            {
                throw new InvalidOperationException("A record with the same ShortDescription already exists.");
            }

            return true;
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            if (await DbSet.AnyAsync(e => EF.Property<string>(e, "Email") == email))
            {
                throw new InvalidOperationException("A record with the same Email already exists.");
            }

            return true;
        }

        public async Task<bool> IsUsernameUnique(string userName)
        {
            if (await DbSet.AnyAsync(e => EF.Property<string>(e, "UserName") == userName))
            {
                throw new InvalidOperationException("A record with the same UserName already exists.");
            }

            return true;
        }
    }
}