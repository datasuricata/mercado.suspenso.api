using mercadosuspenso.domain.Models;
using mercadosuspenso.orm.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mercadosuspenso.orm
{
    public class MercadoDbContext : DbContext
    {
        public DbSet<Aceite> Aceite { get; set; }
        public DbSet<Vinculo> Vinculo { get; set; }
        public DbSet<Vistoria> Vistoria { get; set; }
        public DbSet<Distribuidor> Distribuidor { get; set; }
        public DbSet<Doacao> Doacao { get; set; }
        public DbSet<DoacaoProduto> DoacaoProduto { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Participante> Participante { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Varejista> Varejista { get; set; }

        public MercadoDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseDeleteCascadeOff();
            builder.UseAssemblyMapping();
            builder.UseDataSeeder();

            #region - map -

            builder.Entity<DoacaoProduto>().HasKey(c => new { c.DoacaoId, c.ProdutoId });

            builder.Entity<DoacaoProduto>().HasOne(c => c.Produto)
                .WithMany(c => c.DoacaoProdutos)
                .HasForeignKey(c => c.ProdutoId);

            builder.Entity<DoacaoProduto>().HasOne(c => c.Doacao)
                .WithMany(c => c.DoacaoProdutos)
                .HasForeignKey(c => c.DoacaoId);

            #endregion

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(nameof(Entity.CriadoEm)) != null || entry.Entity.GetType().GetProperty(nameof(Entity.AtualizadoEm)) != null))
            {
                if (entry.Property(nameof(Entity.CriadoEm)) != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property(nameof(Entity.CriadoEm)).CurrentValue = DateTimeOffset.Now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entry.Property(nameof(Entity.CriadoEm)).IsModified = false;
                    }
                }

                if (entry.Property(nameof(Entity.AtualizadoEm)) != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property(nameof(Entity.AtualizadoEm)).CurrentValue = null;
                    }
                
                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property(nameof(Entity.AtualizadoEm)).CurrentValue = DateTimeOffset.Now;
                    }
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}