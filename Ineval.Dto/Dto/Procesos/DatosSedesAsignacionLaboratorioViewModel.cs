using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.Dto
{
    public class DatosSedesAsignacionLaboratorioViewModel : GeneralConfigurationViewModel
    {
        public Guid SedeId { get; set; }
        public virtual DatosSedesLaboratorioViewModel DatosSedes { get; set; }
        public string SessionId { get; set; }
        public string LaboratorioId { get; set; }
        public string Dia { get; set; }
        public string FechaEval { get; set; }
        public string Hora { get; set; }
        public Guid SustentanteId { get; set; }
        public virtual DatosTemporalesViewModel DatosTemporales { get; set; }
    }
}

