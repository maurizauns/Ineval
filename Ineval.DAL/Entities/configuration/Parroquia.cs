using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.DAL
{
    [Table("Parroquia")]
    public class Parroquia:GeneralConfigurationBase
    {
        public Guid? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public Guid? ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        public Guid? CantonId { get; set; }
        public virtual Canton Canton { get; set; }
    }
}
