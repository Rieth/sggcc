namespace Gcc.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoValidacoes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Alternativa", "Valor", c => c.String(nullable: false));
            AlterColumn("dbo.Enquete", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Grupo", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Cliente", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Produto", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Produto", "UnidadeMedida", c => c.String(nullable: false));
            AlterColumn("dbo.Caracteristica", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Caracteristica", "Valor", c => c.String(nullable: false));
            AlterColumn("dbo.Caracteristica", "UnidadeMedida", c => c.String(nullable: false));
            DropColumn("dbo.Alternativa", "Nome");
            DropColumn("dbo.Grupo", "Visibilidade");
            DropColumn("dbo.Cliente", "Idade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cliente", "Idade", c => c.DateTime());
            AddColumn("dbo.Grupo", "Visibilidade", c => c.String());
            AddColumn("dbo.Alternativa", "Nome", c => c.String());
            AlterColumn("dbo.Caracteristica", "UnidadeMedida", c => c.String());
            AlterColumn("dbo.Caracteristica", "Valor", c => c.String());
            AlterColumn("dbo.Caracteristica", "Nome", c => c.String());
            AlterColumn("dbo.Produto", "UnidadeMedida", c => c.String());
            AlterColumn("dbo.Produto", "Nome", c => c.String());
            AlterColumn("dbo.Cliente", "Nome", c => c.String());
            AlterColumn("dbo.Grupo", "Nome", c => c.String());
            AlterColumn("dbo.Enquete", "Nome", c => c.String());
            AlterColumn("dbo.Alternativa", "Valor", c => c.String());
        }
    }
}
