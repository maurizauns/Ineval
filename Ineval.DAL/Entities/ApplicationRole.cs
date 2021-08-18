using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace Ineval.DAL
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            Menus = new HashSet<Menu>();
        }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
