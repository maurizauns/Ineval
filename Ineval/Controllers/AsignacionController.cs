using AutoMapper;
using Ineval.BO;
using Ineval.Controllers;
using Ineval.DAL;
using Ineval.Dto;
using Ineval.Dto.Dto.Procesos;
using Ineval.Models.Filters;
using Microsoft.AspNet.Identity;
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
//using static Ineval.Dto.ApiCycling;
using static Ineval.Dto.ApiDriving;
using static Ineval.Dto.ApiPosicionGeografica;

namespace Ineval.App_Start
{
    public class AsignacionController : BaseController<Guid, Asignacion, AsignacionViewModel>
    {
        SwmContext db = new SwmContext();
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
                ViewBag.NombreProcesoId =
                new SelectList((from s in nombreProcesos.ToList() select new { Id = s.Id, Description = "(" + s.Code + ") " + s.Description }), "Id", "Description", null);
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
                HttpUtility.HtmlEncode(item.EstadoProceso.HasValue ? item.EstadoProceso.Value==1 ? "Activo" : "Finalizado" : "Activo"),
                HttpUtility.HtmlEncode(GridHelperExts.ActionsList("asignacion-modal")
                        .Add(GridHelperExts.EditAction(Url.Action("GetEntity"), item.Id, "asignacionCallback"))
                        .Add(GridHelperExts.DeleteAction(Url.Action("Delete"), "asignacion-grid", item.Id))
                         .Add(ConfiguracionAction(item.Id))
                        .End())
            };
        }
        public IHtmlString ConfiguracionAction(object id = null)
        {
            var button = string.Format(@"<li class=""""><a title=""Proceso inicial"" data-toggle=""tooltip"" class=""btn btn-info btn-xs"" href=""{0}""><i class=""bx bxs-cog""></i></a></li>",
                            Url.Action("Record", "Proceso", new { id }));
            return MvcHtmlString.Create(button);
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

        public async Task<ActionResult> GetFormulario(Guid? id)
        {
            //var procesoService = new NombreProcesoService();
            List<NombreProceso> nombreProceso = new List<NombreProceso>();
            using (var procesoService = new NombreProcesoService())
            {
                nombreProceso = procesoService.GetAll().OrderBy(o => o.Description).ToList();
            }
            return Json(new { procesosList = nombreProceso }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> UpdateEstadoProceso(Guid? Id)
        {
            bool estado = false;
            Asignacion asignacions = new Asignacion();

            asignacions = await EntityService.FirstOrDefaultAsync(x => x.Id == Id);

            asignacions.EstadoProceso = 3;

            var result = await EntityService.UpdateAsync(asignacions);
            if (result.Succeeded)
            {
                estado = true;
            }

            return Json(new { message = "Asignación Finalizada Correctamente", status = estado }, JsonRequestBehavior.AllowGet);

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
            UsuarioService usuarioService = new UsuarioService();
            try
            {
                var userId = User.Identity.GetUserId();
                var usuario = usuarioService.ObtenerPorApplicationUserId(userId);

                ApiCycling.Root weatherForecast = await ApiCycling.GetByCycling("-122.42,37.78", "-77.03,38.91", usuario.APIKEY);

                return Json(new { result = weatherForecast, state = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { result = "", state = false }, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                usuarioService.Dispose();
            }
        }

        public async Task<ActionResult> TestApi2()
        {
            UsuarioService usuarioService = new UsuarioService();
            try
            {
                var userId = User.Identity.GetUserId();
                var usuario = usuarioService.ObtenerPorApplicationUserId(userId);

                ApiDriving.Root weatherForecast = await ApiDriving.GetByDriving("-122.42,37.78", "-77.03,38.91", usuario.APIKEY);

                return Json(new { result = weatherForecast, state = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { result = "", state = false }, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                usuarioService.Dispose();
            }
        }

        public async Task<ActionResult> TestApi3()
        {
            UsuarioService usuarioService = new UsuarioService();
            try
            {
                var userId = User.Identity.GetUserId();
                var usuario = usuarioService.ObtenerPorApplicationUserId(userId);

                ApiPosicionGeografica.Root weatherForecast = await ApiPosicionGeografica.GetByPosicionGeografica("AZUAY,CAMILO PONCE ENRIQUEZ", usuario.APIKEY);

                return Json(new { result = weatherForecast, state = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { result = "", state = false }, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                usuarioService.Dispose();
            }
        }

        public class Posicion
        {
            public string Lng { get; set; }
            public string Lat { get; set; }
        }
        public async Task<ActionResult> Test(Guid? Id)
        {
            List<DatosInstituciones> lista = new List<DatosInstituciones>();
            //Guid? id = "ef7fe99a-0f23-ec11-a5dc-50e0857d5969";
            lista = await db.DatosInstituciones.Where(x => x.AsignacionId == Id && x.provincia == "Pichincha").ToListAsync();
            var jsonResult = Json(new { result = lista }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}