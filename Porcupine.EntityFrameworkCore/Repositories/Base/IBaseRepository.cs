using Microsoft.EntityFrameworkCore;
using Porcupine.Core.Common;
using System.Linq.Expressions;

namespace Porcupine.EntityFrameworkCore.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetAllWithDetailsAsync(params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        Task<bool> IsDescriptionUnique(string description);

        Task<bool> IsEmailUnique(string email);

        Task<bool> IsUsernameUnique(string userName);
    }
}