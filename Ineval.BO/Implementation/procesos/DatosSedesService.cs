using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO
{
    public class DatosSedesService : CatalogService<DatosSedes>
    {
        public DatosSedesService()
        {

        }
        public DatosSedesService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
