using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.DAL
{
    [Table("DatosPersonalTerritorio")]
    public class DatosPersonalTerritorio : BaseEntity
    {
        public Guid? AsignacionId { get; set; }
        public virtual Asignacion Asignacion { get; set; }
        public string tipo_documento { get; set; }
        public string numero_documento { get; set; }
        public string nombres_apellidos { get; set; }
        public string sexo { get; set; }
        public string id_provincia { get; set; }
        public string provincia { get; set; }
        public string canton_id { get; set; }
        public string canton { get; set; }
        public string id_parroquia { get; set; }
        public string parroquia { get; set; }
        public string Tipo_Personal { get; set; }
    }
}
