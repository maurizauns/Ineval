using System;

namespace Ineval.Dto
{
    public class DatosCantonLatLngViewModel:BaseModel
    {
        public Guid? AsignacionId { get; set; }        
        public string Pais { get; set; }
        public string Id_Provincia { get; set; }
        public string Provincia { get; set; }
        public string CantonLat { get; set; }
        public string CantonLng { get; set; }
    }
}
