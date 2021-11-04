using Ineval.BO;
using Ineval.DAL;
using Ineval.Dto;
using Microsoft.AspNet.Identity;
using MvcJqGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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

        protected override IQueryable<DatosPersonalTerritorio> ApplyFilters(IQueryable<DatosPersonalTerritorio> generalQuery, MvcJqGrid.Rule[] filters)
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

        public async Task<ActionResult> GetDatos(Guid? id)
        {
            //DatosPersonalTerritorioService datosPersonalTerritorioService = new DatosPersonalTerritorioService();
            var result = await EntityService.GetAll().Where(x => x.AsignacionId == id).CountAsync();
            return Json(new { Total = result }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<ActionResult> AddSustentantesMasiva(HttpPostedFileWrapper archivo, Guid? Id)
        {
            var userId = User.Identity.GetUserId();

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
                    obj.AsignacionId = Id;
                    listaCabecera.Add(obj);
                }
            }

            try
            {
                insertMasiveData(listaCabecera.ToList());
                b.Close();
                binData = null;
                result = "";
                listaCabecera = null;

                bool status = await EnvioCorreos.SendAsync(userId, "Carga de Datos Exitosos de Personal en Territorio");

                return Json(new { result = "Guardada con éxito!", status = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = ex.Message.ToString(), status = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


        public void insertMasiveData(IEnumerable<DatosPersonalTerritorio> datosPersonalTerritorio)
        {
            var table = new DataTable();
            table.Columns.Add("Id", typeof(Guid));
            table.Columns.Add("AsignacionId", typeof(Guid));
            table.Columns.Add("tipo_documento", typeof(string));
            table.Columns.Add("numero_documento", typeof(string));
            table.Columns.Add("nombres_apellidos", typeof(string));
            table.Columns.Add("sexo", typeof(string));
            table.Columns.Add("id_provincia", typeof(string));
            table.Columns.Add("provincia", typeof(string));
            table.Columns.Add("canton_id", typeof(string));
            table.Columns.Add("canton", typeof(string));
            table.Columns.Add("id_parroquia", typeof(string));
            table.Columns.Add("parroquia", typeof(string));
            table.Columns.Add("FechaCreacion", typeof(DateTime));
            table.Columns.Add("FechaModificacion", typeof(DateTime));
            table.Columns.Add("FechaEliminacion", typeof(DateTime));
            table.Columns.Add("Estado", typeof(EstadoEnum));
            table.Columns.Add("Cargo", typeof(string));


            foreach (var item in datosPersonalTerritorio)
            {
                table.Rows.Add(new object[]
                {
                    item.Id=Guid.NewGuid(),
                    item.AsignacionId,
                    item.tipo_documento,
                    item.numero_documento,
                    item.nombres_apellidos,
                    item.sexo,
                    item.id_provincia,
                    item.provincia,
                    item.canton_id,
                    item.canton,
                    item.id_parroquia,
                    item.parroquia,
                    item.FechaCreacion,
                    item.FechaModificacion,
                    item.FechaEliminacion,
                    item.Estado,
                    item.Cargo
                });
            }

            using (var connection = ConnectionToSql.getConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        try
                        {
                            bulkCopy.DestinationTableName = "DatosPersonalTerritorio";
                            bulkCopy.BulkCopyTimeout = 0;
                            bulkCopy.WriteToServer(table);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            connection.Close();
                        }
                    }
                }

            }
        }
    }
}