using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{
    [Table("DatosSedesAsignacion")]
    public class DatosSedesAsignacion : GeneralConfigurationBase
    {
        public Guid SedeId { get; set; }
        public virtual DatosSedes DatosSedes { get; set; }
        public string SessionId { get; set; }
        public string LaboratorioId { get; set; }
        public Guid SustentanteId { get; set; }
        public virtual DatosTemporales DatosTemporales { get; set; }

    }
}
