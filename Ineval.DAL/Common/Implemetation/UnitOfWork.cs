using Ineval.DAL;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace RP.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContextTransaction _transaction;
        private readonly DbContext _db;


        public UnitOfWork() : this(new SwmContext())
        {

        }

        public UnitOfWork(DbContext db)
        {
            _db = db;
        }


        private bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public DbContextTransaction StartTransaction()
        {
            IsInTransaccion = true;
            _transaction = _db.Database.BeginTransaction();

            return _transaction;
        }

        public bool IsInTransaccion { get; set; }

        public void Commit()
        {
            _db.SaveChanges();
            _transaction.Commit();
            IsInTransaccion = false;
        }

        public async Task CommitAsync()
        {
            await _db.SaveChangesAsync();
            _transaction.Commit();
            IsInTransaccion = false;
        }

        public DbContext Db
        {
            get { return _db; }
        }
    }
}
