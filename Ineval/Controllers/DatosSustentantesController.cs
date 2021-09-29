using DocumentFormat.OpenXml.Drawing;
using Ineval.BO;
using Ineval.Common;
using Ineval.DAL;
using Ineval.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR.Hubs;
using MvcJqGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DatosSustentantesController : BaseController<Guid, DatosSustentantes, DatosSustentantesViewModel>
    {
        public DatosSustentantesController()
        {
            Title = "Test";
            EntityService = new DatosSustentantesService();
        }

        protected override IQueryable<DatosSustentantes> ApplyFilters(IQueryable<DatosSustentantes> generalQuery, MvcJqGrid.Rule[] filters)
        {
            throw new NotImplementedException();
        }

        protected override string[] GetRow(DatosSustentantes item)
        {
            throw new NotImplementedException();
        }

        protected override DatosSustentantesViewModel MapperEntityToModel(DatosSustentantes entity)
        {
            throw new NotImplementedException();
        }

        protected override DatosSustentantes MapperModelToEntity(DatosSustentantesViewModel viewModel)
        {
            throw new NotImplementedException();
        }


        public class Field
        {
            public string FieldName;
            public string FieldType;
            private string v;
            private Type type;

            public Field(string v, Type type)
            {
                this.v = v;
                this.type = type;
            }
        }

        public class DynamicProperty
        {
            private string key;
            private Type type;

            public DynamicProperty(string key, Type type)
            {
                this.key = key;
                this.type = type;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddSustentantesMasiva(HttpPostedFileWrapper archivo, Guid? Id)
        {
            BinaryReader b = new BinaryReader(archivo.InputStream);
            byte[] binData = b.ReadBytes(archivo.ContentLength);
            string result = System.Text.Encoding.UTF7.GetString(binData);

            List<DatosSustentantes> listaCabecera = new List<DatosSustentantes>();


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
                    DatosSustentantes obj = new DatosSustentantes();
                    foreach (var item1 in cabecera.Split(';'))
                    {
                        PropertyInfo prop = obj.GetType().GetProperty(item1.Replace("\r", ""), BindingFlags.Public | BindingFlags.Instance);
                        if (null != prop && prop.CanWrite)
                        {
                            prop.SetValue(obj, vd[i].Replace("\r", ""), null);
                        }
                        i++;

                    }
                    obj.AsignacionId = Id;
                    listaCabecera.Add(obj);
                }
            }


            using (var ctx = new SwmContext())
            {
                ctx.BulkInsert(listaCabecera.ToList());
            }

            var listaDatosPorCabecera = await EntityService.GetAllAsync();

            //listaDatosPorCabecera = listaDatosPorCabecera.GroupBy(x => x.canton_id).ToList();

            try
            {
                //await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Json(new { result = "", status = "error" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = "La matriz ha sido guardada con éxito!", status = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}

