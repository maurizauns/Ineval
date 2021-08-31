namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _280820211444 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parroquia",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CountryId = c.Guid(),
                        ProvinceId = c.Guid(),
                        CantonId = c.Guid(),
                        Code = c.String(),
                        Description = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Parroquia_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Canton", t => t.CantonId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Province", t => t.ProvinceId)
                .Index(t => t.CountryId)
                .Index(t => t.ProvinceId)
                .Index(t => t.CantonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parroquia", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.Parroquia", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Parroquia", "CantonId", "dbo.Canton");
            DropIndex("dbo.Parroquia", new[] { "CantonId" });
            DropIndex("dbo.Parroquia", new[] { "ProvinceId" });
            DropIndex("dbo.Parroquia", new[] { "CountryId" });
            DropTable("dbo.Parroquia",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Parroquia_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
