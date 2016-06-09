namespace Gcc.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alternativa",
                c => new
                    {
                        AlternativaID = c.Long(nullable: false, identity: true),
                        EnqueteID = c.Long(),
                        Nome = c.String(),
                        Valor = c.String(),
                    })
                .PrimaryKey(t => t.AlternativaID)
                .ForeignKey("dbo.Enquete", t => t.EnqueteID)
                .Index(t => t.EnqueteID);
            
            CreateTable(
                "dbo.Enquete",
                c => new
                    {
                        EnqueteID = c.Long(nullable: false, identity: true),
                        GrupoID = c.Long(),
                        Nome = c.String(),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.EnqueteID)
                .ForeignKey("dbo.Grupo", t => t.GrupoID)
                .Index(t => t.GrupoID);
            
            CreateTable(
                "dbo.Grupo",
                c => new
                    {
                        GrupoID = c.Long(nullable: false, identity: true),
                        EnderecoID = c.Long(),
                        Nome = c.String(),
                        Descricao = c.String(),
                        QuantidadeTotal = c.Int(),
                        Visibilidade = c.String(),
                        Email = c.String(),
                        Telefone = c.String(),
                        Celular = c.String(),
                    })
                .PrimaryKey(t => t.GrupoID)
                .ForeignKey("dbo.Endereco", t => t.EnderecoID)
                .Index(t => t.EnderecoID);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        EnderecoID = c.Long(nullable: false, identity: true),
                        Estado = c.String(),
                        Cidade = c.String(),
                        Bairro = c.String(),
                        Rua = c.String(),
                        Numero = c.String(),
                        CEP = c.String(),
                        Complemento = c.String(),
                    })
                .PrimaryKey(t => t.EnderecoID);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ClienteID = c.Long(nullable: false, identity: true),
                        EnderecoID = c.Long(),
                        UserId = c.Long(),
                        Nome = c.String(),
                        Idade = c.DateTime(nullable: false),
                        Telefone = c.String(),
                        Celular = c.String(),
                    })
                .PrimaryKey(t => t.ClienteID)
                .ForeignKey("dbo.Endereco", t => t.EnderecoID)
                .Index(t => t.EnderecoID);
            
            CreateTable(
                "dbo.ParticipantesGrupo",
                c => new
                    {
                        ParticipantesGrupoID = c.Long(nullable: false, identity: true),
                        GrupoID = c.Long(),
                        ClienteID = c.Long(),
                    })
                .PrimaryKey(t => t.ParticipantesGrupoID)
                .ForeignKey("dbo.Cliente", t => t.ClienteID)
                .ForeignKey("dbo.Grupo", t => t.GrupoID)
                .Index(t => t.ClienteID)
                .Index(t => t.GrupoID);
            
            CreateTable(
                "dbo.ProdutoRequerido",
                c => new
                    {
                        ProdutoRequeridoID = c.Long(nullable: false, identity: true),
                        ProdutoID = c.Long(),
                        ClienteID = c.Long(),
                        GrupoID = c.Long(),
                        Quantidade = c.Int(nullable: false),
                        UnidadeMedida = c.String(),
                    })
                .PrimaryKey(t => t.ProdutoRequeridoID)
                .ForeignKey("dbo.Cliente", t => t.ClienteID)
                .ForeignKey("dbo.Grupo", t => t.GrupoID)
                .ForeignKey("dbo.Produto", t => t.ProdutoID)
                .Index(t => t.ClienteID)
                .Index(t => t.GrupoID)
                .Index(t => t.ProdutoID);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        ProdutoID = c.Long(nullable: false, identity: true),
                        GrupoID = c.Long(),
                        Nome = c.String(),
                        Descricao = c.String(),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.ProdutoID)
                .ForeignKey("dbo.Grupo", t => t.GrupoID)
                .Index(t => t.GrupoID);
            
            CreateTable(
                "dbo.Caracteristica",
                c => new
                    {
                        CaracteristicaID = c.Long(nullable: false, identity: true),
                        ProdutoID = c.Long(),
                        Nome = c.String(),
                        Valor = c.String(),
                        Valor2 = c.String(),
                        UnidadeMedida = c.String(),
                        TipoMedida = c.String(),
                    })
                .PrimaryKey(t => t.CaracteristicaID)
                .ForeignKey("dbo.Produto", t => t.ProdutoID)
                .Index(t => t.ProdutoID);
            
            CreateTable(
                "dbo.Voto",
                c => new
                    {
                        VotoID = c.Long(nullable: false, identity: true),
                        ClienteID = c.Long(),
                        AlternativaID = c.Long(),
                    })
                .PrimaryKey(t => t.VotoID)
                .ForeignKey("dbo.Alternativa", t => t.AlternativaID)
                .ForeignKey("dbo.Cliente", t => t.ClienteID)
                .Index(t => t.AlternativaID)
                .Index(t => t.ClienteID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Voto", new[] { "ClienteID" });
            DropIndex("dbo.Voto", new[] { "AlternativaID" });
            DropIndex("dbo.Caracteristica", new[] { "ProdutoID" });
            DropIndex("dbo.Produto", new[] { "GrupoID" });
            DropIndex("dbo.ProdutoRequerido", new[] { "ProdutoID" });
            DropIndex("dbo.ProdutoRequerido", new[] { "GrupoID" });
            DropIndex("dbo.ProdutoRequerido", new[] { "ClienteID" });
            DropIndex("dbo.ParticipantesGrupo", new[] { "GrupoID" });
            DropIndex("dbo.ParticipantesGrupo", new[] { "ClienteID" });
            DropIndex("dbo.Cliente", new[] { "EnderecoID" });
            DropIndex("dbo.Grupo", new[] { "EnderecoID" });
            DropIndex("dbo.Enquete", new[] { "GrupoID" });
            DropIndex("dbo.Alternativa", new[] { "EnqueteID" });
            DropForeignKey("dbo.Voto", "ClienteID", "dbo.Cliente");
            DropForeignKey("dbo.Voto", "AlternativaID", "dbo.Alternativa");
            DropForeignKey("dbo.Caracteristica", "ProdutoID", "dbo.Produto");
            DropForeignKey("dbo.Produto", "GrupoID", "dbo.Grupo");
            DropForeignKey("dbo.ProdutoRequerido", "ProdutoID", "dbo.Produto");
            DropForeignKey("dbo.ProdutoRequerido", "GrupoID", "dbo.Grupo");
            DropForeignKey("dbo.ProdutoRequerido", "ClienteID", "dbo.Cliente");
            DropForeignKey("dbo.ParticipantesGrupo", "GrupoID", "dbo.Grupo");
            DropForeignKey("dbo.ParticipantesGrupo", "ClienteID", "dbo.Cliente");
            DropForeignKey("dbo.Cliente", "EnderecoID", "dbo.Endereco");
            DropForeignKey("dbo.Grupo", "EnderecoID", "dbo.Endereco");
            DropForeignKey("dbo.Enquete", "GrupoID", "dbo.Grupo");
            DropForeignKey("dbo.Alternativa", "EnqueteID", "dbo.Enquete");
            DropTable("dbo.Voto");
            DropTable("dbo.Caracteristica");
            DropTable("dbo.Produto");
            DropTable("dbo.ProdutoRequerido");
            DropTable("dbo.ParticipantesGrupo");
            DropTable("dbo.Cliente");
            DropTable("dbo.Endereco");
            DropTable("dbo.Grupo");
            DropTable("dbo.Enquete");
            DropTable("dbo.Alternativa");
        }
    }
}
