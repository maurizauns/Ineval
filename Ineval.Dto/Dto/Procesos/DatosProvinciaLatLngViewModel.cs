using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.Dto
{
    public class DatosProvinciaLatLngViewModel: BaseModel
    {
        public Guid? AsignacionId { get; set; }
        public string Pais { get; set; }
        public string ProvinciaLat { get; set; }
        public string ProvinciaLng { get; set; }
    }
}
