namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _011020210047 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosParroquiaLatLng",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AsignacionId = c.Guid(),
                        Pais = c.String(),
                        Id_Provincia = c.String(),
                        Provincia = c.String(),
                        Id_Canton = c.String(),
                        Canton = c.String(),
                        ParroquiaLat = c.String(),
                        ParroquiaLng = c.String(),
                        Code = c.String(),
                        Description = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosParroquiaLatLng_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Asignacion", t => t.AsignacionId)
                .Index(t => t.AsignacionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatosParroquiaLatLng", "AsignacionId", "dbo.Asignacion");
            DropIndex("dbo.DatosParroquiaLatLng", new[] { "AsignacionId" });
            DropTable("dbo.DatosParroquiaLatLng",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosParroquiaLatLng_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
