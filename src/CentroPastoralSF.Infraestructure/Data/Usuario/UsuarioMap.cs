using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DizimistaDomain = CentroPastoralSF.Domain.Dizimista.Dizimista;
using UsuarioDomain = CentroPastoralSF.Domain.Usuario.Usuario;

namespace CentroPastoralSF.Infraestructure.Data.Usuario
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioDomain>
    {
        public void Configure(EntityTypeBuilder<UsuarioDomain> builder)
        {
            builder.ToTable("T_USUARIO");

            builder.HasKey(u => u.Id).HasName("PK_T_USUARIO_ID_USUARIO");

            builder.Property(u => u.Id)
                .HasColumnName("ID_USUARIO");

            builder.Property(u => u.Senha)
                .HasColumnName("SNH_SENHA")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.DataCriacao)
                .HasColumnName("DAT_DATA_CRIACAO")
                .IsRequired();

            builder.Property(u => u.DataAtualizacao)
                .HasColumnName("DAT_DATA_ATUALIZACAO")
                .IsRequired();

            builder.OwnsOne(u => u.Login)
                .Property(u => u.Endereco)
                .HasColumnName("EML_EMAIL")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(300)
                .IsRequired();

            builder.OwnsOne(u => u.Login)
                .Ignore(u => u.Validacao);

            builder.OwnsOne(u => u.Nome)
                .Property(u => u.Nome)
                .HasColumnName("NOM_NOME")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.OwnsOne(u => u.Nome)
                .Property(u => u.Sobrenome)
                .HasColumnName("NOM_SOBRENOME")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.OwnsOne(u => u.Nome)
                .Ignore(u => u.Validacao);

            builder.HasMany<DizimistaDomain>()
                .WithOne()
                .HasForeignKey(u => u.UsuarioId)
                .HasConstraintName("FK_T_DIZIMISTA_ID_USUARIO")
            .IsRequired();

            builder.Ignore(u => u.Validacao);
        }
    }
}