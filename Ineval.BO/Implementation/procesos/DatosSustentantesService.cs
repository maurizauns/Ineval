using Ineval.DAL;
using RP.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.BO
{
    public class DatosSustentantesService: EntityService<DatosSustentantes>
    {
        public DatosSustentantesService()
        {
        }
        public DatosSustentantesService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
