using System.ComponentModel.DataAnnotations;

namespace Ineval.Dto
{
    public class GeneralConfigurationViewModel : BaseModel
    {
        [Required(ErrorMessage = "Ingrese Código")]
        [Display(Name = "Código:")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Ingrese Descripción")]
        [Display(Name = "Descripción:")]
        public string Description { get; set; }
    }
}