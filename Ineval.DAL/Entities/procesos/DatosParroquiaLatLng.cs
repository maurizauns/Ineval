using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{
    [Table("DatosParroquiaLatLng")]
    public class DatosParroquiaLatLng : GeneralConfigurationBase
    {
        public Guid? AsignacionId { get; set; }
        public virtual Asignacion Asignacion { get; set; }
        public string Pais { get; set; }
        public string Id_Provincia { get; set; }
        public string Provincia { get; set; }
        public string Id_Canton { get; set; }
        public string Canton { get; set; }
        public string ParroquiaLat { get; set; }
        public string ParroquiaLng { get; set; }
    }
}
