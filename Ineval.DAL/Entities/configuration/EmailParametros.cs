
using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{
    [Table("EmailParametros")]
    public class EmailParametros : GeneralConfigurationBase
    {
        public string EmailPrincipal { get; set; }
        public string EmailPassword { get; set; }
        public string EmailCopia { get; set; }

    }
}