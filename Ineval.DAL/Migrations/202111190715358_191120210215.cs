namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _191120210215 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosExcelInstituciones",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosExcelInstituciones_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DatosExcelInstituciones",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosExcelInstituciones_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
