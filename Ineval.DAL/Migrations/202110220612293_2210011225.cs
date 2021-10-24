namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _2210011225 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailParametros",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        EmailPrincipal = c.String(),
                        EmailPassword = c.String(),
                        EmailCopia = c.String(),
                        Code = c.String(),
                        Description = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EmailParametros_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmailParametros",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EmailParametros_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
