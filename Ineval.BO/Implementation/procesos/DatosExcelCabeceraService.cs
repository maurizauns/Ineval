using Ineval.DAL;
using RP.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.BO
{
    public class DatosExcelCabeceraService:CatalogService<DatosExcelCabecera>
    {
        public DatosExcelCabeceraService()
        {

        }
        public DatosExcelCabeceraService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        //public List<Canton> GetByProvinciaId(Guid provinceId)
        //{
        //    return Where(p => p.ProvinceId == provinceId).ToList();
        //}
        //public List<Parroquia> GetByCantonId(Guid cantonId)
        //{
        //    return Where(p => p.CantonId == cantonId).ToList();
        //}
    }
}
