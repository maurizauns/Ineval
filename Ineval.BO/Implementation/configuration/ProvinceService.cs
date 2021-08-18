using RP.DAL.Repository;
using Ineval.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ineval.BO
{
    public class ProvinceService : CatalogService<Province>
    {
        private readonly string provinceList = "provinceList";
        public ProvinceService()
        {

        }
        public ProvinceService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<Province> GetByPaisId(Guid countryId)
        {
            return Where(p => p.CountryId == countryId).ToList();
        }

        public override async Task<SaveResult> DeleteAsync(Guid id)
        {
            using (var service = new CantonService())
            {
                var ciudades = service.GetByProvinciaId(id);
                if (ciudades.Any())
                    return SaveResult.Failed(new[] { "No puede eliminar provincia, tiene ciudades relacionadas." });

            }

            return await base.DeleteAsync(id);
        }

    }
}