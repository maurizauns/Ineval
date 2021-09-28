using AutoMapper;
using Ineval.BO;
using Ineval.DAL;
using Ineval.Dto;
using Ineval.Models.Filters;
using MvcJqGrid;
using RP.Website.Helpers;
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
    public class ParroquiaController : BaseController<Guid, Parroquia, ParroquiaViewModel>
    {
        public ParroquiaController()
        {
            EntityService = new ParroquiaService();
            Title = "Parroquia";
        }

        public override void OnBeginIndex()
        {
            List<Country> paises = null;
            List<Canton> cantones = null;
            using (var paisService = new CountryService())
            {
                paises = paisService.GetAll().ToList();
                ViewBag.CountryId = new SelectList(paises, "Id", "Description", null);
            }

            using (var provinciaService = new ProvinceService())
            {
                var paisId = Guid.Empty;
                if (paises.Any())
                {
                    paisId = paises.FirstOrDefault().Id;
                }

                ViewBag.ProvinceId = new SelectList(provinciaService.Where(p => p.CountryId == paisId).ToList(), "Id", "Description", null);
            }

            using (var cantonService = new CantonService())
            {
                var provinceId = Guid.Empty;
                if (paises.Any())
                {
                    provinceId = paises.FirstOrDefault().Id;
                }
                ViewBag.CantonId = new SelectList(cantonService.Where(c => c.ProvinceId == provinceId).ToList(), "Id", "Description", null);
            }
        }
        
        //// GET: Parroquia
        //public ActionResult Index()
        //{
        //    return View();
        //}

        protected override IQueryable<Parroquia> ApplyFilters(IQueryable<Parroquia> generalQuery, Rule[] filters)
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

        protected override string[] GetRow(Parroquia item)
        {
            return new[]
             {
                HttpUtility.HtmlEncode(item.Code),
                HttpUtility.HtmlEncode(item.Description),
                HttpUtility.HtmlEncode(item.Canton != null ? item.Canton.Description : ""),
                HttpUtility.HtmlEncode(item.Province != null ? item.Province.Description : ""),
                HttpUtility.HtmlEncode(item.Country != null ? item.Country.Description: ""),
                HttpUtility.HtmlEncode(GridHelperExts.ActionsList("canton-modal")
                        .Add(GridHelperExts.EditAction(Url.Action("GetEntity"), item.Id, "cantonCallback"))
                        .Add(GridHelperExts.DeleteAction(Url.Action("Delete"), "canton-grid", item.Id))
                        .End())
            };
        }

        protected override ParroquiaViewModel MapperEntityToModel(Parroquia entity)
        {
            return Mapper.Map<Parroquia, ParroquiaViewModel>(entity);
        }

        protected override Parroquia MapperModelToEntity(ParroquiaViewModel viewModel)
        {
            var parroquia = new Parroquia();
            if (viewModel.Id != null && viewModel.Id != Guid.Empty)
            {
                parroquia = EntityService.GetById(viewModel.Id.Value);
            }
            return Mapper.Map(viewModel, parroquia);
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

                var result = await elements.Where(q => q.ProvinceId == id).Select(q => new
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