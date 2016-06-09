namespace Gcc.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cliente", "Idade", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cliente", "Idade", c => c.DateTime(nullable: false));
        }
    }
}
