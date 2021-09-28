using System;
using System.ComponentModel.DataAnnotations;

namespace Ineval.Dto
{
    public class DatosPersonalTerritorioViewModel:BaseModel
    {
        [Required(ErrorMessage = "Seleccione Tipo de Proceso.")]
        [Display(Name = "Proceso")]
        public Guid? AsignacionId { get; set; }
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
