using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.Dto.Dto.Procesos
{
    public class DatosInstitucionesViewModel: BaseModel
    {
        [Required(ErrorMessage = "Seleccione Tipo de Asignacion.")]
        [Display(Name = "Asignacion")]
        public Guid? AsignacionId { get; set; }
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
