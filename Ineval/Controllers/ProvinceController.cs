using AutoMapper;
using MvcJqGrid;
using RP.Website.Helpers;
using Ineval.BO;
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
    [Authorize(Roles = "Administrador")]
    public class ProvinceController : BaseController<Guid, Province, ProvinceViewModel>
    {
        public ProvinceController()
        {
            EntityService = new ProvinceService();
            Title = "Provincia";
        }

        public override void OnBeginIndex()
        {
            using (var paisService = new CountryService())
            {
                ViewBag.CountryId = new SelectList(paisService.GetAll().ToList(), "Id", "Description", null);
            }
        }

        protected override IQueryable<Province> ApplyFilters(IQueryable<Province> generalQuery, Rule[] filters)
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

        protected override string[] GetRow(Province item)
        {
            return new[]
            {
                HttpUtility.HtmlEncode(item.Code),
                HttpUtility.HtmlEncode(item.Description),
                HttpUtility.HtmlEncode(item.Country != null ?  item.Country.Description: ""),
                HttpUtility.HtmlEncode(GridHelperExts.ActionsList("province-modal")
                        .Add(GridHelperExts.EditAction(Url.Action("GetEntity"), item.Id, "provinceCallback"))
                        .Add(GridHelperExts.DeleteAction(Url.Action("Delete"), "province-grid", item.Id))
                        .End())
            };
        }

        protected override Province MapperModelToEntity(ProvinceViewModel viewModel)
        {
            var provincia = new Province();
            if (viewModel.Id != null && viewModel.Id != Guid.Empty)
            {
                provincia = EntityService.GetById(viewModel.Id.Value);
            }
            return Mapper.Map(viewModel, provincia);
        }

        protected override ProvinceViewModel MapperEntityToModel(Province entity)
        {
            return Mapper.Map<Province, ProvinceViewModel>(entity);
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
        public async Task<JsonResult> GetValues(Guid? id)
        {
            try
            {
                var elements = await EntityService.GetAllAsync();

                var result = await elements.Where(q => q.CountryId == id).Select(q => new
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