using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO
{
    public class DatosFiltrosLaboratorioService : CatalogService<DatosFiltrosLaboratorio>
    {
        public DatosFiltrosLaboratorioService()
        {

        }
        public DatosFiltrosLaboratorioService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
