using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using DizimistaDomain = CentroPastoralSF.Domain.Dizimista.Dizimista;

namespace CentroPastoralSF.Infraestructure.Data.Dizimista
{
    public class DizimistaMap : IEntityTypeConfiguration<DizimistaDomain>
    {
        public void Configure(EntityTypeBuilder<DizimistaDomain> builder)
        {
            builder.ToTable("T_DIZIMISTA");

            builder.HasKey(d => d.Id).HasName("PK_T_DIZIMISTA_ID_DIZIMISTA");

            builder.Property(d => d.Id)
                .HasColumnName("ID_DIZIMISTA");

            builder.Property(d => d.DataCriacao)
                .HasColumnName("DAT_DATA_CRIACAO")
                .IsRequired();

            builder.Property(d => d.DataAtualizacao)
                .HasColumnName("DAT_DATA_ATUALIZACAO")
                .IsRequired();

            builder.Ignore(d => d.Validacao);

            builder.OwnsOne(d => d.Nome)
                   .Property(d => d.Nome)
                   .HasColumnName("NOM_NOME")
                   .HasColumnType("NVARCHAR")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.OwnsOne(d => d.Nome)
                .Property(u => u.Sobrenome)
                .HasColumnName("NOM_SOBRENOME")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.OwnsOne(d => d.Nome)
                .Ignore(d => d.Validacao);

            builder.OwnsOne(d => d.Telefone)
                .Property(d => d.Ddd)
                .HasColumnName("NUM_DDD")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(2)
                .IsRequired();

            builder.OwnsOne(d => d.Telefone)
                .Property(d => d.Numero)
                .HasColumnName("NUM_NUMERO")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(9)
                .IsRequired();

            builder.OwnsOne(d => d.Telefone)
                .Ignore(d => d.Validacao);

            builder.OwnsOne(d => d.Endereco)
                .Property(d => d.Logradouro)
                .HasColumnName("END_LOGRADOURO")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.OwnsOne(d => d.Endereco)
                .Property(d => d.Numero)
                .HasColumnName("END_NUMERO")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(10)
                .IsRequired();

            builder.OwnsOne(d => d.Endereco)
                .Property(d => d.Complemento)
                .HasColumnName("END_COMPLEMENTO")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.OwnsOne(d => d.Endereco)
                .Property(d => d.Bairro)
                .HasColumnName("END_BAIRRO")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            builder.OwnsOne(d => d.Endereco)
                .Property(d => d.Municipio)
                .HasColumnName("END_MUNICIPIO")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            builder.OwnsOne(d => d.Endereco)
                .Property(d => d.UF)
                .HasColumnName("END_UF")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(2)
                .IsRequired();

            builder.OwnsOne(d => d.Endereco)
                .Property(d => d.Cep)
                .HasColumnName("END_CEP")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(8)
                .IsRequired();
            
            builder.OwnsOne(d => d.Endereco)
                .Ignore(d => d.Validacao);

            builder.OwnsOne(d => d.Email)
                .Property(d => d.Endereco)
                .HasColumnName("EML_EMAIL")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(300)
                .IsRequired();

            builder.OwnsOne(d => d.Email)
                .Ignore(d => d.Validacao);

            builder.Property(d => d.DataNascimento)
                .HasColumnName("DAT_DATA_NASCIMENTO")
                .IsRequired();

            builder.Property(d => d.UsuarioId)
                .HasColumnName("ID_USUARIO")
                .IsRequired();

            //builder.HasIndex(d => d.UsuarioId, "IX_T_DIZIMISTA_ID_USUARIO")
            //    .IsUnique();
        }
    }
}