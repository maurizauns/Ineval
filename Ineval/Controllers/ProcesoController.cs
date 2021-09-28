using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    public class ProcesoController : Controller
    {
        public  ActionResult Record(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}