using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{
    [Table("Test")]
    public class Test : BaseEntity
    {
        public string Name { get; set; }
    }
}
