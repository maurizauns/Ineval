using ClosedXML.Excel;
using Ineval.BO;
using Ineval.DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DatosExcelPersonalController : BaseConfiguracionGeneralController<DatosExcelPersonal>
    {
        // GET: DatosExcelPersonal
        public DatosExcelPersonalController()
        {
            EntityService = new DatosExcelPersonalService();
            Title = "Excel";
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
            listaGuid.Add(new Guid("8196D339-1320-EC11-A5DB-50E0857D5969"));
            listaGuid.Add(new Guid("7864B047-1320-EC11-A5DB-50E0857D5969"));
            listaGuid.Add(new Guid("13BC5857-1320-EC11-A5DB-50E0857D5969"));
            listaGuid.Add(new Guid("60DADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("5F828D5D-1320-EC11-A5DB-50E0857D5969"));
            listaGuid.Add(new Guid("3D3B6D6A-1320-EC11-A5DB-50E0857D5969"));
            listaGuid.Add(new Guid("512AC175-1320-EC11-A5DB-50E0857D5969"));
            listaGuid.Add(new Guid("C4BE1981-1320-EC11-A5DB-50E0857D5969"));
            listaGuid.Add(new Guid("4EE77788-1320-EC11-A5DB-50E0857D5969"));
            listaGuid.Add(new Guid("98E0AD91-1320-EC11-A5DB-50E0857D5969"));
            listaGuid.Add(new Guid("35D7E09B-1320-EC11-A5DB-50E0857D5969"));
            listaGuid.Add(new Guid("BD2D5FAA-1320-EC11-A5DB-50E0857D5969"));

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

            List<DatosExcelPersonal> datosExcelCabeceras = new List<DatosExcelPersonal>();
            List<DatosExcelPersonal> lista = EntityService.GetAll().ToList();

            foreach (var item in Ids)
            {
                List<DatosExcelPersonal> valor = lista.Where(x => x.Id == item.Id).ToList();
                if (valor != null)
                {
                    DatosExcelPersonal datos = new DatosExcelPersonal
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

            bool mensaje = await EnvioCorreos.SendAsync(userId, "Se descargo la matriz de datos de Personal En Territorio");

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

            return new ExcelResult(wb, NombreDocumento + DateTime.Now.ToString("dd/MM/yyyy"));
        }

        public class DatosExcelNew
        {
            public Guid Id { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public bool check { get; set; }
            public bool Habilitado { get; set; }
        }

    }
}