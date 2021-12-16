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
        public DbSet<DatosInstituciones> DatosInstituciones { get; set; }
        public DbSet<ParametrosIniciales> ParametrosIniciales { get; set; }
        public DbSet<DatosExcelPersonal> DatosExcelPersonal { get; set; }
        public DbSet<DatosPersonalTerritorio> DatosPersonalTerritorio { get; set; }
        public DbSet<DatosProvinciaLatLng> DatosProvinciaLatLng { get; set; }
        public DbSet<DatosCantonLatLng> DatosCantonLatLng { get; set; }
        public DbSet<DatosParroquiaLatLng> DatosParroquiaLatLng { get; set; }
        public DbSet<DatosSedes> DatosSedes { get; set; }
        public DbSet<DatosSedesAsignacion> DatosSedesAsignacion { get; set; }
        public DbSet<DatosFiltros> DatosFiltros { get; set; }
        public DbSet<EmailParametros> EmailParametros { get; set; }
        public DbSet<DatosMapboxAPIKEY> DatosMapboxAPIKEY { get; set; }
        public DbSet<DatosExcelInstituciones> DatosExcelInstituciones { get; set; }
        public DbSet<DatosExcelLaboratorio> DatosExcelLaboratorio { get; set; }
        public DbSet<DatosLaboratorio> DatosLaboratorio { get; set; }
        public DbSet<DatosFiltrosLaboratorio> DatosFiltrosLaboratorio { get; set; }
        public DbSet<DatosSedesLaboratorio> DatosSedesLaboratorio { get; set; }
    }
}