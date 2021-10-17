using Ineval.DAL;
using RP.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.BO
{
    public class DatosProvinciaLatLngService: CatalogService<DatosProvinciaLatLng>
    {
        public DatosProvinciaLatLngService()
        {

        }
        public DatosProvinciaLatLngService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
