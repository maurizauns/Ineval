using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{
    [Table("Prueba")]
    public class Test : BaseEntity
    {
        public string Name { get; set; }
    }
}
