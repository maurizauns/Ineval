using AutoMapper;
using Ineval.BO;
using Ineval.DAL;
using Ineval.Dto;
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
    public class DatosMapasController : Controller
    {
        SwmContext db = new SwmContext();

        public class Datoscmb
        {
            public string Code { get; set; }
            public string Description { get; set; }
        }
        public async Task<ActionResult> GetFormulario(Guid? id)
        {
            
            List<DatosSedes> result = await db.DatosSedes.Where(x => x.AsignacionId == id).ToListAsync();
            List<DatosSedesViewModel> resultDTO = Mapper.Map<List<DatosSedesViewModel>>(result);           

            List<Datoscmb> datosProvincia = new List<Datoscmb>();
            List<Datoscmb> datosCanton = new List<Datoscmb>();
            List<Datoscmb> datosParroquia = new List<Datoscmb>();            

            return Json(new { result = resultDTO.OrderBy(o => o.Description) }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> MapaByProvincia(Guid? Id, Guid? id_sede)
         {
            List<DatosSedes> lista = new List<DatosSedes>();
            if (id_sede != null)
            {
                lista = await db.DatosSedes.Where(x => x.AsignacionId == Id && x.Id == id_sede).ToListAsync();
            }
            else
            {
                lista = await db.DatosSedes.Where(x => x.AsignacionId == Id).ToListAsync();
            }


            var jsonResult = Json(new { result = lista }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public async Task<ActionResult> MapaByCanton(Guid? Id, string canton_id)
        {
            List<DatosInstituciones> lista = new List<DatosInstituciones>();
            lista = await db.DatosInstituciones.Where(x => x.AsignacionId == Id && x.canton_id == canton_id).ToListAsync();
            var jsonResult = Json(new { result = lista }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public async Task<ActionResult> MapaByParroquia(Guid? Id, string id_parroquia)
        {
            List<DatosInstituciones> lista = new List<DatosInstituciones>();
            lista = await db.DatosInstituciones.Where(x => x.AsignacionId == Id && x.id_parroquia == id_parroquia).ToListAsync();
            var jsonResult = Json(new { result = lista }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public async Task<ActionResult> MapaByAmie(Guid? Id)
        {
            List<DatosInstituciones> lista = new List<DatosInstituciones>();
            lista = await db.DatosInstituciones.Where(x => x.AsignacionId == Id && x.provincia == "Pichincha").ToListAsync();
            var jsonResult = Json(new { result = lista }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

    }
}