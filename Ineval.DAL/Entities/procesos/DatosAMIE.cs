using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.DAL.Entities.procesos
{
    public class DatosAMIE:BaseEntity
    {
        public string Amie { get; set; }
        public string NombreInstitucion { get; set; }
        public string id_provincia { get; set; }
        public string provincia { get; set; }
        public string canton_id { get; set; }
        public string canton { get; set; }
        public string id_parroquia { get; set; }
        public string parroquia { get; set; }
        public string id_circuito { get; set; }
        public string circuito { get; set; }
        public string id_distrito { get; set; }
        public string distrito { get; set; }
        public string id_zona { get; set; }
        public string sostenimiento_institucion { get; set; }
        public string regimen_institucion { get; set; }
    }
}
