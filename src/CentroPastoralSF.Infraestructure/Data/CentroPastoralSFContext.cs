using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DizimistaDomain = CentroPastoralSF.Domain.Dizimista.Dizimista;
using UsuarioDomain = CentroPastoralSF.Domain.Usuario.Usuario;
using ValidacaoDomain = CentroPastoralSF.Domain.Validacao;
using ErroDomain = CentroPastoralSF.Domain.Erro;


namespace CentroPastoralSF.Infraestructure.Data
{
    public class CentroPastoralSFContext : DbContext
    {
        public CentroPastoralSFContext(DbContextOptions<CentroPastoralSFContext> options)
            : base(options)
        { }

        public DbSet<DizimistaDomain> Dizimistas { get; set; } = null!;
        public DbSet<UsuarioDomain> Usuarios { get; set; } = null!;

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity.GetType().GetProperty("DataCriacao") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("DataCriacao").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("DataCriacao").IsModified = false;
            }

            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity.GetType().GetProperty("DataAtualizacao") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("DataAtualizacao").CurrentValue = DateTime.MinValue;

                if (entry.State == EntityState.Modified)
                    entry.Property("DataAtualizacao").CurrentValue = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidacaoDomain>();
            modelBuilder.Ignore<ErroDomain>();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}