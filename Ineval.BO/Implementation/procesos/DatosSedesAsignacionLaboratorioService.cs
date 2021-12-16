using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO
{
    public class DatosSedesAsignacionLaboratorioService : CatalogService<DatosSedesAsignacionLaboratorio>
    {
        public DatosSedesAsignacionLaboratorioService()
        {

        }
        public DatosSedesAsignacionLaboratorioService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
