using Ineval.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RP.DAL.Repository
{
    public class Repository<TEntity> : IRepository<Guid, TEntity>
                where TEntity : BaseEntity
    {
        private DbSet<TEntity> DbSet { get { return UnitOfWork.Db.Set<TEntity>(); } }

        //public Repository()
        //{
        //    UnitOfWork = new UnitOfWork();x
        //}

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        ~Repository()
        {
            Dispose();
        }

        public IUnitOfWork UnitOfWork { get; set; }

        public virtual TEntity GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return GetAll().FirstOrDefault(r => r.Id.Equals(id));
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return await Task.Run(() => GetAll().FirstOrDefaultAsync(r => r.Id.Equals(id)));
        }

        public virtual IQueryable<TEntity> GetAllDelete()
        {
            return DbSet.Where(t => t.Estado == EstadoEnum.Eliminado);
        }

        public virtual async Task<IQueryable<TEntity>> GetAllDeleteAsync()
        {
            var result = GetAllDelete();
            return await Task.Run(() => result);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet.Where(t => t.Estado == EstadoEnum.Activo);
        }

        public virtual async Task<IQueryable<TEntity>> GetAllAsync()
        {
            var result = GetAll();
            return await Task.Run(() => result);
        }

        public virtual SaveResult Save(TEntity entity)
        {
            return entity.IsNew() ? Create(entity) : Update(entity);
        }

        public virtual async Task<SaveResult> SaveAsync(TEntity entity)
        {
            if (entity.IsNew())
            {
                return await CreateAsync(entity);
            }
            return await UpdateAsync(entity);
        }

        public virtual SaveResult Create(TEntity entity)
        {
            try
            {
                entity.Estado = EstadoEnum.Activo;
                entity.FechaCreacion = DateTime.Now;

                DbSet.Add(entity);

                if (!UnitOfWork.IsInTransaccion)
                {
                    UnitOfWork.Db.SaveChanges();
                }

                return SaveResult.Success(entity);
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
        }

        public virtual async Task<SaveResult> CreateAsync(TEntity entity)
        {
            try
            {
                entity.Estado = EstadoEnum.Activo;
                entity.FechaCreacion = DateTime.Now;

                DbSet.Add(entity);
                if (!UnitOfWork.IsInTransaccion)
                {
                    await UnitOfWork.Db.SaveChangesAsync();
                }

                return SaveResult.Success(entity);
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
        }

        public virtual SaveResult CreateRange(IEnumerable<TEntity> entities)
        {
            try
            {
                var enumerable = entities as TEntity[] ?? entities.ToArray();
                foreach (var entity in enumerable)
                {
                    entity.Estado = EstadoEnum.Activo;
                    entity.FechaCreacion = DateTime.Now;
                }

                DbSet.AddRange(enumerable);

                if (!UnitOfWork.IsInTransaccion)
                {
                    UnitOfWork.Db.SaveChanges();
                }

                return SaveResult.Success();
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
        }

        public virtual async Task<SaveResult> CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                var enumerable = entities as TEntity[] ?? entities.ToArray();
                foreach (var entity in enumerable)
                {
                    entity.Estado = EstadoEnum.Activo;
                    entity.FechaCreacion = DateTime.Now;
                }

                DbSet.AddRange(enumerable);
                if (!UnitOfWork.IsInTransaccion)
                {
                    await UnitOfWork.Db.SaveChangesAsync();
                }

                return SaveResult.Success();
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
        }

        public virtual SaveResult Update(TEntity entity)
        {
            try
            {
                if (entity.IsNew())
                {
                    throw new Exception("No se puede grabar actualizar una entidad nueva");
                }

                entity.FechaModificacion = DateTime.Now;
                DbSet.Attach(entity);
                UnitOfWork.Db.Entry(entity).State = EntityState.Modified;

                if (!UnitOfWork.IsInTransaccion)
                {
                    UnitOfWork.SaveChanges();
                }
                return SaveResult.Success();
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
        }

        public virtual async Task<SaveResult> UpdateAsync(TEntity entity)
        {
            try
            {
                if (entity.IsNew())
                {
                    throw new Exception("No se puede grabar actualizar una entidad nueva");
                }
                entity.FechaModificacion = DateTime.Now;
                DbSet.Attach(entity);
                UnitOfWork.Db.Entry(entity).State = EntityState.Modified;

                if (!UnitOfWork.IsInTransaccion)
                {
                    await UnitOfWork.SaveChangesAsync();
                }

                return SaveResult.Success();
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
        }

        public virtual SaveResult Delete(TEntity entity)
        {
            try
            {
                entity.Estado = EstadoEnum.Eliminado;
                entity.FechaEliminacion = DateTime.Now;

                UnitOfWork.Db.Entry(entity).State = EntityState.Modified;

                if (!UnitOfWork.IsInTransaccion)
                {
                    UnitOfWork.SaveChanges();
                }

                return SaveResult.Success(entity);
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
        }

        public virtual async Task<SaveResult> DeleteAsync(TEntity entity)
        {
            try
            {
                entity.Estado = EstadoEnum.Eliminado;
                entity.FechaEliminacion = DateTime.Now;

                UnitOfWork.Db.Entry(entity).State = EntityState.Modified;

                if (!UnitOfWork.IsInTransaccion)
                {
                    await UnitOfWork.SaveChangesAsync();
                }
                return SaveResult.Success(entity);
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
        }

        public virtual SaveResult Delete(Guid id)
        {
            try
            {
                var entity = GetById(id);
                return Delete(entity);
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
        }

        public virtual async Task<SaveResult> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                return await DeleteAsync(entity);
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
        }

        public virtual TEntity FirstOrDefault()
        {
            return GetAll().FirstOrDefault();
        }
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Where(predicate).FirstOrDefault();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync()
        {
            return await Task.Run(() => GetAll().FirstOrDefaultAsync());
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => Where(predicate).FirstOrDefaultAsync());
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }
        public virtual async Task<IQueryable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => GetAll().Where(predicate));
        }

        public virtual bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Count(predicate) > 0;
        }

        public virtual async Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await GetAll().CountAsync(predicate);
            return await Task.Run(() => result > 0);
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Count(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => GetAll().CountAsync(predicate));
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    UnitOfWork.Dispose();
                }
            }
            _disposed = true;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual SaveResult DeleteRange(IEnumerable<TEntity> entities)
        {
            try
            {
                var enumerable = entities as TEntity[] ?? entities.ToArray();
                DbSet.RemoveRange(enumerable);

                if (!UnitOfWork.IsInTransaccion)
                {
                    UnitOfWork.Db.SaveChanges();
                }

                return SaveResult.Success();
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
        }

        public virtual async Task<SaveResult> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                var enumerable = entities as TEntity[] ?? entities.ToArray();
                DbSet.RemoveRange(enumerable);
                if (!UnitOfWork.IsInTransaccion)
                {
                    await UnitOfWork.Db.SaveChangesAsync();
                }

                return SaveResult.Success();
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
        }


    }
}
