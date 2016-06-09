namespace Gcc.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracoesProduto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produto", "UnidadeMedida", c => c.String());
            DropColumn("dbo.ProdutoRequerido", "UnidadeMedida");
            DropColumn("dbo.Caracteristica", "Valor2");
            DropColumn("dbo.Caracteristica", "TipoMedida");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Caracteristica", "TipoMedida", c => c.String());
            AddColumn("dbo.Caracteristica", "Valor2", c => c.String());
            AddColumn("dbo.ProdutoRequerido", "UnidadeMedida", c => c.String());
            DropColumn("dbo.Produto", "UnidadeMedida");
        }
    }
}
