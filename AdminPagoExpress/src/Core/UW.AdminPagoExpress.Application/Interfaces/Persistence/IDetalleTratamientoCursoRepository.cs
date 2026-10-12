using UW.AdminPagoExpress.Domain.Entities;

namespace UW.AdminPagoExpress.Application.Interfaces.Persistence;

public interface IDetalleTratamientoCursoRepository
{
    Task<List<DetalleTratamientoCurso>> ListarPorCursoAsync(long idCurso, CancellationToken ct = default);
    Task<List<DetalleTratamientoCurso>> ListarPorTratamientoAsync(long idTratamiento, CancellationToken ct = default);
    Task<long> CrearAsync(DetalleTratamientoCurso entity, CancellationToken ct = default);
    Task CrearVariosAsync(IEnumerable<DetalleTratamientoCurso> entities, CancellationToken ct = default);
    Task ActualizarEstadoAsync(long idDetalle, int estado, CancellationToken ct = default);
}
