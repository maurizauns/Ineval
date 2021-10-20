namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _191020211133 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosSedesAsignacion",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SedeId = c.Guid(nullable: false),
                        SessionId = c.String(),
                        LaboratorioId = c.String(),
                        SustentanteId = c.Guid(nullable: false),
                        Code = c.String(),
                        Description = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                        DatosSedes_Id = c.Guid(),
                        DatosTemporales_Id = c.Guid(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosSedesAsignacion_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DatosSedes", t => t.DatosSedes_Id)
                .ForeignKey("dbo.DatosTemporales", t => t.DatosTemporales_Id)
                .Index(t => t.DatosSedes_Id)
                .Index(t => t.DatosTemporales_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatosSedesAsignacion", "DatosTemporales_Id", "dbo.DatosTemporales");
            DropForeignKey("dbo.DatosSedesAsignacion", "DatosSedes_Id", "dbo.DatosSedes");
            DropIndex("dbo.DatosSedesAsignacion", new[] { "DatosTemporales_Id" });
            DropIndex("dbo.DatosSedesAsignacion", new[] { "DatosSedes_Id" });
            DropTable("dbo.DatosSedesAsignacion",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosSedesAsignacion_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
