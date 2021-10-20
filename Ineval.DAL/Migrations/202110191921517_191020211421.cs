namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _191020211421 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosFiltros",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AsignacionId = c.Guid(),
                        Filtro1 = c.Int(),
                        Filtro2 = c.Int(),
                        Filtro3 = c.Int(),
                        Filtro4 = c.Int(),
                        Filtro5 = c.Int(),
                        Code = c.String(),
                        Description = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosFiltros_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Asignacion", t => t.AsignacionId)
                .Index(t => t.AsignacionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatosFiltros", "AsignacionId", "dbo.Asignacion");
            DropIndex("dbo.DatosFiltros", new[] { "AsignacionId" });
            DropTable("dbo.DatosFiltros",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosFiltros_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
