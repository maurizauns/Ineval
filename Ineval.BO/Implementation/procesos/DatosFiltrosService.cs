using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO
{
    public class DatosFiltrosService : CatalogService<DatosFiltros>
    {
        public DatosFiltrosService()
        {

        }
        public DatosFiltrosService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
