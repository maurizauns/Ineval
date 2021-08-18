using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{
    [Table("Province")]
    public class Province : GeneralConfigurationBase
    {
        public Guid? CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}