using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    public class ViewTestController : Controller
    {
        public ActionResult Register()
          => PartialView();
        public ActionResult Register2()
          => PartialView();
        public ActionResult Parametros()
        => PartialView();

        public ActionResult GenerarExcelDatosSustentantes()
        => PartialView();

        public ActionResult GenerarExcelDatosPersonalTerritorio()
        => PartialView();

        public ActionResult SubirDatosSustentantes()
            => PartialView();

        public ActionResult SubirDatosPersonalTerritorio()
            => PartialView();
    }
}