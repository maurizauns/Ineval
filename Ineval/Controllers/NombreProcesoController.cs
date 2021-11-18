using AutoMapper;
using Ineval.BO;
using Ineval.DAL;
using Ineval.Dto;
using MvcJqGrid;
using RP.Website.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    [Authorize(Roles = "Administrador, Responsable_Unidad, Analista, Visitante")]
    public class NombreProcesoController : BaseController<Guid, NombreProceso, NombreProcesoViewModel>
    {
        public NombreProcesoController()
        {
            EntityService = new NombreProcesoService();
            Title = "Nombre Proceso";
        }       

        protected override IQueryable<NombreProceso> ApplyFilters(IQueryable<NombreProceso> generalQuery, Rule[] filters)
        {
            if (filters == null)
            {
                return generalQuery;
            }

            foreach (var item in filters)
            {
                var term = item.data.Trim().ToUpper();

                if (String.Equals(item.field, "codigo", StringComparison.OrdinalIgnoreCase))
                {
                    generalQuery = generalQuery.Where(x => x.Code.Trim().ToUpper().Contains(term));
                }
                else if (String.Equals(item.field, "descripcion", StringComparison.OrdinalIgnoreCase))
                {
                    generalQuery = generalQuery.Where(x => x.Description.Trim().ToLower().Contains(term));
                }
            }
            return generalQuery;
        }

        protected override string[] GetRow(NombreProceso item)
        {
            return new[]
            {
                HttpUtility.HtmlEncode(item.Code),
                HttpUtility.HtmlEncode(item.Description),                
                HttpUtility.HtmlEncode(GridHelperExts.ActionsList("nombreproceso-modal")
                        .Add(GridHelperExts.EditAction(Url.Action("GetEntity"), item.Id, "nombreprocesoCallback"))
                        .Add(GridHelperExts.DeleteAction(Url.Action("Delete"), "nombreproceso-grid", item.Id))
                        .End())
            };
        }

        protected override NombreProcesoViewModel MapperEntityToModel(NombreProceso entity)
        {
            return Mapper.Map<NombreProceso, NombreProcesoViewModel>(entity);
        }

        protected override NombreProceso MapperModelToEntity(NombreProcesoViewModel viewModel)
        {
            var nombreproceso = new NombreProceso();
            if (viewModel.Id != null && viewModel.Id != Guid.Empty)
            {
                nombreproceso = EntityService.GetById(viewModel.Id.Value);
            }
            return Mapper.Map(viewModel, nombreproceso);
        }
    }
}