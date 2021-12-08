using ClosedXML.Excel;
using Ineval.BO;
using Ineval.DAL;
using Microsoft.AspNet.Identity;
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
    [Authorize(Roles = "Administrador, Responsable_Unidad, Analista, Visitante")]
    public class DatosExcelLaboratorioController : BaseConfiguracionGeneralController<DatosExcelLaboratorio>
    {
        SwmContext db = new SwmContext();
        public DatosExcelLaboratorioController()
        {
            EntityService = new DatosExcelLaboratorioService();
            Title = "Excel Laboratorio";
        }

        public class DatosExcelNew
        {
            public Guid Id { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public bool check { get; set; }
            public bool Habilitado { get; set; }
        }

        public class IdSelecionados
        {
            public Guid Id { get; set; }
        }

        public async Task<ActionResult> GetFormulario(Guid? id)
        {
            string NombreProceso = "";
            using (AsignacionService asignacionService = new AsignacionService())
            {
                var result = await asignacionService.WhereAsync(x => x.Id == id);
                NombreProceso = " (" + result.FirstOrDefault().NombreProceso.Code + ") " + result.FirstOrDefault().NombreProceso.Description;
            }

            List<DatosExcelNew> datosExcelNews = new List<DatosExcelNew>();
            var ListaDatosExcel = await EntityService.GetAllAsync();

            List<Guid> listaGuid = new List<Guid>();

            listaGuid.Add(new Guid("EBC40568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("ECC40568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("EDC40568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("EEC40568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("EFC40568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("F0C40568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("F1C40568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("F2C40568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("F3C40568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("F4C40568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("F5C40568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("F6C40568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("F7C40568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("00C50568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("01C50568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("02C50568-C357-EC11-A5ED-50E0857D5969"));
            listaGuid.Add(new Guid("03C50568-C357-EC11-A5ED-50E0857D5969"));

            foreach (var item in ListaDatosExcel)
            {

                datosExcelNews.Add(new DatosExcelNew
                {
                    Id = item.Id,
                    Code = item.Code,
                    Description = item.Description,
                    check = listaGuid.Contains(item.Id) ? true : false,
                    //Habilitado = listaGuid.Contains(item.Id) ? false : true,
                });
            }
            return Json(new { NombreProceso = NombreProceso, ListaDatosExcel = datosExcelNews }, JsonRequestBehavior.AllowGet);
        }

        public virtual async Task<ActionResult> Generar(ICollection<GenericaId> Ids)
        {
            var userId = User.Identity.GetUserId();

            List<DatosExcelLaboratorio> datosExcelCabeceras = new List<DatosExcelLaboratorio>();
            List<DatosExcelLaboratorio> lista = EntityService.GetAll().ToList();

            foreach (var item in Ids)
            {
                List<DatosExcelLaboratorio> valor = lista.Where(x => x.Id == item.Id).ToList();
                if (valor != null)
                {
                    DatosExcelLaboratorio datos = new DatosExcelLaboratorio
                    {
                        Id = valor.FirstOrDefault().Id,
                        Code = valor.FirstOrDefault().Code,
                        Description = valor.FirstOrDefault().Description,
                        FechaCreacion = valor.FirstOrDefault().FechaCreacion,
                        FechaModificacion = valor.FirstOrDefault().FechaModificacion,
                        FechaEliminacion = valor.FirstOrDefault().FechaEliminacion,
                        Estado = valor.FirstOrDefault().Estado
                    };

                    datosExcelCabeceras.Add(datos);
                }
            }

            List<string> cabecera = new List<string>();
            foreach (var item in datosExcelCabeceras)
            {
                cabecera.Add(item.Code);
            }



            bool mensaje = await EnvioCorreos.SendAsync(userId, "Se descargo la matriz de Datos Instituciones");


            return Json(new { Datos = true, cabecera }, JsonRequestBehavior.AllowGet);
        }

        public class GenericaId
        {
            public Guid? Id { get; set; }
        }

        public ActionResult ExportarExcel(string cabecera, string NombreDocumento)
        {


            XLWorkbook wb = new XLWorkbook();
            var ws1 = wb.Worksheets.Add("Reportes");

            int cont = 1;
            var data = cabecera.Split(',');

            foreach (var item in data)
            {
                ws1.Cell(1, cont).Value = item;
                ws1.Cell(1, cont).Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
                ws1.Cell(1, cont).Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
                ws1.Cell(1, cont).Style.Font.Bold = true;
                cont++;
            }

            ws1.Cell(1, 13).Style.Alignment.WrapText = true;

            ws1.Columns().AdjustToContents();

            return new ExcelResult(wb, NombreDocumento + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        //CARGAR DATOS LABORATORIOS
        [HttpPost]
        public async Task<ActionResult> AddSustentantesMasiva(HttpPostedFileWrapper archivo, Guid? Id)
        {
            var userId = User.Identity.GetUserId();

            BinaryReader b = new BinaryReader(archivo.InputStream);
            byte[] binData = b.ReadBytes(archivo.ContentLength);
            string result = System.Text.Encoding.UTF7.GetString(binData);


            //int datoseliminacion = await db.DatosTemporales.AsNoTracking().Where(x => x.AsignacionId == Id).CountAsync();
            //if (datoseliminacion > 0)
            //{
            //    var registroseliminados = db.Database.SqlQuery<List<int>>("exec sp_DeleteDatosTemporales @AsignacionId", new SqlParameter("AsignacionId", Id)).ToList();
            //}


            List<DatosLaboratorio> listaCabecera = new List<DatosLaboratorio>();

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
                    DatosLaboratorio obj = new DatosLaboratorio();
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

                bool status = await EnvioCorreos.SendAsync(userId, "Carga de Datos Exitosos de Datos Sustentantes");

            }
            catch (Exception ex)
            {
                return Json(new { result = ex.Message.ToString(), status = "error" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = "Guardada con éxito!", status = "success" }, JsonRequestBehavior.AllowGet);
        }


        public void insertMasiveData(IEnumerable<DatosLaboratorio> datosTemporales)
        {
            var table = new DataTable();
            table.Columns.Add("Id", typeof(Guid));
            table.Columns.Add("AsignacionId", typeof(Guid));
            table.Columns.Add("sede", typeof(string));
            table.Columns.Add("sede_institucion", typeof(string));
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
            table.Columns.Add("direccion", typeof(string));
            table.Columns.Add("referencia", typeof(string));
            table.Columns.Add("telefono1", typeof(string));
            table.Columns.Add("telefono2", typeof(string));            
            table.Columns.Add("celular", typeof(string));
            table.Columns.Add("sostenimiento", typeof(string));
            table.Columns.Add("regimen", typeof(string));
            table.Columns.Add("jornada", typeof(string));            
            table.Columns.Add("computadora_laboratorio", typeof(string));
            table.Columns.Add("coordenada_Lat", typeof(string));
            table.Columns.Add("coordenada_Lng", typeof(string));            
            table.Columns.Add("FechaCreacion", typeof(DateTime));
            table.Columns.Add("FechaModificacion", typeof(DateTime));
            table.Columns.Add("FechaEliminacion", typeof(DateTime));
            table.Columns.Add("Estado", typeof(EstadoEnum));
            table.Columns.Add("codigo_laboratorio", typeof(string));

            foreach (var item in datosTemporales)
            {
                table.Rows.Add(new object[]{
                   item.Id=Guid.NewGuid(),
                  item.AsignacionId,
                  item.sede
                  ,item.sede_institucion
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
                  ,item.direccion
                  ,item.referencia                  
                  ,item.telefono1
                  ,item.telefono2
                  ,item.celular
                  ,item.sostenimiento
                  ,item.referencia
                  ,item.jornada                  
                  ,item.computadora_laboratorio
                  ,item.coordenada_Lat
                  ,item.coordenada_Lng
                  ,item.FechaCreacion
                  ,item.FechaModificacion
                  ,item.FechaEliminacion
                  ,item.Estado
                  ,item.codigo_laboratorio
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
                            bulkCopy.DestinationTableName = "DatosLaboratorio";
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

        public async Task<ActionResult> GetInstituciones(Guid? id)
        {
            try
            {
                int result = 0;
                int resultInstituciones = 0;
                using (DatosLaboratorioService datosLaboratorioService = new DatosLaboratorioService())
                {
                    result = await datosLaboratorioService.GetAll().Where(x => x.AsignacionId == id).CountAsync();                    
                }                

                return Json(new { Total = result, TotalInstituciones = resultInstituciones }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Dispose();
            }
        }
    }
}