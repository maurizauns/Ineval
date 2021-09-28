using Ineval.BO;
using Ineval.DAL;
using Ineval.Dto;
using MvcJqGrid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DatosPersonalTerritorioController : BaseController<Guid, DatosPersonalTerritorio, DatosPersonalTerritorioViewModel>
    {
        SwmContext db = new SwmContext();
        // GET: DatosPersonalTerritorio
        public DatosPersonalTerritorioController()
        {
            Title = "Datos Personal en Territorio";
            EntityService = new DatosPersonalTerritorioService();
        }

        protected override IQueryable<DatosPersonalTerritorio> ApplyFilters(IQueryable<DatosPersonalTerritorio> generalQuery, Rule[] filters)
        {
            throw new NotImplementedException();
        }

        protected override string[] GetRow(DatosPersonalTerritorio item)
        {
            throw new NotImplementedException();
        }

        protected override DatosPersonalTerritorioViewModel MapperEntityToModel(DatosPersonalTerritorio entity)
        {
            throw new NotImplementedException();
        }

        protected override DatosPersonalTerritorio MapperModelToEntity(DatosPersonalTerritorioViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult> AddSustentantesMasiva(HttpPostedFileWrapper archivo)
        {
            BinaryReader b = new BinaryReader(archivo.InputStream);
            byte[] binData = b.ReadBytes(archivo.ContentLength);
            string result = System.Text.Encoding.UTF7.GetString(binData);

            List<DatosPersonalTerritorio> listaCabecera = new List<DatosPersonalTerritorio>();

            string cabecera = string.Empty;

            foreach (var item in result.Split('\n'))
            {
                cabecera = item;
                break;
            };

            foreach (var item in result.Split('\n').Skip(1))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    int i = 0;
                    string[] vd = item.Split(';');
                    DatosPersonalTerritorio obj = new DatosPersonalTerritorio();
                    foreach (var item1 in cabecera.Split(';'))
                    {
                        PropertyInfo prop = obj.GetType().GetProperty(item1.Replace("\r", ""), BindingFlags.Public | BindingFlags.Instance);
                        if (null != prop && prop.CanWrite)
                        {
                            prop.SetValue(obj, vd[i].Replace("\r", ""), null);
                        }
                        i++;

                    }
                    obj.AsignacionId = Guid.Parse("7D2F63B0-FF1F-EC11-A5DB-50E0857D5969");
                    listaCabecera.Add(obj);
                }
            }


            using (var ctx = new SwmContext())
            {
                ctx.BulkInsert(listaCabecera.ToList());
            }

            b.Close();
            binData = null;
            result = "";
            listaCabecera = null;

            try
            {
            }
            catch (Exception)
            {
                return Json(new { result = "", status = "error" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = "Guardada con éxito!", status = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}