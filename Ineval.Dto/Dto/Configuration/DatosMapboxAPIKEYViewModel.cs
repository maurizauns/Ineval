using System.ComponentModel.DataAnnotations;

namespace Ineval.Dto
{
    public class DatosMapboxAPIKEYViewModel : BaseModel
    {
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
        [Display(Name = "Máximo Consultas")]
        public int NumeroMaximoConsulta { get; set; }

        [Required(ErrorMessage = "Ingrese Mínomo Consultas.")]
        [Display(Name = "Mínomo Consultas")]
        public int NumeroMininoConsulta { get; set; }
        public int NumeroUsadasConsultas { get; set; }
    }
}