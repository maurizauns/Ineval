using System.ComponentModel.DataAnnotations;

namespace Ineval.Dto
{
    public class DatosMapboxAPIKEYViewModel : BaseModel
    {
        [EmailAddress(ErrorMessage = "Cuenta de Email Incorrecta.")]
        [Required(ErrorMessage = "Ingrese Email.")]
        [Display(Name = "Email")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Ingrese Contraseña.")]
        [Display(Name = "Contraseña")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Ingrese Clave ApiKey.")]
        [Display(Name = "ApiKey")]
        public string APIKEY { get; set; }

        [Required(ErrorMessage = "Ingrese Máximo Consultas.")]
        [Range(1, 100000,
        ErrorMessage = "El Máximo de Consultas Puede ser Entre {1} y {2}")]
        [Display(Name = "Máximo Consultas")]
        public int NumeroMaximoConsulta { get; set; }

        [Required(ErrorMessage = "Ingrese Mínimo Consultas.")]
        [Range(1, 100000,
        ErrorMessage = "El Mínimo de Consultas Puede ser Entre {1} y {2}")]
        [Display(Name = "Mínimo Consultas")]
        public int NumeroMininoConsulta { get; set; }
        public int NumeroUsadasConsultas { get; set; }
    }
}