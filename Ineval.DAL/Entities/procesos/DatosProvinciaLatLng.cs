using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.DAL
{
    [Table("DatosProvinciaLatLng")]
    public class DatosProvinciaLatLng : GeneralConfigurationBase
    {
        public Guid? AsignacionId { get; set; }
        public virtual Asignacion Asignacion { get; set; }
        public string Pais { get; set; }
        public string ProvinciaLat { get; set; }
        public string ProvinciaLng { get; set; }
    }
}
