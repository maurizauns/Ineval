using ClosedXML.Excel;
using Ineval.BO;
using Ineval.DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ineval.Controllers
{
    [Authorize(Roles = "Administrador, Responsable_Unidad, Analista, Visitante")]
    public class DatosExcelCabeceraController : BaseConfiguracionGeneralController<DatosExcelCabecera>
    {
        public DatosExcelCabeceraController()
        {
            EntityService = new DatosExcelCabeceraService();
            Title = "Excel";
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
        [Authorize(Roles = "Administrador, Responsable_Unidad, Analista, Visitante")]
        public async Task<ActionResult> GetFormulario(Guid? id)
        {
            string NombreProceso = "";
            using (AsignacionService asignacionService = new AsignacionService())
            {
                var result = await asignacionService.WhereAsync(x => x.Id == id);
                NombreProceso = " ("+ result.FirstOrDefault().NombreProceso.Code + ") "+result.FirstOrDefault().NombreProceso.Description;
            }

            List<DatosExcelNew> datosExcelNews = new List<DatosExcelNew>();
            var ListaDatosExcel = await EntityService.GetAllAsync();

            List<Guid> listaGuid = new List<Guid>();
            listaGuid.Add(new Guid("5DDADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("5EDADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("5FDADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("60DADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("77DADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("78DADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("79DADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("7ADADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("7BDADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("7CDADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("7DDADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("7EDADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("7FDADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("80DADD44-300B-EC11-A5D2-F062FE885646"));
            listaGuid.Add(new Guid("81DADD44-300B-EC11-A5D2-F062FE885646"));

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

            List<DatosExcelCabecera> datosExcelCabeceras = new List<DatosExcelCabecera>();
            List<DatosExcelCabecera> lista = EntityService.GetAll().ToList();

            foreach (var item in Ids)
            {
                List<DatosExcelCabecera> valor = lista.Where(x => x.Id == item.Id).ToList();
                if (valor != null)
                {
                    DatosExcelCabecera datos = new DatosExcelCabecera
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

            

            bool mensaje = await EnvioCorreos.SendAsync(userId, "Se descargo la matriz de datos Sustentantes");


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
    }
}