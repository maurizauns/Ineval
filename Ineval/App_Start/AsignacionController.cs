using Ineval.Controllers;
using Ineval.DAL;
using Ineval.Dto.Dto.Procesos;
using MvcJqGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ineval.App_Start
{
    public class AsignacionController : BaseController<Guid, Asignacion, AsignacionViewModel>
    {
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