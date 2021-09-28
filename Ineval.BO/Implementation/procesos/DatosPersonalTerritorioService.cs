using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO
{
    public class DatosPersonalTerritorioService : EntityService<DatosPersonalTerritorio>
    {
        public DatosPersonalTerritorioService()
        {

        }
        public DatosPersonalTerritorioService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
