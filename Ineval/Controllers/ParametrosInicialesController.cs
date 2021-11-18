using AutoMapper;
using Ineval.BO;
using Ineval.DAL;
using Ineval.Dto;
using MvcJqGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    [Authorize(Roles = "Administrador, Responsable_Unidad, Analista, Visitante")]
    public class ParametrosInicialesController : BaseController<Guid, ParametrosIniciales, ParametrosInicialesViewModel>
    {
        SwmContext db = new SwmContext();
        public ParametrosInicialesController()
        {
            Title = "Test";
            EntityService = new ParametrosInicialesService();
        }

        protected override IQueryable<ParametrosIniciales> ApplyFilters(IQueryable<ParametrosIniciales> generalQuery, Rule[] filters)
        {
            throw new NotImplementedException();
        }

        protected override string[] GetRow(ParametrosIniciales item)
        {
            throw new NotImplementedException();
        }

        protected override ParametrosInicialesViewModel MapperEntityToModel(ParametrosIniciales entity)
        {
            return Mapper.Map<ParametrosIniciales, ParametrosInicialesViewModel>(entity);
        }

        protected override ParametrosIniciales MapperModelToEntity(ParametrosInicialesViewModel viewModel)
        {
            var parametrosIniciales = new ParametrosIniciales();
            if (viewModel.Id != null && viewModel.Id != Guid.Empty)
            {
                parametrosIniciales = EntityService.GetById(viewModel.Id.Value);
            }
            return Mapper.Map(viewModel, parametrosIniciales);
        }


        public async Task<ActionResult> GetFormulario(Guid? id)
        {
            ParametrosIniciales parametrosIniciales = new ParametrosIniciales();
            parametrosIniciales = await EntityService.FirstOrDefaultAsync(x => x.AsignacionId == id);
            return Json(new { ParametrosIniciales = parametrosIniciales }, JsonRequestBehavior.AllowGet);

        }
    }
}