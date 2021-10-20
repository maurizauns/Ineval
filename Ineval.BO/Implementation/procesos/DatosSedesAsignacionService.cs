using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO
{
    public class DatosSedesAsignacionService : CatalogService<DatosSedesAsignacion>
    {
        public DatosSedesAsignacionService()
        {

        }
        public DatosSedesAsignacionService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
