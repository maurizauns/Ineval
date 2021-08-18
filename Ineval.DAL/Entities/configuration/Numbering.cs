using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{
    [Table("Numbering")]
    public class Numbering : BaseEntity
    {
        public string DocumentType { get; set; }
        public string Module { get; set; }
        public int Establishment { get; set; }
        public int EmissionPoint { get; set; }
        public int Sequential { get; set; }
    }
}