namespace Gcc.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoCliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cliente", "Email");
        }
    }
}
