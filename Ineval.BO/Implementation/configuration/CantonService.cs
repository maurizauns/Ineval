using RP.DAL.Repository;
using Ineval.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Ineval.BO
{
    public class CantonService : CatalogService<Canton>
    {
        public CantonService()
        {

        }
        public CantonService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<Canton> GetByProvinciaId(Guid provinceId)
        {
            return Where(p => p.ProvinceId == provinceId).ToList();
        }
    }
}