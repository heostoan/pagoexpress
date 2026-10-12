using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UW.AdminPagoExpress.Domain.Entities;

namespace UW.AdminPagoExpress.Infrastructure.Persistence.Configurations;

public class ComposicionTratamientoConfiguration : IEntityTypeConfiguration<ComposicionTratamiento>
{
    public void Configure(EntityTypeBuilder<ComposicionTratamiento> builder)
    {
        builder.ToTable("COMPOSICION_TRATAMIENTO_CO");
        builder.HasKey(c => c.IdComposicionTratamiento);
        builder.Property(c => c.IdComposicionTratamiento).HasColumnName("ID_COMPOSICION_TRATAMIENTO_CO");
        builder.Property(c => c.IdTratamiento).HasColumnName("ID_TRATAMIENTO");
        builder.Property(c => c.CodigoOracle).HasColumnName("CODIGO_ORACLE");
        builder.Property(c => c.Cantidad).HasColumnName("CANTIDAD");
        builder.Property(c => c.UnidadMedida).HasColumnName("UNIDAD_MEDIDA");
    }
}
