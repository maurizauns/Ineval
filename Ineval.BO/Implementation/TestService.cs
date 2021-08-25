using RP.DAL.Repository;
using Ineval.DAL;
namespace Ineval.BO
{
    public class TestService : EntityService<Test>
    {
        public TestService()
        {
        }
        public TestService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}