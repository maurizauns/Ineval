using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.DAL
{
    [Table("ParametrosIniciales")]
    public class ParametrosIniciales : BaseEntity
    {
        public Guid? AsignacionId { get; set; }
        public virtual Asignacion Asignacion { get; set; }
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
        public int? Tipo { get; set; }
    }
}
