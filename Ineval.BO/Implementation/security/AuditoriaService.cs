using RP.DAL.Repository;
using Ineval.DAL;

namespace Ineval.BO
{
    public class AuditoriaService : EntityService<Auditoria>
    {
        public AuditoriaService()
        {

        }
        public AuditoriaService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
