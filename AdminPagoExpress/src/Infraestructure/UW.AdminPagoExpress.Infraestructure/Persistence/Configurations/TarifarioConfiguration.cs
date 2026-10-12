using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UW.AdminPagoExpress.Infrastructure.Persistence.Configurations;

public class TarifarioConfiguration : IEntityTypeConfiguration<Tarifario>
{
    public void Configure(EntityTypeBuilder<Tarifario> builder)
    {
        builder.ToTable("TARIFARIO_CO");

        builder.HasKey(t => t.Id).HasName("PK_TARIFARIO_CO");

        builder.Property(t => t.Id)
            .HasColumnName("ID_TARIFARIO")
            .HasColumnType("NUMBER")
            .ValueGeneratedOnAdd()
            .UseSequence("SEQ_TARIFARIO_CO");

        builder.Property(t => t.PrecioAlumno)
            .HasColumnName("PRECIO_ALUMNO")
            .HasColumnType("NUMBER(18,2)")
            .IsRequired();

        builder.Property(t => t.PrecioExterno)
            .HasColumnName("PRECIO_EXTERNO")
            .HasColumnType("NUMBER(18,2)")
            .IsRequired();

        builder.Property(t => t.PrecioEmpleado)
            .HasColumnName("PRECIO_EMPLEADO")
            .HasColumnType("NUMBER(18,2)")
            .IsRequired();

        builder.Property(t => t.CodigoSpring)
            .HasColumnName("CODIGO_SPRING")
            .HasColumnType("VARCHAR2(50)")
            .IsRequired();

        builder.Property(t => t.CodigoOracle)
            .HasColumnName("CODIGO_ORACLE")
            .HasColumnType("VARCHAR2(50)")
            .IsRequired();

        builder.Property(t => t.IdTratamiento)
            .HasColumnName("ID_TRATAMIENTO")
            .HasColumnType("NUMBER")
            .IsRequired();

        builder.HasOne<Tratamiento>()
            .WithMany()
            .HasForeignKey(t => t.IdTratamiento)
            .HasConstraintName("FK_TARIFARIO_TRATAMIENTO");
    }
}
