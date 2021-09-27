using Ineval.BO;
using Ineval.Controllers;
using Ineval.DAL;
using Ineval.Dto.Dto.Procesos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ineval.App_Start
{
    public class AsignacionController : BaseController<Guid, Asignacion, AsignacionViewModel>
    {
        public AsignacionController()
        {
            EntityService = new AsignacionService();

            Title = "Excel";
        }
        protected override IQueryable<Asignacion> ApplyFilters(IQueryable<Asignacion> generalQuery,MvcJqGrid.Rule[] filters)
        {
            throw new NotImplementedException();
        }

        protected override string[] GetRow(Asignacion item)
        {
            throw new NotImplementedException();
        }

        protected override AsignacionViewModel MapperEntityToModel(Asignacion entity)
        {
            throw new NotImplementedException();
        }

        protected override Asignacion MapperModelToEntity(AsignacionViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult> GetFormulario(int id)
        {
            //var procesoService = new NombreProcesoService();
            List<NombreProceso> nombreProceso = new List<NombreProceso>();
            using (var procesoService = new NombreProcesoService())
            {
                nombreProceso = procesoService.GetAll().OrderBy(o => o.Description).ToList();
            }
            return Json(new { procesosList = nombreProceso }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> TestApi()
        {
            string url = "https://api.mapbox.com/directions/v5/mapbox/cycling/-122.42,37.78;-77.03,38.91?access_token=pk.eyJ1IjoiY2hyaXNyb2JlcnQiLCJhIjoiY2s5MHZ3azYyMDYzbzNlcGQ0a2gweDYwYSJ9.zcP0ljnRL_Jgb_9RYYECJQ";
            WebRequest webRequest = WebRequest.Create(url);
            HttpWebResponse httpWebResponse = null;

            

            httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

            string result = string.Empty;

            
            using (Stream stream = httpWebResponse.GetResponseStream())
            {
                StreamReader streamReader = new StreamReader(stream);
                result = streamReader.ReadToEnd();
                streamReader.Close();
            }

            Root weatherForecast = JsonConvert.DeserializeObject<Root>(result);

            return Json(new { weatherForecast }, JsonRequestBehavior.AllowGet);
        }

        public class Leg
        {
            public string summary { get; set; }
            public double weight { get; set; }
            public double duration { get; set; }
            public List<object> steps { get; set; }
            public double distance { get; set; }
        }

        public class Route
        {
            public string geometry { get; set; }
            public List<Leg> legs { get; set; }
            public string weight_name { get; set; }
            public double weight { get; set; }
            public double duration { get; set; }
            public double distance { get; set; }
        }

        public class Waypoint
        {
            public double distance { get; set; }
            public string name { get; set; }
            public List<double> location { get; set; }
        }

        public class Root
        {
            public List<Route> routes { get; set; }
            public List<Waypoint> waypoints { get; set; }
            public string code { get; set; }
            public string uuid { get; set; }
        }
    }
}