namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _202109272223 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosInstituciones",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AsignacionId = c.Guid(),
                        Amie = c.String(),
                        NombreInstitucion = c.String(),
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
                        sostenimiento_institucion = c.String(),
                        regimen_institucion = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosInstituciones_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Asignacion", t => t.AsignacionId)
                .Index(t => t.AsignacionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatosInstituciones", "AsignacionId", "dbo.Asignacion");
            DropIndex("dbo.DatosInstituciones", new[] { "AsignacionId" });
            DropTable("dbo.DatosInstituciones",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosInstituciones_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
