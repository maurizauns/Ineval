namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _141220211108 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosSedesLaboratorio",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AsignacionId = c.Guid(),
                        NumeroSession = c.Int(nullable: false),
                        NumeroLaboratorio = c.Int(nullable: false),
                        coordenada_lat = c.String(),
                        coordenada_lng = c.String(),
                        Agrupados = c.String(),
                        NumeroTotalSustentantes = c.Int(nullable: false),
                        Code = c.String(),
                        Description = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosSedesLaboratorio_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Asignacion", t => t.AsignacionId)
                .Index(t => t.AsignacionId);
            
            CreateTable(
                "dbo.DatosSedesAsignacionLaboratorio",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SedeId = c.Guid(nullable: false),
                        SessionId = c.String(),
                        LaboratorioId = c.String(),
                        Dia = c.String(),
                        FechaEval = c.String(),
                        Hora = c.String(),
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
                    { "DynamicFilter_DatosSedesAsignacionLaboratorio_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DatosSedesLaboratorio", t => t.DatosSedes_Id)
                .ForeignKey("dbo.DatosTemporales", t => t.DatosTemporales_Id)
                .Index(t => t.DatosSedes_Id)
                .Index(t => t.DatosTemporales_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatosSedesAsignacionLaboratorio", "DatosTemporales_Id", "dbo.DatosTemporales");
            DropForeignKey("dbo.DatosSedesAsignacionLaboratorio", "DatosSedes_Id", "dbo.DatosSedesLaboratorio");
            DropForeignKey("dbo.DatosSedesLaboratorio", "AsignacionId", "dbo.Asignacion");
            DropIndex("dbo.DatosSedesAsignacionLaboratorio", new[] { "DatosTemporales_Id" });
            DropIndex("dbo.DatosSedesAsignacionLaboratorio", new[] { "DatosSedes_Id" });
            DropIndex("dbo.DatosSedesLaboratorio", new[] { "AsignacionId" });
            DropTable("dbo.DatosSedesAsignacionLaboratorio",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosSedesAsignacionLaboratorio_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.DatosSedesLaboratorio",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosSedesLaboratorio_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
