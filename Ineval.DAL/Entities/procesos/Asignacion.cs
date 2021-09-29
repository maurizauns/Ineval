using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{
    [Table("Asignacion")]
    public class Asignacion : GeneralConfigurationBase
    {
        public Guid? NombreProcesoId { get; set; }
        public virtual NombreProceso NombreProceso { get; set; }
        public int? EstadoProceso { get; set; }
    }
}
