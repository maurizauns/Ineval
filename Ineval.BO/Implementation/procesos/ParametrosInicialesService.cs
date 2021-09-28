using Ineval.DAL;
using RP.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.BO
{
    public class ParametrosInicialesService:EntityService<ParametrosIniciales>
    {
        public ParametrosInicialesService()
        {

        }
        public ParametrosInicialesService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
