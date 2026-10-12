using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UW.AdminPagoExpress.Domain.Entities;

namespace UW.AdminPagoExpress.Infrastructure.Persistence.Configurations;

public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("PACIENTES_CO");

        builder.HasKey(t => t.Id).HasName("PK_PACIENTE_CO");

        builder.Property(t => t.Id)
            .HasColumnName("ID_PACIENTE")
            .HasColumnType("NUMBER")
            .ValueGeneratedOnAdd()
            .UseSequence("SEQ_PACIENTE_CO");

        builder.Property(p => p.Nombres)
               .HasColumnName("NOMBRES")
               .HasMaxLength(100)
               .IsUnicode(false)
               .IsRequired();

        builder.Property(p => p.ApellidoPaterno)
               .HasColumnName("APELLIDO_PATERNO")
               .HasMaxLength(100)
               .IsUnicode(false)
               .IsRequired();

        builder.Property(p => p.ApellidoMaterno)
               .HasColumnName("APELLIDO_MATERNO")
               .HasMaxLength(100)
               .IsUnicode(false)
               .IsRequired();

        builder.Property(p => p.FechaNacimiento)
               .HasColumnName("FECHA_NACIMIENTO")
               .HasColumnType("DATE");

        builder.Property(p => p.Genero)
               .HasColumnName("GENERO")
               .HasColumnType("CHAR(1)")
               .HasMaxLength(1)
               .IsFixedLength()
               .IsUnicode(false);

        builder.Property(p => p.DocIdentidad)
               .HasColumnName("DOC_IDENTIDAD")
               .HasMaxLength(16)
               .IsUnicode(false)
               .IsRequired();

        builder.Property(p => p.TipoDocumento)
               .HasColumnName("TIPO_DOCUMENTO")
               .HasMaxLength(5)
               .IsUnicode(false);

        builder.Property(p => p.Celular)
               .HasColumnName("CELULAR")
               .HasMaxLength(20)
               .IsUnicode(false);

        builder.Property(p => p.Correo)
               .HasColumnName("CORREO")
               .HasMaxLength(100)
               .IsUnicode(false);

        /*builder.Property(p => p.IdCliente)
               .HasColumnName("IDCLIENTE")
               .HasMaxLength(100)
               .IsUnicode(false);*/

        builder.Property(p => p.IdAlumno)
               .HasColumnName("ID_ALUMNO")
               .HasMaxLength(15)
               .IsUnicode(false);

        builder.Property(p => p.IdFirma)
               .HasColumnName("ID_FIRMA")
               .HasColumnType("NUMBER");

        /*builder.Property(p => p.UriFoto)
               .HasColumnName("URI_FOTO")
               .HasColumnType("CLOB")
               .IsRequired(false);*/

        builder.Property(p => p.FechaCreacion)
               .HasColumnName("FECHA_CREACION")
               .HasColumnType("DATE")
               .HasDefaultValueSql("SYSDATE")
               .ValueGeneratedOnAdd();

        builder.Property(p => p.FechaModificacion)
               .HasColumnName("FECHA_MODIFICACION")
               .HasColumnType("DATE");

        builder.HasIndex(p => p.DocIdentidad)
               .HasDatabaseName("IX_PACIENTE_CO_DOC_IDENTIDAD");

        /*builder.HasIndex(p => p.IdCliente)
               .HasDatabaseName("IX_PACIENTE_CO_IDCLIENTE");*/

        builder.HasIndex(p => p.IdAlumno)
               .HasDatabaseName("IX_PACIENTE_CO_ID_ALUMNO");
    }
}
