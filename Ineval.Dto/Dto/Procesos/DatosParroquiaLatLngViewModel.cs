using System;

namespace Ineval.Dto
{
    public class DatosParroquiaLatLngViewModel:BaseModel
    {
        public Guid? AsignacionId { get; set; }        
        public string Pais { get; set; }
        public string Id_Provincia { get; set; }
        public string Provincia { get; set; }
        public string Id_Canton { get; set; }
        public string Canton { get; set; }
        public string ParroquiaLat { get; set; }
        public string ParroquiaLng { get; set; }
    }
}
