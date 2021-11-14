using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.Dto
{
    public class ParroquiaViewModel : BaseModel
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

        [Required(ErrorMessage = "Seleccione Canton.")]
        [Display(Name = "Canton")]
        public Guid? CantonId { get; set; }

        [Required(ErrorMessage = "Ingrese Latitud.")]
        [Display(Name = "Coordenada_lat")]
        public string Coordenada_lat { get; set; }

        [Required(ErrorMessage = "Ingrese Longitud.")]
        [Display(Name = "Coordenada_lng")]
        public string Coordenada_lng { get; set; }
    }
}
