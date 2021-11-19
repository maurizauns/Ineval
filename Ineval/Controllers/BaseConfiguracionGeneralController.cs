using MvcJqGrid;
using RP.Website.Helpers;
using Ineval.DAL;
using Ineval.Dto;
using Ineval.Models.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    [Authorize(Roles = "Administrador, Responsable_Unidad, Analista, Visitante")]
    public class BaseConfiguracionGeneralController<TEntity> : BaseController<Guid, TEntity, GeneralConfigurationViewModel>
        where TEntity : GeneralConfigurationBase, new()
    {
        protected override IQueryable<TEntity> ApplyFilters(IQueryable<TEntity> generalQuery, Rule[] filters)
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
                    generalQuery = generalQuery.Where(x => x.Description.Trim().ToUpper().Contains(term));
                }
            }
            return generalQuery;
        }

        protected override string[] GetRow(TEntity item)
        {
            return new[]
            {
                HttpUtility.HtmlEncode(item.Code),
                HttpUtility.HtmlEncode(item.Description),
                HttpUtility.HtmlEncode(GridHelperExts.ActionsList("configuracionGeneral-modal")
                            .Add(GridHelperExts.EditAction(Url.Action("GetEntity"), item.Id, "configuracionGeneralCallback"))
                            .Add(GridHelperExts.DeleteAction(Url.Action("Delete"), "configuracionGeneral-grid", item.Id))
                            .End())
            };
        }

        protected override TEntity MapperModelToEntity(GeneralConfigurationViewModel viewModel)
        {
            return new TEntity()
            {
                Id = viewModel.Id.GetValueOrDefault(),
                Code = viewModel.Code,
                Description = viewModel.Description,
            };
        }

        protected override GeneralConfigurationViewModel MapperEntityToModel(TEntity entity)
        {
            return new GeneralConfigurationViewModel()
            {
                Id = entity.Id,
                Code = entity.Code,
                Description = entity.Description,
            };
        }

        public override IEnumerable<FieldFilter> Filters
        {
            get
            {
                var filters = new List<FieldFilter>
                {
                    new FieldFilter
                    {
                        Description = "Descripción",
                        Name = "descripcion",
                        Type = FilterType.Textbox
                    },
                    new FieldFilter
                    {
                        Description = "Código",
                        Name = "codigo",
                        Type = FilterType.Textbox
                    }
                };
                return filters;
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> GetValues()
        {
            try
            {
                var elements = await EntityService.GetAllAsync();

                var result = await elements.Select(q => new
                {
                    value = q.Id,
                    text = q.Description
                }).ToListAsync();

                return Json(new
                {
                    success = true,
                    values = (object)result
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    values = default(object)
                }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}