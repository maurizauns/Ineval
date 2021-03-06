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

        public class DatosSedesSin
        {
            public Guid Id { get; set; }
            public Guid? AsignacionId { get; set; }
            public virtual Asignacion Asignacion { get; set; }
            public string Code { get; set; }

            public string Description { get; set; }
            public int NumeroSession { get; set; }
            public int NumeroLaboratorio { get; set; }
            public string coordenada_lat { get; set; }
            public string coordenada_lng { get; set; }
            public string Agrupados { get; set; }
            public int NumeroTotalSustentantes { get; set; }

        }

        public async Task<ActionResult> GetFormulario(Guid? id)
        {
            List<DatosSedesSin> datosSedesSins = new List<DatosSedesSin>();
            List<DatosSedes> result = await db.DatosSedes.Where(x => x.AsignacionId == id).ToListAsync();
            foreach (var item in result)
            {
                datosSedesSins.Add(new DatosSedesSin
                {
                    Id = item.Id,
                    AsignacionId = item.AsignacionId,
                    Asignacion = item.Asignacion,
                    Code = item.Code,
                    Description = item.Description,
                    coordenada_lat = item.coordenada_lat,
                    coordenada_lng = item.coordenada_lng,
                    Agrupados = item.Agrupados,
                    NumeroLaboratorio = item.NumeroLaboratorio,
                    NumeroSession = item.NumeroSession,
                    NumeroTotalSustentantes = item.NumeroTotalSustentantes
                });
            }
            //List<DatosSedesViewModel> resultDTO = Mapper.Map<List<DatosSedesViewModel>>(result);           

            List<Datoscmb> datosProvincia = new List<Datoscmb>();
            List<Datoscmb> datosCanton = new List<Datoscmb>();
            List<Datoscmb> datosParroquia = new List<Datoscmb>();

            var jsonResult = Json(new { result = datosSedesSins.OrderBy(o => o.Description) }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public async Task<ActionResult> MapaByProvincia(Guid? Id, Guid? id_sede)
        {
            List<DatosSedes> lista = new List<DatosSedes>();
            List<DatosSedesSin> dto = new List<DatosSedesSin>();
            if (id_sede != null)
            {
                lista = await db.DatosSedes.Where(x => x.AsignacionId == Id && x.Id == id_sede).ToListAsync();
            }
            else
            {
                lista = await db.DatosSedes.Where(x => x.AsignacionId == Id).ToListAsync();
            }
            foreach (var item in lista)
            {
                dto.Add(new DatosSedesSin
                {
                    Agrupados = item.Agrupados,
                    Asignacion = item.Asignacion,
                    AsignacionId = item.AsignacionId,
                    Code = item.Code,
                    coordenada_lat = item.coordenada_lat,
                    coordenada_lng = item.coordenada_lng,
                    Description = item.Description,
                    Id= item.Id,
                    NumeroLaboratorio= item.NumeroLaboratorio,
                    NumeroSession = item.NumeroSession,
                    NumeroTotalSustentantes=item.NumeroTotalSustentantes
                });

            }

            

            var jsonResult = Json(new { result = dto }, JsonRequestBehavior.AllowGet);
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