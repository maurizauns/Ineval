using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.Dto.Dto.Procesos
{
    public class AsignacionViewModel:BaseModel
    {
        [Required(ErrorMessage = "Ingrese Código.")]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Ingrese Descripción.")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Seleccione Tipo de Proceso.")]
        [Display(Name = "Proceso")]
        public Guid? NombreProcesoId { get; set; }
    }
}
