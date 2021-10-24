using Ineval.DAL;
using RP.DAL.Repository;

namespace Ineval.BO
{
    public class EmailParametrosService : CatalogService<EmailParametros>
    {
        public EmailParametrosService()
        {

        }
        public EmailParametrosService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}