using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Ineval.DAL
{
    [Table("DatosSedes")]
    public class DatosSedes : GeneralConfigurationBase
    {
        public Guid? AsignacionId { get; set; }
        public virtual Asignacion Asignacion { get; set; }
        public int NumeroSession { get; set; }
        public int NumeroLaboratorio { get; set; }
        public string coordenada_lat { get; set; }
        public string coordenada_lng { get; set; }
        public int NumeroTotalSustentantes { get; set; }

        [JsonIgnore]
        public virtual ICollection<DatosSedesAsignacion> DatosSedesAsignacion { get; set; }
    }
}
