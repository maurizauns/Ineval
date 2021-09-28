using Ineval.DAL;
using RP.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.BO
{
    public class DatosExcelPersonalService : CatalogService<DatosExcelPersonal>
    {
        public DatosExcelPersonalService()
        {

        }
        public DatosExcelPersonalService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }    
}
