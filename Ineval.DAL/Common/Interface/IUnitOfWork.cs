using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace RP.DAL.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        Task CommitAsync();
        DbContext Db { get; }

        Task<int> SaveChangesAsync();
        int SaveChanges();

        DbContextTransaction StartTransaction();

        bool IsInTransaccion { get; set; }

    }
}
