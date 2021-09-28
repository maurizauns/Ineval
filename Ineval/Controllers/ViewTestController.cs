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
    }
}