using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UW.AdminPagoExpress.Domain.Entities;

namespace UW.AdminPagoExpress.Infrastructure.Persistence.Configurations;

public class DetalleTratamientoCursoConfiguration : IEntityTypeConfiguration<DetalleTratamientoCurso>
{
    public void Configure(EntityTypeBuilder<DetalleTratamientoCurso> builder)
    {
        builder.ToTable("DETALLE_TRATAMIENTO_CURSO_CO");
        builder.HasKey(d => d.IdDetalleTratamientoCurso);
        builder.Property(d => d.IdDetalleTratamientoCurso).HasColumnName("ID_DETALLE_TRATAMIENTO_CURSO");
        builder.Property(d => d.IdTratamiento).HasColumnName("ID_TRATAMIENTO");
        builder.Property(d => d.IdCurso).HasColumnName("ID_CURSO");
        builder.Property(d => d.Estado).HasColumnName("ESTADO");
    }
}
