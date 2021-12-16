using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ineval.DAL
{
    [Table("DatosSedesLaboratorio")]
    public class DatosSedesLaboratorio : GeneralConfigurationBase
    {
        public Guid? AsignacionId { get; set; }
        public virtual Asignacion Asignacion { get; set; }
        public int NumeroSession { get; set; }
        public int NumeroLaboratorio { get; set; }
        public string coordenada_lat { get; set; }
        public string coordenada_lng { get; set; }
        public string Agrupados { get; set; }
        public int NumeroTotalSustentantes { get; set; }

        [JsonIgnore]
        public virtual ICollection<DatosSedesAsignacionLaboratorio> DatosSedesAsignacionLaboratorio { get; set; }
    }
}

