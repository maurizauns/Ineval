using RP.DAL.Repository;
using Ineval.DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ineval.BO
{
    public class CountryService : CatalogService<Country>
    {
        private readonly string countryList = "countryList";
        public CountryService()
        {
        }
        public CountryService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}