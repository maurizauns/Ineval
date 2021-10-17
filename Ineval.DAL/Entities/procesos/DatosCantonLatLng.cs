using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{
    [Table("DatosCantonLatLng")]
    public class DatosCantonLatLng : GeneralConfigurationBase
    {
        public Guid? AsignacionId { get; set; }
        public virtual Asignacion Asignacion { get; set; }
        public string Pais { get; set; }
        public string Id_Provincia { get; set; }
        public string Provincia { get; set; }
        public string CantonLat { get; set; }
        public string CantonLng { get; set; }
    }
}
