using RP.DAL.Repository;
using Ineval.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ineval.BO
{
    public class EntityService<TEntity> : IEntityService<Guid, TEntity> where TEntity : BaseEntity
    {
        public IUnitOfWork UnitOfWork { get; set; }
        private Repository<TEntity> Repository { get; set; }

        protected EntityService() : this(new UnitOfWork())
        {
        }

        protected EntityService(IUnitOfWork unitOfWork)
        {
            Repository = new Repository<TEntity>(unitOfWork);
            UnitOfWork = unitOfWork;
        }

        public virtual void Dispose()
        {
            if (Repository != null)
            {
                Repository.Dispose();
            }
            if (UnitOfWork != null)
            {
                UnitOfWork.Dispose();
            }
        }

        public virtual TEntity GetById(Guid id)
        {
            return Repository.GetById(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await Repository.GetByIdAsync(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Repository.GetAll();
        }

        public virtual async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Repository.GetAllAsync();
        }

        public virtual SaveResult Save(TEntity entity)
        {
            return Repository.Save(entity);
        }

        public virtual async Task<SaveResult> SaveAsync(TEntity entity)
        {
            return await Repository.SaveAsync(entity);
        }

        public virtual SaveResult Create(TEntity entity)
        {
            return Repository.Create(entity);
        }

        public virtual async Task<SaveResult> CreateAsync(TEntity entity)
        {
            return await Repository.CreateAsync(entity);
        }

        public virtual SaveResult CreateRange(IEnumerable<TEntity> entities)
        {
            return Repository.CreateRange(entities);
        }

        public virtual async Task<SaveResult> CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            return await Repository.CreateRangeAsync(entities);
        }

        public virtual SaveResult Update(TEntity entity)
        {
            return Repository.Update(entity);
        }

        public virtual async Task<SaveResult> UpdateAsync(TEntity entity)
        {
            return await Repository.UpdateAsync(entity);
        }

        public virtual SaveResult Delete(TEntity entity)
        {
            return Repository.Delete(entity);
        }

        public virtual async Task<SaveResult> DeleteAsync(TEntity entity)
        {
            return await Repository.DeleteAsync(entity);
        }

        public SaveResult Delete(Guid id)
        {
            return Repository.Delete(id);
        }

        public virtual async Task<SaveResult> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync(id);
        }

        public virtual TEntity FirstOrDefault()
        {
            return Repository.FirstOrDefault();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.FirstOrDefault(predicate);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync()
        {
            return await Repository.FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Repository.FirstOrDefaultAsync(predicate);
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Where(predicate);
        }

        public virtual async Task<IQueryable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Repository.WhereAsync(predicate);
        }

        public virtual bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Contains(predicate);
        }

        public virtual async Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Repository.ContainsAsync(predicate);
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Count(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Repository.CountAsync(predicate);
        }

        public virtual SaveResult DeleteRange(IEnumerable<TEntity> entities)
        {
            return Repository.DeleteRange(entities);
        }

        public virtual async Task<SaveResult> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            return await Repository.DeleteRangeAsync(entities);
        }
    }
}
