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
            AsignacionService Entity= new AsignacionService();

            List<Asignacion> nombreProcesos = await Entity.GetAll().ToListAsync();

            return Json(nombreProcesos.Count(), JsonRequestBehavior.AllowGet);
                
        }
    }
}