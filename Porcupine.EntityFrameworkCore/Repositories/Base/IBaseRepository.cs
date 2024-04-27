using Porcupine.Core.Common;
using System.Linq.Expressions;

namespace Porcupine.EntityFrameworkCore.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);
    }
}