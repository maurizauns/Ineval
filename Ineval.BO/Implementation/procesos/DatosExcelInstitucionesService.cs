using Ineval.DAL;
using RP.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.BO
{
    public class DatosExcelInstitucionesService : CatalogService<DatosExcelInstituciones>
    {
        public DatosExcelInstitucionesService()
        {

        }
        public DatosExcelInstitucionesService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}