using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO
{
    public class DatosSedesLaboratorioService : CatalogService<DatosSedesLaboratorio>
    {
        public DatosSedesLaboratorioService()
        {

        }
        public DatosSedesLaboratorioService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
