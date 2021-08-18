using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ineval.DAL
{
    public class ApplicationUser : IdentityUser, IBaseEntity
    {
        public ApplicationUser()
        {
            FechaCreacion = DateTime.Now;
            Estado = EstadoEnum.Activo;
            RelatedUsers = new HashSet<ApplicationUser>();
            OtherRelatedUsers = new HashSet<ApplicationUser>();
        }

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public EstadoEnum Estado { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //[NotMapped]
        //public Empresa Empresa { get; set; }

        public virtual ICollection<ApplicationUser> RelatedUsers { get; set; }
        public virtual ICollection<ApplicationUser> OtherRelatedUsers { get; set; }
    }
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            this.HasMany(x => x.RelatedUsers)
               .WithMany(x => x.OtherRelatedUsers)
               .Map(x => x.ToTable("RelatedUsers"));
        }
    }
}
