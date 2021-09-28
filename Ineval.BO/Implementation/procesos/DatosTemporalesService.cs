using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO
{
    public class DatosTemporalesService : EntityService<DatosTemporales>
    {
        public DatosTemporalesService()
        {
        }
        public DatosTemporalesService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
