using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.Dto
{
    public class ParametrosInicialesViewModel : BaseModel
    {
        [Required(ErrorMessage = "Seleccione Tipo de Asignacion.")]
        [Display(Name = "Asignacion")]
        public Guid? AsignacionId { get; set; }
        public bool? SiNoNumeroLaboratorios { get; set; }
        public int? NumeroLaboratorios { get; set; }
        public bool? SiNoNumeroEquipos { get; set; }
        public int? NumeroEquipos { get; set; }
        public bool? SiNoNumerosSesiones { get; set; }
        public int? NumerosSesiones { get; set; }
        public bool? SiNoNumeroDiasEvaluar { get; set; }
        public int? NumeroDiasEvaluar { get; set; }
        public bool? SiNoTiempoViaje { get; set; }
        public int? TiempoViaje { get; set; }
        public bool? SiNoDuracionPrueba { get; set; }
        public int? DuracionPrueba { get; set; }
        public string HoraMaxima { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string TiempoEvaluacion { get; set; }
        public string TiempoReceso { get; set; }
        public string TiempoReal { get; set; }
        public int? tipo { get; set; }
        public string FechaSesion { get; set; }
        public string HorariosSesion { get; set; }

        public int? SustentantesTotales { get; set; }
        public int? SesionesOcupar { get; set; }
        public int? TotalLaboratorios { get; set; }
    }
}
