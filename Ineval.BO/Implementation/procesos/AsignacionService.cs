using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO
{
    public class AsignacionService: CatalogService<Asignacion>
    {
        public AsignacionService()
        {

        }
        public AsignacionService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        //public List<Canton> GetByProvinciaId(Guid provinceId)
        //{
        //    return Where(p => p.ProvinceId == provinceId).ToList();
        //}
    }
}
