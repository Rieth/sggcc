namespace Gcc.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoEnqueteNoVoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Voto", "EnqueteID", c => c.Long());
            AddForeignKey("dbo.Voto", "EnqueteID", "dbo.Enquete", "EnqueteID");
            CreateIndex("dbo.Voto", "EnqueteID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Voto", new[] { "EnqueteID" });
            DropForeignKey("dbo.Voto", "EnqueteID", "dbo.Enquete");
            DropColumn("dbo.Voto", "EnqueteID");
        }
    }
}
