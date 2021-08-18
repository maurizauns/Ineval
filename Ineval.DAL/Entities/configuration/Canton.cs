using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{

    [Table("Canton")]
    public class Canton : GeneralConfigurationBase
    {
        public Guid? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public Guid? ProvinceId { get; set; }
        public virtual Province Province { get; set; }

    }
}