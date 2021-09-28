namespace Ineval.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _020920212331 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DatosSustentantes", "Newess", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DatosSustentantes", "Newess");
        }
    }
}
