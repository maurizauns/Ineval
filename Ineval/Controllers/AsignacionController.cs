using AutoMapper;
using Ineval.BO;
using Ineval.Controllers;
using Ineval.DAL;
using Ineval.Dto;
using Ineval.Dto.Dto.Procesos;
using Ineval.Models.Filters;
using Newtonsoft.Json;
using RP.Website.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Ineval.Dto.ApiCycling;

namespace Ineval.App_Start
{
    public class AsignacionController : BaseController<Guid, Asignacion, AsignacionViewModel>
    {
        public AsignacionController()
        {
            EntityService = new AsignacionService();

            Title = "Asignación";
        }

        public override void OnBeginIndex()
        {
            List<NombreProceso> nombreProcesos = null;
            using (var nombreProcesosService = new NombreProcesoService())
            {
                nombreProcesos = nombreProcesosService.GetAll().ToList();
                ViewBag.NombreProcesoId = //new SelectList(nombreProcesos, "Id", "Description", null);
                new SelectList((from s in nombreProcesos.ToList()
                                select new
                                {
                                    Id = s.Id,
                                    Description = "(" + s.Code + ") " + s.Description
                                }),
        "Id",
        "Description",
        null);
            }
        }

        protected override IQueryable<Asignacion> ApplyFilters(IQueryable<Asignacion> generalQuery, MvcJqGrid.Rule[] filters)
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

        protected override string[] GetRow(Asignacion item)
        {
            return new[]
             {
                HttpUtility.HtmlEncode(item.Code),
                HttpUtility.HtmlEncode(item.Description),
                HttpUtility.HtmlEncode(item.NombreProceso != null ?  "(" + item.NombreProceso.Code + ") " +item.NombreProceso.Description : ""),
                HttpUtility.HtmlEncode(GridHelperExts.ActionsList("asignacion-modal")
                        //.Add(GridHelperExts.CreateLink(Url.Action("GetEntity"),item.Id,"asignacionCallback"))
                        .Add(GridHelperExts.EditAction(Url.Action("GetEntity"), item.Id, "asignacionCallback"))
                        .Add(GridHelperExts.DeleteAction(Url.Action("Delete"), "asignacion-grid", item.Id))
                        .End())
            };
        }

        protected override AsignacionViewModel MapperEntityToModel(Asignacion entity)
        {
            return Mapper.Map<Asignacion, AsignacionViewModel>(entity);
        }

        protected override Asignacion MapperModelToEntity(AsignacionViewModel viewModel)
        {
            var asignacion = new Asignacion();
            if (viewModel.Id != null && viewModel.Id != Guid.Empty)
            {
                asignacion = EntityService.GetById(viewModel.Id.Value);
            }
            return Mapper.Map(viewModel, asignacion);
        }

        public async Task<ActionResult> GetFormulario(int id)
        {
            //var procesoService = new NombreProcesoService();
            List<NombreProceso> nombreProceso = new List<NombreProceso>();
            using (var procesoService = new NombreProcesoService())
            {
                nombreProceso = procesoService.GetAll().OrderBy(o => o.Description).ToList();
            }
            return Json(new { procesosList = nombreProceso }, JsonRequestBehavior.AllowGet);
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

                var result = await elements.Where(q => q.Id == id).Select(q => new
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

        public async Task<ActionResult> TestApi()
        {
            Root weatherForecast = await ApiCycling.GetByCycling();




            //httpWebResponse = (HttpWebResponse)webRequest.GetResponse();


            using (Stream stream = httpWebResponse.GetResponseStream())
            {
                StreamReader streamReader = new StreamReader(stream);
                result = streamReader.ReadToEnd();
                streamReader.Close();
            }

            //using (Stream stream = httpWebResponse.GetResponseStream())
            //{
            //    StreamReader streamReader = new StreamReader(stream);
            //    result = streamReader.ReadToEnd();
            //    streamReader.Close();
            //}

            //Root weatherForecast = JsonConvert.DeserializeObject<Root>(result);

            return Json(new { weatherForecast }, JsonRequestBehavior.AllowGet);
        }
    }
}