using Gcc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Gcc.Data.DataLayerEntityFramework
{
    public class GccContext : DbContext
    {
        public GccContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Alternativa> Alternativas { get; set; }
        public DbSet<Caracteristica> Caracteristicas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecoes { get; set; }
        public DbSet<Enquete> Enquetes { get; set; }
        public DbSet<Grupo> Grupoes { get; set; }
        public DbSet<Produto> Produtoes { get; set; }
        public DbSet<ParticipantesGrupo> ParticipanteGrupoes { get; set; }
        public DbSet<ProdutoRequerido> ProdutoRequeridoes { get; set; }
        public DbSet<Voto> Votoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

    }
}