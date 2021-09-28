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
        public int? NumeroLaboratorios { get; set; }
        public int? NumeroEquipos { get; set; }
        public int? NumerosSesiones { get; set; }
        public int? NumeroDiasEvaluar { get; set; }
        public int? TiempoViaje { get; set; }
    }
}
