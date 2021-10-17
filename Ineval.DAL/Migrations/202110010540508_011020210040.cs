namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _011020210040 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosCantonLatLng",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AsignacionId = c.Guid(),
                        Pais = c.String(),
                        Id_Provincia = c.String(),
                        Provincia = c.String(),
                        CantonLat = c.String(),
                        CantonLng = c.String(),
                        Code = c.String(),
                        Description = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosCantonLatLng_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Asignacion", t => t.AsignacionId)
                .Index(t => t.AsignacionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatosCantonLatLng", "AsignacionId", "dbo.Asignacion");
            DropIndex("dbo.DatosCantonLatLng", new[] { "AsignacionId" });
            DropTable("dbo.DatosCantonLatLng",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosCantonLatLng_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
