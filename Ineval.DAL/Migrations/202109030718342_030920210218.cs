namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _030920210218 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Asignacion",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        NombreProcesoId = c.Guid(),
                        Code = c.String(),
                        Description = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Asignacion_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NombreProceso", t => t.NombreProcesoId)
                .Index(t => t.NombreProcesoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Asignacion", "NombreProcesoId", "dbo.NombreProceso");
            DropIndex("dbo.Asignacion", new[] { "NombreProcesoId" });
            DropTable("dbo.Asignacion",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Asignacion_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
