using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO
{
    public class DatosMapboxAPIKEYService : CatalogService<DatosMapboxAPIKEY>
    {
        public DatosMapboxAPIKEYService()
        {

        }
        public DatosMapboxAPIKEYService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}