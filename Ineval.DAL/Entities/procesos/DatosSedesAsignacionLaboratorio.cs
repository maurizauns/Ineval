using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{
    [Table("DatosSedesAsignacionLaboratorio")]
    public class DatosSedesAsignacionLaboratorio : GeneralConfigurationBase
    {
        public Guid SedeId { get; set; }
        public virtual DatosSedesLaboratorio DatosSedes { get; set; }
        public string SessionId { get; set; }
        public string LaboratorioId { get; set; }
        public string Dia { get; set; }
        public string FechaEval { get; set; }
        public string Hora { get; set; }
        public Guid SustentanteId { get; set; }
        public virtual DatosTemporales DatosTemporales { get; set; }

    }
}
