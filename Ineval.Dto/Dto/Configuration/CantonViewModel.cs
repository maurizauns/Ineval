using System;
using System.ComponentModel.DataAnnotations;

namespace Ineval.Dto
{
    public class CantonViewModel : BaseModel
    {
        [Required(ErrorMessage = "Ingrese Código.")]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Ingrese Descripción.")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Seleccione Pais.")]
        [Display(Name = "Pais")]
        public Guid? CountryId { get; set; }

        [Required(ErrorMessage = "Seleccione Provincia.")]
        [Display(Name = "Provincia")]
        public Guid? ProvinceId { get; set; }
    }
}
