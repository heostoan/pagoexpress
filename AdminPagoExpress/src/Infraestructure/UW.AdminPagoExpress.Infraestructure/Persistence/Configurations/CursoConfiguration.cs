using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UW.AdminPagoExpress.Infrastructure.Persistence.Configurations;

public class CursoConfiguration : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("CURSOS_CO");

        builder.HasKey(c => c.Id).HasName("PK_CURSOS_CO");

        builder.Property(c => c.Id)
            .HasColumnName("ID_CURSO")
            .HasColumnType("NUMBER")
            .ValueGeneratedOnAdd()
            .UseSequence("SEQ_CURSOS_CO");

        builder.Property(c => c.Nombre)
            .HasColumnName("NOMBRE")
            .HasColumnType("VARCHAR2(200)")
            .IsRequired();

        builder.Property(c => c.CodigoCurso)
            .HasColumnName("CODIGO_CURSO")
            .HasColumnType("VARCHAR2(50)")
            .IsRequired();

        builder.Property(c => c.Ciclo)
            .HasColumnName("CICLO")
            .HasColumnType("VARCHAR2(50)")
            .IsRequired();

        builder.Property(c => c.Sis)
            .HasColumnName("SIS")
            .HasColumnType("VARCHAR2(50)")
            .IsRequired();

        builder.Property(c => c.Estado)
            .HasColumnName("ESTADO")
            .HasColumnType("CHAR(1)")
            .HasMaxLength(1)
            .IsRequired();
    }
}
