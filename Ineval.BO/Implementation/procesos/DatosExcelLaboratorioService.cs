using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO
{
    public class DatosExcelLaboratorioService : CatalogService<DatosExcelLaboratorio>
    {
        public DatosExcelLaboratorioService()
        {

        }
        public DatosExcelLaboratorioService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}