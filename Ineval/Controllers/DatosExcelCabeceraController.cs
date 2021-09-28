using ClosedXML.Excel;
using Ineval.BO;
using Ineval.DAL;
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
    [Authorize(Roles = "Administrador")]
    public class DatosExcelCabeceraController : BaseConfiguracionGeneralController<DatosExcelCabecera>
    {
        public DatosExcelCabeceraController()
        {
            EntityService = new DatosExcelCabeceraService();
            Title = "Excel";
        }

        public async Task<ActionResult> GetFormulario(int id)
        {
            var ListaDatosExcel = await EntityService.GetAllAsync();
            return Json(new { ListaDatosExcel = ListaDatosExcel }, JsonRequestBehavior.AllowGet);
        }

        public virtual async Task<ActionResult> Generar(ICollection<GenericaId> Ids)
        {
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

            return Json(new { Datos = true, cabecera }, JsonRequestBehavior.AllowGet);            
        }

        public class GenericaId
        {
            public Guid? Id { get; set; }
        }

        public ActionResult ExportarExcel(string cabecera,string NombreDocumento)        
        {
            XLWorkbook wb = new XLWorkbook();
            var ws1 = wb.Worksheets.Add("Reportes");
            
            int cont = 1;
            var data = cabecera.Split(',');

            foreach (var item in data)
            {
                ws1.Cell( 1,cont).Value = item;
                ws1.Cell(1, cont).Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
                ws1.Cell(1, cont).Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
                ws1.Cell(1, cont).Style.Font.Bold = true;
                cont++;
            }

            ws1.Cell(1, 13).Style.Alignment.WrapText = true;

            ws1.Columns().AdjustToContents();

            return new ExcelResult(wb, NombreDocumento + DateTime.Now.ToString("dd/MM/yyyy"));
        }
    }
}