using RP.DAL.Repository;
namespace Ineval.BO.Interface
{
    public interface IUnitOfWorkService
    {
        IUnitOfWork UnitOfWork { get; set; }

    }
}
