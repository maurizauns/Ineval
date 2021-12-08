using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO
{
    public class DatosLaboratorioService : EntityService<DatosLaboratorio>
    {
        public DatosLaboratorioService()
        {

        }
        public DatosLaboratorioService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
