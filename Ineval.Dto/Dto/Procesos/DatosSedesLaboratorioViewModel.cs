using System;
using System.Collections.Generic;

namespace Ineval.Dto
{
    public class DatosSedesLaboratorioViewModel : GeneralConfigurationViewModel
    {
        public Guid? AsignacionId { get; set; }
        public int NumeroSession { get; set; }
        public int NumeroLaboratorio { get; set; }
        public string coordenada_lat { get; set; }
        public string coordenada_lng { get; set; }
        public string Agrupados { get; set; }
        public int NumeroTotalSustentantes { get; set; }
        public ICollection<DatosSedesAsignacionLaboratorioViewModel> DatosSedesAsignacionLaboratorio { get; set; }
    }
}
