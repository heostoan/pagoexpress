using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UW.AdminPagoExpress.Domain.Entities;

namespace UW.AdminPagoExpress.Infrastructure.Persistence.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("CATEGORIAS_CO");

        builder.HasKey(c => c.Id).HasName("PK_CATEGORIAS_CO");

        builder.Property(c => c.Id)
            .HasColumnName("ID_CATEGORIA")
            .HasColumnType("NUMBER")
            .ValueGeneratedOnAdd()
            //.UseSequence("SEQ_CATEGORIAS_CO")
            ;

        builder.Property(c => c.Nombre)
            .HasColumnName("NOMBRE")
            .HasColumnType("VARCHAR2(50)")
            .IsRequired();

        builder.Property(c => c.Estado)
            .HasColumnName("ESTADO")
            .HasColumnType("CHAR(1)")
            .HasMaxLength(1)
            .IsRequired();

        builder.Property(c => c.Descripcion)
            .HasColumnName("DESCRIPCION")
            .HasColumnType("VARCHAR2(255)")
            .IsRequired(false);
    }
}