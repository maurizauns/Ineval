using Ineval.BO;
using Ineval.DAL;
using Ineval.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DatosTemporalesController : BaseController<Guid, DatosTemporales, DatosTemporalesViewModel>
    {
        SwmContext db = new SwmContext();
        public DatosTemporalesController()
        {
            Title = "Test";
            EntityService = new DatosTemporalesService();
        }

        protected override IQueryable<DatosTemporales> ApplyFilters(IQueryable<DatosTemporales> generalQuery, MvcJqGrid.Rule[] filters)
        {
            throw new NotImplementedException();
        }

        protected override string[] GetRow(DatosTemporales item)
        {
            throw new NotImplementedException();
        }

        protected override DatosTemporalesViewModel MapperEntityToModel(DatosTemporales entity)
        {
            throw new NotImplementedException();
        }

        protected override DatosTemporales MapperModelToEntity(DatosTemporalesViewModel viewModel)
        {
            throw new NotImplementedException();
        }        

        [HttpPost]
        public async Task<ActionResult> AddSustentantesMasiva(HttpPostedFileWrapper archivo)
        {
            BinaryReader b = new BinaryReader(archivo.InputStream);
            byte[] binData = b.ReadBytes(archivo.ContentLength);
            string result = System.Text.Encoding.UTF7.GetString(binData);           

            List<DatosTemporales> listaCabecera = new List<DatosTemporales>();

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
                    DatosTemporales obj = new DatosTemporales();
                    foreach (var item1 in cabecera.Split(';'))
                    {
                        PropertyInfo prop = obj.GetType().GetProperty(item1.Replace("\r", ""), BindingFlags.Public | BindingFlags.Instance);
                        if (null != prop && prop.CanWrite)
                        {
                            prop.SetValue(obj, vd[i].Replace("\r", ""), null);
                        }
                        i++;

                    }
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
        
        public async Task<ActionResult> Contar()
        {
            var datostemporesls =  from datosTemporales in db.DatosTemporales
                                        group datosTemporales by new { datosTemporales.amie, datosTemporales.nombre_institucion } into DatosAgrupados

                                        select new { Clave = DatosAgrupados.Key, Datos = DatosAgrupados };
            

            foreach (var item in datostemporesls)
            {

            }


            return Json(new { datos = datostemporesls.Count() }, JsonRequestBehavior.AllowGet);
        }
    }
}