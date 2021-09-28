using Ineval.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RP.DAL.Repository
{
    public interface IRepository<in TKey, TEntity> : IDisposable
                    where TEntity : BaseEntityClass<TKey>

    {
        IUnitOfWork UnitOfWork { get; set; }
        TEntity GetById(TKey id);
        Task<TEntity> GetByIdAsync(TKey id);
        IQueryable<TEntity> GetAll();
        Task<IQueryable<TEntity>> GetAllAsync();

        IQueryable<TEntity> GetAllDelete();
        Task<IQueryable<TEntity>> GetAllDeleteAsync();
        SaveResult Save(TEntity entity);
        Task<SaveResult> SaveAsync(TEntity entity);
        SaveResult Create(TEntity entity);
        Task<SaveResult> CreateAsync(TEntity entity);
        SaveResult CreateRange(IEnumerable<TEntity> entities);
        Task<SaveResult> CreateRangeAsync(IEnumerable<TEntity> entities);
        SaveResult Update(TEntity entity);
        Task<SaveResult> UpdateAsync(TEntity entity);
        SaveResult Delete(TEntity entity);
        Task<SaveResult> DeleteAsync(TEntity entity);
        SaveResult Delete(TKey id);
        Task<SaveResult> DeleteAsync(TKey id);
        SaveResult DeleteRange(IEnumerable<TEntity> entities);
        Task<SaveResult> DeleteRangeAsync(IEnumerable<TEntity> entities);
        TEntity FirstOrDefault();
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync();
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        bool Contains(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate);
        int Count(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        Task<IQueryable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
