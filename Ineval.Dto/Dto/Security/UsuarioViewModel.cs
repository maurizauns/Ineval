using System;
using System.ComponentModel.DataAnnotations;

namespace Ineval.Dto
{
    public class UsuarioViewModel : BaseModel
    {
        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "Ingrese Identificación")]
        public string Identificacion { get; set; }

        [Display(Name = "Tipo Identificación")]
        [Required(ErrorMessage = "Seleccione Tipo Identificación")]
        public string TipoIdentificacion { get; set; }

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "Ingrese Nombres")]
        public string NombresCompletos { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Ingrese e-mail")]
        [EmailAddress(ErrorMessage = "Ingrese un e-mail válido")]
        public string Email { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Seleccione Rol")]
        public string ApplicationRoleName { get; set; }

        [Display(Name = "Establecimientos")]
        public string Establecimientos { get; set; }
        public string APIKEY { get; set; }
    }
}
