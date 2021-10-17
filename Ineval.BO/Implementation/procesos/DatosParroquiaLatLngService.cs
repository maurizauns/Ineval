using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO.Implementation.procesos
{
    public class DatosParroquiaLatLngService : CatalogService<DatosParroquiaLatLng>
    {
        public DatosParroquiaLatLngService()
        {

        }
        public DatosParroquiaLatLngService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
