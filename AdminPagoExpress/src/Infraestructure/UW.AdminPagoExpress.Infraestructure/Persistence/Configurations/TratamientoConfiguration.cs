using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UW.AdminPagoExpress.Infrastructure.Persistence.Configurations;

public class TratamientoConfiguration : IEntityTypeConfiguration<Tratamiento>
{
    public void Configure(EntityTypeBuilder<Tratamiento> builder)
    {
        builder.ToTable("TRATAMIENTOS_CO");

        builder.HasKey(t => t.Id)
            .HasName("PK_TRATAMIENTOS_CO");

        builder.Property(t => t.Id)
            .HasColumnName("ID_TRATAMIENTO")
            .HasColumnType("NUMBER")
            .ValueGeneratedOnAdd()
            .UseSequence("SEQ_TRATAMIENTOS_CO"); // Asegúrate de que este sea el nombre real de la secuencia en Oracle

        builder.Property(t => t.Nombre)
            .HasColumnName("NOMBRE")
            .HasColumnType("VARCHAR2(200)")
            .IsRequired();

        // Mapeo de CHAR(1) a bool
        builder.Property(t => t.HistoriaClinica)
            .HasColumnName("HISTORIA_CLINICA")
            .HasColumnType("CHAR(1)")
            .HasMaxLength(1)
            .IsRequired();

        builder.Property(t => t.Estado)
            .HasColumnName("ESTADO")
            .HasColumnType("CHAR(1)")
            .HasMaxLength(1)
            .IsRequired();

        builder.Property(t => t.VentaLibre)
            .HasColumnName("VENTA_LIBRE")
            .HasColumnType("CHAR(1)")
            .HasMaxLength(1)
            .IsRequired();

        builder.Property(t => t.IdCategoria)
            .HasColumnName("ID_CATEGORIA")
            .HasColumnType("NUMBER")
            .IsRequired();

        // Relación con Categoría (FK)
        /*builder.HasOne<Categoria>() // Asumiendo que existe la entidad Categoria
            .WithMany()
            .HasForeignKey(t => t.IdCategoria)
            .HasConstraintName("FK_TRATAMIENTO_CATEGORIA");*/

        builder.HasOne(t => t.Categoria)
              .WithMany()
              .HasForeignKey(t => t.IdCategoria)
              .HasConstraintName("FK_TRATAMIENTO_CATEGORIA");
    }
}