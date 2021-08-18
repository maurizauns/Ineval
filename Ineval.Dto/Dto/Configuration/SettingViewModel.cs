using System.ComponentModel.DataAnnotations;

namespace Ineval.Dto
{
    public class SettingViewModel : BaseModel
    {
        [Display(Name = "Ruta Aplicación")]
        public string RutaAplicacion { get; set; }

        [Display(Name = "Registros por Página")]
        public int RegitrosPorPagina { get; set; }
        [Display(Name = "Formato Fecha")]
        public string FormatoFecha { get; set; }
    }
}