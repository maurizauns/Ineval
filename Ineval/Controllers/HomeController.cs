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
    [Authorize]
    public class HomeController : Controller
    {
        SwmContext db = new SwmContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<JsonResult> GetAllProcesos()
        {
            List<Asignacion> nombreProcesos = new List<Asignacion>();

            nombreProcesos = await db.Asignacion.ToListAsync();



            return Json(nombreProcesos.Count(), JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> GetAllProcesosActivos()
        {
            AsignacionService Entity = new AsignacionService();

            List<Asignacion> nombreProcesos = await Entity.Where(x => x.EstadoProceso == 1).ToListAsync();

            return Json(nombreProcesos.Count(), JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> GetAllProcesosFinalizados()
        {
            List<Asignacion> nombreProcesos = new List<Asignacion>();

            nombreProcesos = await db.Asignacion.Where(x => x.EstadoProceso == 3).ToListAsync();

            return Json(nombreProcesos.Count(), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> UpdateDatosMapbos()
        {
            try
            {            
                //Primero obtenemos el día actual
                DateTime date = DateTime.Now;

                //Asi obtenemos el primer dia del mes actual
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

                //Y de la siguiente forma obtenemos el ultimo dia del mes
                //agregamos 1 mes al objeto anterior y restamos 1 día.
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

                int resultRes = date.Day - oUltimoDiaDelMes.Day;

                if (resultRes == 0)
                {
                    DatosMapboxAPIKEYService service = new DatosMapboxAPIKEYService();
                    List<DatosMapboxAPIKEY> result = await service.GetAll().ToListAsync();

                    foreach (var item in result)
                    {
                        item.NumeroUsadasConsultas = 0;
                        var entry = db.Entry(item);
                        entry.State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
            finally
            {
                Dispose();
            }
        }
    }
}