using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{
    public class Menu : BaseEntity
    {
        public Menu()
        {
            Roles = new HashSet<ApplicationRole>();
            MenuItems = new List<Menu>();
        }
        public string Nombre { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int Orden { get; set; }
        public Guid? ParentId { get; set; }
        public virtual Menu Parent { get; set; }
        public string Icon { get; set; }
        public string IconColor { get; set; }
        public string Type { get; set; }
        public string Region { get; set; }
        public virtual ICollection<ApplicationRole> Roles { get; set; }

        [NotMapped]
        public List<Menu> MenuItems { get; set; }
    }
}
