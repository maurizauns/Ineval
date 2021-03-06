using System;
using System.ComponentModel.DataAnnotations;

namespace Ineval.Dto
{
    public class EmailParametrosViewModel : BaseModel
    {   
        [Required(ErrorMessage = "Ingrese Email Principal para configurar en el envio de correos.")]
        [Display(Name = "Email Principal")]
        public string EmailPrincipal { get; set; }

        [Required(ErrorMessage = "Ingrese Contraseña del correo Principal para configurar en el envio de correos.")]
        [Display(Name = "Contraseña Email Princiapl")]
        public string EmailPassword { get; set; }        
        public string EmailCopia { get; set; }
    }
}
