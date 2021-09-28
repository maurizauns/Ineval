namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _202109272305 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosPersonalTerritorio",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AsignacionId = c.Guid(),
                        tipo_documento = c.String(),
                        numero_documento = c.String(),
                        nombres_apellidos = c.String(),
                        sexo = c.String(),
                        id_provincia = c.String(),
                        provincia = c.String(),
                        canton_id = c.String(),
                        canton = c.String(),
                        id_parroquia = c.String(),
                        parroquia = c.String(),
                        Tipo_Personal = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosPersonalTerritorio_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Asignacion", t => t.AsignacionId)
                .Index(t => t.AsignacionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatosPersonalTerritorio", "AsignacionId", "dbo.Asignacion");
            DropIndex("dbo.DatosPersonalTerritorio", new[] { "AsignacionId" });
            DropTable("dbo.DatosPersonalTerritorio",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosPersonalTerritorio_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
