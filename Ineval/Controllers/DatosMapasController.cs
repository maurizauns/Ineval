using Ineval.BO;
using Ineval.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
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
            //var procesoService = new NombreProcesoService();
            List<DatosInstituciones> nombreProvincia = new List<DatosInstituciones>();
            //using (var procesoService = new DatosInstitucionesService())
            //{
            //    nombreProvincia = procesoService.GetAll().Where(x => x.AsignacionId == id).ToList();
            //}

            List<Datoscmb> datosProvincia = new List<Datoscmb>();
            List<Datoscmb> datosCanton = new List<Datoscmb>();
            List<Datoscmb> datosParroquia = new List<Datoscmb>();

            var datostemporeProvincias = from datosInstituciones in db.DatosInstituciones
                                         where datosInstituciones.AsignacionId == id
                                         group datosInstituciones by new { datosInstituciones.id_provincia, datosInstituciones.provincia } into DatosAgrupados

                                         select new { Clave = DatosAgrupados.Key, Datos = DatosAgrupados };

            foreach (var item in datostemporeProvincias)
            {
                datosProvincia.Add(new Datoscmb
                {
                    Code = item.Clave.id_provincia,
                    Description = item.Clave.provincia
                });
            }

            var datostemporesCantonces = from datosInstituciones in db.DatosInstituciones
                                         where datosInstituciones.AsignacionId == id
                                         group datosInstituciones by new { datosInstituciones.canton_id, datosInstituciones.canton } into DatosAgrupados

                                         select new { Clave = DatosAgrupados.Key, Datos = DatosAgrupados };

            foreach (var item in datostemporesCantonces)
            {
                datosCanton.Add(new Datoscmb
                {
                    Code = item.Clave.canton_id,
                    Description = item.Clave.canton
                });
            }


            var datostemporeInstituciones = from datosInstituciones in db.DatosInstituciones
                                            where datosInstituciones.AsignacionId == id
                                            group datosInstituciones by new { datosInstituciones.id_parroquia, datosInstituciones.parroquia } into DatosAgrupados

                                            select new { Clave = DatosAgrupados.Key, Datos = DatosAgrupados };

            foreach (var item in datostemporeInstituciones)
            {
                datosParroquia.Add(new Datoscmb
                {
                    Code = item.Clave.id_parroquia,
                    Description = item.Clave.parroquia
                });
            }


            return Json(new { Provincia = datosProvincia.OrderBy(o=>o.Description), Canton = datosCanton.OrderBy(o => o.Description), Parroquia = datosParroquia.OrderBy(o => o.Description) }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> MapaByProvincia(Guid? Id, string id_provincia)
        {
            List<DatosInstituciones> lista = new List<DatosInstituciones>();
            //Guid? id = "ef7fe99a-0f23-ec11-a5dc-50e0857d5969";
            lista = await db.DatosInstituciones.Where(x => x.AsignacionId == Id && x.id_provincia == id_provincia).ToListAsync();
            var jsonResult = Json(new { result = lista }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public async Task<ActionResult> MapaByCanton(Guid? Id, string canton_id)
        {
            List<DatosInstituciones> lista = new List<DatosInstituciones>();
            //Guid? id = "ef7fe99a-0f23-ec11-a5dc-50e0857d5969";
            lista = await db.DatosInstituciones.Where(x => x.AsignacionId == Id && x.canton_id == canton_id).ToListAsync();
            var jsonResult = Json(new { result = lista }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public async Task<ActionResult> MapaByParroquia(Guid? Id, string id_parroquia)
        {
            List<DatosInstituciones> lista = new List<DatosInstituciones>();
            //Guid? id = "ef7fe99a-0f23-ec11-a5dc-50e0857d5969";
            lista = await db.DatosInstituciones.Where(x => x.AsignacionId == Id && x.id_parroquia == id_parroquia).ToListAsync();
            var jsonResult = Json(new { result = lista }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public async Task<ActionResult> MapaByAmie(Guid? Id)
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