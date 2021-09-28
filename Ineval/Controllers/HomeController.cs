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

            List<Asignacion> nombreProcesos = await Entity.Where(x => x.Estado == EstadoEnum.Activo).ToListAsync();

            return Json(nombreProcesos.Count(), JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> GetAllProcesosFinalizados()
        {
            List<Asignacion> nombreProcesos = new List<Asignacion>();

            nombreProcesos = await db.Asignacion.Where(x => x.Estado != EstadoEnum.Activo).ToListAsync();

            //= await Entity.GetAll().Where(x => x.Estado == EstadoEnum.Eliminado).ToListAsync();

            return Json(nombreProcesos.Count(), JsonRequestBehavior.AllowGet);

        }
    }
}