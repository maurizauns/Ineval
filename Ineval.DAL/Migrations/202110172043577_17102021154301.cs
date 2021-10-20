namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _17102021154301 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosSedes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AsignacionId = c.Guid(),
                        NumeroSession = c.Int(nullable: false),
                        NumeroLaboratorio = c.Int(nullable: false),
                        coordenada_lat = c.String(),
                        coordenada_lng = c.String(),
                        Code = c.String(),
                        Description = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosSedes_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Asignacion", t => t.AsignacionId)
                .Index(t => t.AsignacionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatosSedes", "AsignacionId", "dbo.Asignacion");
            DropIndex("dbo.DatosSedes", new[] { "AsignacionId" });
            DropTable("dbo.DatosSedes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosSedes_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
