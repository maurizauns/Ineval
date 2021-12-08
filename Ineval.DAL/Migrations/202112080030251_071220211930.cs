namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _071220211930 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosLaboratorio",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AsignacionId = c.Guid(),
                        sede = c.String(),
                        sede_institucion = c.String(),
                        id_provincia = c.String(),
                        provincia = c.String(),
                        canton_id = c.String(),
                        canton = c.String(),
                        id_parroquia = c.String(),
                        parroquia = c.String(),
                        id_circuito = c.String(),
                        circuito = c.String(),
                        id_distrito = c.String(),
                        distrito = c.String(),
                        id_zona = c.String(),
                        direccion = c.String(),
                        referencia = c.String(),
                        telefono1 = c.String(),
                        telefono2 = c.String(),
                        celular = c.String(),
                        sostenimiento = c.String(),
                        regimen = c.String(),
                        jornada = c.String(),
                        codigo_labooratorio = c.String(),
                        computadora_laboratorio = c.String(),
                        coordenada_Lat = c.String(),
                        coordenada_Lng = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosLaboratorio_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Asignacion", t => t.AsignacionId)
                .Index(t => t.AsignacionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatosLaboratorio", "AsignacionId", "dbo.Asignacion");
            DropIndex("dbo.DatosLaboratorio", new[] { "AsignacionId" });
            DropTable("dbo.DatosLaboratorio",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosLaboratorio_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
