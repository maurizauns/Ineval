using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO
{
    public class DatosCantonLatLngService : CatalogService<DatosCantonLatLng>
    {
        public DatosCantonLatLngService()
        {

        }
        public DatosCantonLatLngService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
