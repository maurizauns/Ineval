using Ineval.DAL;
using Ineval.Dto.Dto.Procesos;
using MvcJqGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    public class ProcesoController : BaseProcesoController<Guid,Asignacion, AsignacionViewModel>
    {
        public  ActionResult Record(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        protected override IQueryable<Asignacion> ApplyFilters(IQueryable<Asignacion> generalQuery, Rule[] filters)
        {
            throw new NotImplementedException();
        }

        protected override string[] GetRow(Asignacion item)
        {
            throw new NotImplementedException();
        }

        protected override AsignacionViewModel MapperEntityToModel(Asignacion entity)
        {
            throw new NotImplementedException();
        }

        protected override Asignacion MapperModelToEntity(AsignacionViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}