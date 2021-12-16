namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _141220210936 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosFiltrosLaboratorios",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AsignacionId = c.Guid(),
                        Filtro1 = c.Int(),
                        Filtro2 = c.String(),
                        Filtro3 = c.String(),
                        Filtro4 = c.String(),
                        Filtro5 = c.String(),
                        Code = c.String(),
                        Description = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosFiltrosLaboratorio_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Asignacion", t => t.AsignacionId)
                .Index(t => t.AsignacionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatosFiltrosLaboratorios", "AsignacionId", "dbo.Asignacion");
            DropIndex("dbo.DatosFiltrosLaboratorios", new[] { "AsignacionId" });
            DropTable("dbo.DatosFiltrosLaboratorios",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosFiltrosLaboratorio_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
