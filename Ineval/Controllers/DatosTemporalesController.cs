using AutoMapper;
using Ineval.BO;
using Ineval.DAL;
using Ineval.Dto;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
        public async Task<ActionResult> AddSustentantesMasiva(HttpPostedFileWrapper archivo, Guid? Id)
        {
            var userId = User.Identity.GetUserId();

            BinaryReader b = new BinaryReader(archivo.InputStream);
            byte[] binData = b.ReadBytes(archivo.ContentLength);
            string result = System.Text.Encoding.UTF7.GetString(binData);


            int datoseliminacion = await db.DatosTemporales.AsNoTracking().Where(x => x.AsignacionId == Id).CountAsync();
            if (datoseliminacion > 0)
            {
                var registroseliminados = db.Database.SqlQuery<List<int>>("exec sp_DeleteDatosTemporales @AsignacionId", new SqlParameter("AsignacionId", Id)).ToList();
            }


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
                    obj.AsignacionId = Id;
                    listaCabecera.Add(obj);
                }
            }

            try
            {
                insertMasiveData(listaCabecera);

                b.Close();
                binData = null;
                result = "";
                listaCabecera = null;

                bool status = await EnvioCorreos.SendAsync(userId, "Carga de Datos Exitosos");

            }
            catch (Exception ex)
            {
                return Json(new { result = ex.Message.ToString(), status = "error" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = "Guardada con éxito!", status = "success" }, JsonRequestBehavior.AllowGet);
        }


        public void insertMasiveData(IEnumerable<DatosTemporales> datosTemporales)
        {
            var table = new DataTable();
            table.Columns.Add("Id", typeof(Guid));
            table.Columns.Add("usu_id", typeof(string));
            table.Columns.Add("tipo_identificacion", typeof(string));
            table.Columns.Add("identificacion", typeof(string));
            table.Columns.Add("primer_nombre", typeof(string));
            table.Columns.Add("segundo_nombre", typeof(string));
            table.Columns.Add("primer_apellido", typeof(string));
            table.Columns.Add("segundo_apellido", typeof(string));
            table.Columns.Add("sexo", typeof(string));
            table.Columns.Add("grado", typeof(string));
            table.Columns.Add("paralelo", typeof(string));
            table.Columns.Add("dia_nacimiento", typeof(string));
            table.Columns.Add("mes_nacimiento", typeof(string));
            table.Columns.Add("anio_nacimiento", typeof(string));
            table.Columns.Add("pais_nacimiento", typeof(string));
            table.Columns.Add("provincia_nacimiento", typeof(string));
            table.Columns.Add("discapacidad", typeof(string));
            table.Columns.Add("tipo_discapacidad", typeof(string));
            table.Columns.Add("porcentaje_discapacidad", typeof(string));
            table.Columns.Add("correo_sustentante", typeof(string));
            table.Columns.Add("telefono_sustentante", typeof(string));
            table.Columns.Add("telefono_sustentante_secundario", typeof(string));
            table.Columns.Add("celular_sustentante", typeof(string));
            table.Columns.Add("jornada_sustentante", typeof(string));
            table.Columns.Add("saber", typeof(string));
            table.Columns.Add("amie", typeof(string));
            table.Columns.Add("nombre_institucion", typeof(string));
            table.Columns.Add("id_provincia", typeof(string));
            table.Columns.Add("provincia", typeof(string));
            table.Columns.Add("canton_id", typeof(string));
            table.Columns.Add("canton", typeof(string));
            table.Columns.Add("id_parroquia", typeof(string));
            table.Columns.Add("parroquia", typeof(string));
            table.Columns.Add("id_circuito", typeof(string));
            table.Columns.Add("circuito", typeof(string));
            table.Columns.Add("id_distrito", typeof(string));
            table.Columns.Add("distrito", typeof(string));
            table.Columns.Add("id_zona", typeof(string));
            table.Columns.Add("sostenimiento_institucion", typeof(string));
            table.Columns.Add("regimen_institucion", typeof(string));
            table.Columns.Add("ciclo", typeof(string));
            table.Columns.Add("poblacion", typeof(string));
            table.Columns.Add("modalidad", typeof(string));
            table.Columns.Add("coordenada_x", typeof(string));
            table.Columns.Add("coordenada_y", typeof(string));
            table.Columns.Add("computador", typeof(string));
            table.Columns.Add("internet", typeof(string));
            table.Columns.Add("conexion_internet", typeof(string));
            table.Columns.Add("camara_web", typeof(string));
            table.Columns.Add("microfono", typeof(string));
            table.Columns.Add("FechaCreacion", typeof(DateTime));
            table.Columns.Add("FechaModificacion", typeof(DateTime));
            table.Columns.Add("FechaEliminacion", typeof(DateTime));
            table.Columns.Add("Estado", typeof(EstadoEnum));
            table.Columns.Add("AsignacionId", typeof(Guid));

            foreach (var item in datosTemporales)
            {
                table.Rows.Add(new object[]{
                   item.Id=Guid.NewGuid(),
                  item.usu_id,
                  item.tipo_identificacion
                  ,item.identificacion!="" && item.identificacion!=null ? item.identificacion.Length<10 ? "0"+item.identificacion:item.identificacion:item.identificacion
                  ,item.primer_nombre
                  ,item.segundo_nombre
                  ,item.primer_apellido
                  ,item.segundo_apellido
                  ,item.sexo
                  ,item.grado
                  ,item.paralelo
                  ,item.dia_nacimiento
                  ,item.mes_nacimiento
                  ,item.anio_nacimiento
                  ,item.pais_nacimiento
                  ,item.provincia_nacimiento
                  ,item.discapacidad
                  ,item.tipo_discapacidad
                  ,item.porcentaje_discapacidad
                  ,item.correo_sustentante
                  ,item.telefono_sustentante
                  ,item.telefono_sustentante_secundario
                  ,item.celular_sustentante
                  ,item.jornada_sustentante
                  ,item.saber
                  ,item.amie
                  ,item.nombre_institucion
                  ,item.id_provincia
                  ,item.provincia
                  ,item.canton_id
                  ,item.canton
                  ,item.id_parroquia
                  ,item.parroquia
                  ,item.id_circuito
                  ,item.circuito
                  ,item.id_distrito
                  ,item.distrito
                  ,item.id_zona
                  ,item.sostenimiento_institucion
                  ,item.regimen_institucion
                  ,item.ciclo
                  ,item.poblacion
                  ,item.modalidad
                  ,item.coordenada_x
                  ,item.coordenada_y
                  ,item.computador
                  ,item.internet
                  ,item.conexion_internet
                  ,item.camara_web
                  ,item.microfono
                  ,item.FechaCreacion
                  ,item.FechaModificacion
                  ,item.FechaEliminacion
                  ,item.Estado
                  ,item.AsignacionId
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
                            bulkCopy.DestinationTableName = "DatosTemporales";
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

        public async Task<ActionResult> Contar()
        {
            var datostemporesls = from datosTemporales in db.DatosTemporales
                                  group datosTemporales by new { datosTemporales.amie, datosTemporales.nombre_institucion } into DatosAgrupados

                                  select new { Clave = DatosAgrupados.Key, Datos = DatosAgrupados };


            foreach (var item in datostemporesls)
            {

            }


            return Json(new { datos = datostemporesls.Count() }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> MigrarDatosInstituciones(Guid? Id)
        {
            try
            {
                List<DatosTemporalesViewModel> datostemporesls = db.Database.SqlQuery<DatosTemporalesViewModel>("exec sp_MigrarDatosInstituciones @AsignacionId", new SqlParameter("AsignacionId", Id)).ToList<DatosTemporalesViewModel>();

                if (datostemporesls.Count() > 0)
                {
                    List<DatosInstituciones> listaInstitucionbes = new List<DatosInstituciones>();
                    foreach (var item in datostemporesls)
                    {
                        listaInstitucionbes.Add(new DatosInstituciones
                        {
                            AsignacionId = Id,
                            Amie = item.amie,
                            NombreInstitucion = item.nombre_institucion,
                            id_provincia = item.id_provincia,
                            provincia = item.provincia,
                            canton_id = item.canton_id,
                            canton = item.canton,
                            id_parroquia = item.id_parroquia,
                            parroquia = item.parroquia,
                            id_distrito = item.id_distrito,
                            distrito = item.distrito,
                            id_zona = item.id_zona,
                            circuito = item.circuito,
                            id_circuito = item.id_circuito,
                            sostenimiento_institucion = item.sostenimiento_institucion,
                            regimen_institucion = item.regimen_institucion,
                            coordenada_Lat = item.coordenada_x != null ? item.coordenada_x.Replace(',', '.') : null,
                            coordenada_Lng = item.coordenada_y != null ? item.coordenada_y.Replace(',', '.') : null,
                        });
                    }

                    using (var ctx = new SwmContext())
                    {
                        ctx.BulkInsert(listaInstitucionbes.ToList());
                    }

                    return Json(new { message = "Migración con éxito!", status = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "No hay Datos de Instituciones!", status = "error" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(new { message = ex.Message.ToString(), status = "error" }, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                Dispose();
            }

            return Json(new { }, JsonRequestBehavior.AllowGet);


        }


        public async Task<ActionResult> GetFormulario(Guid? id)
        {
            DatosInstitucionesService datosInstitucionesService = new DatosInstitucionesService();
            var result = await EntityService.GetAll().Where(x => x.AsignacionId == id).CountAsync();
            var resultInstituciones = await datosInstitucionesService.GetAll().Where(x => x.AsignacionId == id).CountAsync();

            return Json(new { Total = result, TotalInstituciones = resultInstituciones }, JsonRequestBehavior.AllowGet);

        }
    }
}