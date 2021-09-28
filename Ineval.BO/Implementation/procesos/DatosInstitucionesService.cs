using Ineval.DAL;
using RP.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.BO
{
    public class DatosInstitucionesService : EntityService<DatosInstituciones>
    {
        public DatosInstitucionesService()
        {

        }
        public DatosInstitucionesService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
