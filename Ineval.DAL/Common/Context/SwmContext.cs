using EntityFramework.DynamicFilters;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
namespace Ineval.DAL
{
    public class SwmContext : IdentityDbContext<ApplicationUser>
    {
        public static SwmContext Create()
        {
            return new SwmContext();
        }
        public SwmContext()
            // : base("DefaultConnection")
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Database.CommandTimeout = 3000;
            //Database.SetInitializer<WebVentasContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Filter("RegistrosEliminados", (IBaseEntity d, EstadoEnum estado) => d.Estado != estado, EstadoEnum.Eliminado);
        }

        public DbSet<Menu> Menus { get; set; }
        new public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Configuracion> Configuracion { get; set; }
        public DbSet<Numbering> Numbering { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Canton> Canton { get; set; }
        public DbSet<Parroquia> Parroquia { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<DatosExcelCabecera> DatosExcelCabecera { get; set; }
        public DbSet<DatosSustentantes> DatosSustentantes { get; set; }
        public DbSet<NombreProceso> NombreProceso { get; set; }
        public DbSet<DatosTemporales> DatosTemporales { get; set; }
        public DbSet<Asignacion> Asignacion { get; set; }
    }
}