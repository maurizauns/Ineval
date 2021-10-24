namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _221020210258 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosMapboxAPIKEY",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        APIKEY = c.String(),
                        NumeroMaximoConsulta = c.Int(nullable: false),
                        NumeroMininoConsulta = c.Int(nullable: false),
                        NumeroUsadasConsultas = c.Int(nullable: false),
                        Code = c.String(),
                        Description = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosMapboxAPIKEY_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DatosMapboxAPIKEY",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosMapboxAPIKEY_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
