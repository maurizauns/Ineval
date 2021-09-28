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
        public int? NumeroLaboratorios { get; set; }
        public int? NumeroEquipos { get; set; }
        public int? NumerosSesiones { get; set; }
        public int? NumeroDiasEvaluar { get; set; }
        public int? TiempoViaje { get; set; }
    }
}
