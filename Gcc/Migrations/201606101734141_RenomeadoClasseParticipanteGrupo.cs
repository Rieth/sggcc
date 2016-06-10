namespace Gcc.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenomeadoClasseParticipanteGrupo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ParticipantesGrupo", "ClienteID", "dbo.Cliente");
            DropForeignKey("dbo.ParticipantesGrupo", "GrupoID", "dbo.Grupo");
            DropIndex("dbo.ParticipantesGrupo", new[] { "ClienteID" });
            DropIndex("dbo.ParticipantesGrupo", new[] { "GrupoID" });
            CreateTable(
                "dbo.ParticipanteGrupo",
                c => new
                    {
                        ParticipanteGrupoID = c.Long(nullable: false, identity: true),
                        GrupoID = c.Long(),
                        ClienteID = c.Long(),
                    })
                .PrimaryKey(t => t.ParticipanteGrupoID)
                .ForeignKey("dbo.Cliente", t => t.ClienteID)
                .ForeignKey("dbo.Grupo", t => t.GrupoID)
                .Index(t => t.ClienteID)
                .Index(t => t.GrupoID);
            
            DropTable("dbo.ParticipantesGrupo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ParticipantesGrupo",
                c => new
                    {
                        ParticipantesGrupoID = c.Long(nullable: false, identity: true),
                        GrupoID = c.Long(),
                        ClienteID = c.Long(),
                    })
                .PrimaryKey(t => t.ParticipantesGrupoID);
            
            DropIndex("dbo.ParticipanteGrupo", new[] { "GrupoID" });
            DropIndex("dbo.ParticipanteGrupo", new[] { "ClienteID" });
            DropForeignKey("dbo.ParticipanteGrupo", "GrupoID", "dbo.Grupo");
            DropForeignKey("dbo.ParticipanteGrupo", "ClienteID", "dbo.Cliente");
            DropTable("dbo.ParticipanteGrupo");
            CreateIndex("dbo.ParticipantesGrupo", "GrupoID");
            CreateIndex("dbo.ParticipantesGrupo", "ClienteID");
            AddForeignKey("dbo.ParticipantesGrupo", "GrupoID", "dbo.Grupo", "GrupoID");
            AddForeignKey("dbo.ParticipantesGrupo", "ClienteID", "dbo.Cliente", "ClienteID");
        }
    }
}
