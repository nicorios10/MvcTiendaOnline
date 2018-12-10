namespace TiendaOnlineMvc.Migrations.TiendaOnline
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Usuarios : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "UserType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "UserType");
        }
    }
}
