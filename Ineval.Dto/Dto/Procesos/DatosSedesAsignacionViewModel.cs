using System;

namespace Ineval.Dto
{
    public class DatosSedesAsignacionViewModel : GeneralConfigurationViewModel
    {
        public Guid SedeId { get; set; }
        public virtual DatosSedesViewModel DatosSedes { get; set; }
        public string SessionId { get; set; }
        public string LaboratorioId { get; set; }
        public string Dia { get; set; }
        public string FechaEval { get; set; }
        public string Hora { get; set; }
        public Guid SustentanteId { get; set; }
        public virtual DatosTemporalesViewModel DatosTemporales { get; set; }
    }
}
