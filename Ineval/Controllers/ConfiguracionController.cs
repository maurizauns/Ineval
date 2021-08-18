using AutoMapper;
using Ineval.BO;
using Ineval.Common;
using Ineval.Dto;
using Ineval.Extensions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Swm.Controllers
{
    [CustomAuthorize(ModuleName = "Setting")]
    [Authorize]
    public class SettingController : Controller
    {
        private SettingService configuracionService = new SettingService();
        public ActionResult Index()
        {
            var configuracion = SettingService.ObtenerConfiguracion();
            var model = new SettingViewModel();
            model = Mapper.Map(configuracion, model);
            ViewBag.Title = "Configuración";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(SettingViewModel model)
        {
            var configuracion = SettingService.ObtenerConfiguracion();
            configuracion = Mapper.Map(model, configuracion);

            Context.PageSize = configuracion.RegitrosPorPagina;
            Context.FormatoFecha = configuracion.FormatoFecha;
            configuracionService.Update(configuracion);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            configuracionService.Dispose();
            base.Dispose(disposing);
        }
    }
}