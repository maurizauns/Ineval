namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _202109272237 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParametrosIniciales",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AsignacionId = c.Guid(),
                        NumeroLaboratorios = c.Int(),
                        NumeroEquipos = c.Int(),
                        NumerosSesiones = c.Int(),
                        NumeroDiasEvaluar = c.Int(),
                        TiempoViaje = c.Int(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParametrosIniciales_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Asignacion", t => t.AsignacionId)
                .Index(t => t.AsignacionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParametrosIniciales", "AsignacionId", "dbo.Asignacion");
            DropIndex("dbo.ParametrosIniciales", new[] { "AsignacionId" });
            DropTable("dbo.ParametrosIniciales",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParametrosIniciales_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
