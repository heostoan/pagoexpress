using UW.AdminPagoExpress.Domain.Entities;

namespace UW.AdminPagoExpress.Application.Interfaces.Persistence;

public interface IComposicionTratamientoRepository
{
    Task<List<ComposicionTratamiento>> ListarPorTratamientoAsync(long idTratamientoPadre, CancellationToken ct = default);
    Task<List<ComposicionTratamiento>> ListarTodasAsync(CancellationToken ct = default);
    Task<long> CrearAsync(ComposicionTratamiento entity, CancellationToken ct = default);
    Task CrearVariosAsync(IEnumerable<ComposicionTratamiento> entities, CancellationToken ct = default);
    Task ActualizarEstadoAsync(long idComposicion, CancellationToken ct = default);
}
