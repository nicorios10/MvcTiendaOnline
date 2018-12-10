namespace TiendaOnlineMvc.Migrations.TiendaOnline
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageUri : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageUri", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageUri");
        }
    }
}
