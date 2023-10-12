using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rinha.Entities;

namespace Rinha.Data.Mapping
{
    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("pessoas");

            builder.Property(p => p.Id).HasColumnName("id");

            builder.Property(p => p.Apelido).HasMaxLength(32).HasColumnName("apelido");

            builder.Property(p => p.Nome).HasMaxLength(100).HasColumnName("nome");

            builder.Property(p => p.Nascimento).HasColumnName("nascimento");

            builder.Property(p => p.Stack)
                .HasConversion(
                    s => string.Join(',', s),
                    s => s.Split(',', StringSplitOptions.None).ToList()
                ).HasColumnName("stack");
        }
    }
}